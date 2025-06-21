using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ExplosiveObject : MonoBehaviour
{
    SpriteRenderer sp;
    Rigidbody2D rb;

    public GameObject explosion;
    public float force;
    bool primed;
    public Sprite barrelGround;
    public Sprite barrelHeld;
    // Start is called before the first frame update
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void PrimeExplosive()
    {
        sp.color = Color.yellow;
        primed = true;
    }
    public void PickUp(Transform heldPos)
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.SetParent(heldPos);
        transform.position = heldPos.position;
        transform.rotation = Quaternion.Euler(Vector2.zero);
    }
    public void Drop()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(new Vector2(1, 0.5f) * 50);
        rb.AddTorque(Random.Range(25f, 100f));
    }

    public void Throw(Vector2 throwDirection)
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(throwDirection);
        rb.AddTorque(Random.Range(25f, 100f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (primed)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                StartCoroutine(ExplodeDelay(0f));
            }
            if (collision.gameObject.CompareTag("Item"))
            {
                StartCoroutine(ExplodeDelay(0f));
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(ExplodeDelay(0f));
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.GetComponent<PhysicsObject>())
            {
                if (collision.gameObject.GetComponent<PhysicsObject>().lethalSpeed)
                {
                    StartCoroutine(ExplodeDelay(0f));
                }
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            StartCoroutine(ExplodeDelay(0.2f));
        }
        if (collision.CompareTag("Damage"))
        {
            StartCoroutine(ExplodeDelay(0.8f));
        }
    }
    IEnumerator ExplodeDelay(float delayTime)
    {
        sp.color = Color.red;
        primed = false;

        yield return new WaitForSeconds(delayTime);

        CameraShaker.Instance.ShakeOnce(20, 8, 0, 1);
        float randomSpin = Random.Range(0f, 359f);
        var bomb = Instantiate(explosion, transform.position, Quaternion.Euler(Vector3.forward * randomSpin));
        bomb.GetComponent<Explosion>().force = force;
        Destroy(gameObject);
    }
}