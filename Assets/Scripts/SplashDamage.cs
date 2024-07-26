using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamage : MonoBehaviour
{
    public GameObject explosion;
    BoxCollider2D bc;
    public Vector2 startSize;
    public Vector2 maxSize;
    Vector2 velocity = Vector2.zero;
    bool splashing;

    private void Start()
    {
        startSize = transform.localScale;
        bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
    }
    public void Splash()
    {
        transform.localScale = startSize;
        StartCoroutine(SplashDelay());
        Instantiate(explosion, transform.position, Quaternion.Euler(0f, 0f, 0f));
    }
    void Update()
    {
        if (splashing)
        {
            transform.localScale = Vector2.SmoothDamp(transform.localScale, maxSize, ref velocity, 0.2f);
        }
    }
    IEnumerator SplashDelay()
    {
        splashing = true;
        bc.enabled = true;
        yield return new WaitForSeconds(0.7f);
        transform.localScale = startSize;
        splashing = false;
        bc.enabled = false;
    }
}
