using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    SpriteRenderer sp;
    public float flashRate;
    public float riseDistance;
    Vector3 velocity = Vector3.zero;
    Vector3 startPos;

    int flashCount;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        StartCoroutine(FlashDelay());
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, startPos.y + riseDistance, startPos.z), ref velocity, 0.4f);
    }

    IEnumerator FlashDelay()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Flash());    
    }

    IEnumerator Flash()
    {
        sp.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(flashRate);

        sp.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(flashRate);

        flashCount++;

        if (flashCount >= 10)
        {
            Destroy(gameObject);
        }
        StartCoroutine(Flash());
    }
}
