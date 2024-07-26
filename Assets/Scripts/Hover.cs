using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    Vector3 startPosition;
    public Vector3 endPosition;

    public float holdTime;
    public float duration = 3f;

    public float hoverDistance; 

    public AnimationCurve animCurve;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(FloatUp(startPosition, endPosition));
    }

    IEnumerator FloatUp(Vector3 start, Vector3 end)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = animCurve.Evaluate(t);


            transform.position = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = end;

        yield return new WaitForSeconds(holdTime);

        StartCoroutine(FloatDown(transform.position, transform.position + new Vector3(0f, -hoverDistance, 0f)));
    }
    IEnumerator FloatDown(Vector3 start, Vector3 end)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = animCurve.Evaluate(t);


            transform.position = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = end;

        yield return new WaitForSeconds(holdTime);
        
        StartCoroutine(FloatUp(transform.position, transform.position + new Vector3(0f, hoverDistance, 0f)));
    }

}

