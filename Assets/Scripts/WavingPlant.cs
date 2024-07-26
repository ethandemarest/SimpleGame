using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavingPlant : MonoBehaviour
{
    float startingRotation;
    float rotation;
    public float brushVal;
    public float duration = 3f;
    public AnimationCurve animCurve;
    public AnimationCurve brushAnimation;

    bool hasBrushed;

    void Start()
    {
        startingRotation = transform.position.z;
        StartCoroutine(DelayStart(Random.Range(0.0f, 0.8f)));
    }
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation - brushVal));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasBrushed)
        {
            hasBrushed = true; 
            StartCoroutine(BrushPast());
        }
    }

    IEnumerator DelayStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(LerpValue(startingRotation, -10));
    }

    IEnumerator LerpValue(float start, float end)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = animCurve.Evaluate(t);

            rotation = Mathf.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        rotation = end;
        StartCoroutine(LerpValue(end, start));
    }

    IEnumerator BrushPast()
    {
        float timeElapsed = 0;

        while (timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime;
            brushVal = brushAnimation.Evaluate(timeElapsed);

            yield return null;
        }
        brushVal = 0f;
        hasBrushed = false;
    }
}
