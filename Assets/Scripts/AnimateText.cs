using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateText : MonoBehaviour
{
    public float animationTime;
    public AnimationCurve animCurve;

    public void AnimateSize()
    {
        StopAllCoroutines();
        StartCoroutine(LerpSize(Vector3.zero, Vector3.one));
    }
    public void AnimatePosition(Vector3 startPos, Vector3 endPos)
    {
        StopAllCoroutines();
        StartCoroutine(LerpPosition(startPos, endPos));
    }

    IEnumerator LerpSize(Vector3 start, Vector3 end)
    {
        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            transform.localScale = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = end;
    }
    IEnumerator LerpPosition(Vector3 start, Vector3 end)
    {
        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        GetComponent<RectTransform>().anchoredPosition = end;
    }

}
