using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 startPosition;
    public Vector3 endPosition;

    public float delayStart;
    public float holdTimeOpen;
    public float holdTimeClosed;
    public float duration = 3f;

    public AnimationCurve animCurve;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(DelayStart(delayStart));
    }

    IEnumerator DelayStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(LerpValue(startPosition, endPosition));
    }

    IEnumerator LerpValue(Vector3 start, Vector3 end)
    {
        float timeElapsed = 0;

        while(timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = animCurve.Evaluate(t);
            transform.position = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = end;
        if(holdTimeOpen != 0)
        {
            if (transform.position == startPosition)
            {
                yield return new WaitForSecondsRealtime(holdTimeClosed);
            }
            else
            {
                yield return new WaitForSecondsRealtime(holdTimeOpen);
            }
        }
        StartCoroutine(LerpValue(end, start));
    }
}
