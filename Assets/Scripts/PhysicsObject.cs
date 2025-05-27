using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public bool lethalSpeed;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude >= 20)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            lethalSpeed = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            lethalSpeed = false;
        }
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
            rb.AddForce(diff * collision.GetComponent<Explosion>().force);
            rb.AddTorque(Random.Range(25f, 100f));

        }
    }
}