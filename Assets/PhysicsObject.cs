using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage")) //SWORD HIT
        {
            rb.AddForce(collision.GetComponent<AttackBox>().attackDirection * 500);
            rb.AddTorque(Random.Range(25f, 100f));

        }
        if (collision.CompareTag("Explosion")) //EXPLOSION
        {
            Vector2 diff = transform.position - collision.transform.position;
            rb.AddForce(diff * 500);
        }
    }
}
