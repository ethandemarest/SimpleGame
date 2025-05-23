using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiPush : MonoBehaviour
{
    Rigidbody2D rb;
    public bool touchingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (touchingPlayer)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.isKinematic = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }
}