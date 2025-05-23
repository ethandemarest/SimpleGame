using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{
    public GameObject explosion;
    bool primed;
    SpriteRenderer sp;
    // Start is called before the first frame update
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    public void PrimeExplosive()
    {
        sp.color = Color.yellow;
        primed = true;
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
        Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 359f))));
        Destroy(gameObject);
    }
}