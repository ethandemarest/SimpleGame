using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{
    public GameObject explosion;
    bool primed;
    // Start is called before the first frame update
    public void PrimeExplosive()
    {
        primed = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(GetComponent<Rigidbody2D>().velocity);

        if (!primed)
        {
            if(GetComponent<Rigidbody2D>().velocity.magnitude >= 10f)
            {
                StartCoroutine(ExplodeDelay(0f));
            }
        }
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
        primed = false;
        yield return new WaitForSeconds(delayTime);
        Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 359f))));
        Destroy(gameObject);
    }
}

