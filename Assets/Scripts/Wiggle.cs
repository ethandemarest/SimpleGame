using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    float speed = 40f;
    float duration = 0.5f;

    Vector2 startPos;
    RectTransform rt;
    private void Start()
    {
        rt = GetComponent<RectTransform>();
        startPos = rt.anchoredPosition;
    }

    public void StartWiggle(float distance)
    {
        StopAllCoroutines();
        StartCoroutine(WiggleRoutine(distance));
    }

    IEnumerator WiggleRoutine(float distance)
    {
        rt.anchoredPosition = startPos;
        float sinVal;
        float time = 0f;

        while (time < duration)
        {
            sinVal = Mathf.Sin(time * speed) * distance;
            rt.anchoredPosition = Vector3.Lerp(new Vector3(rt.anchoredPosition.x, rt.anchoredPosition.y + sinVal), startPos, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        rt.anchoredPosition = startPos;
    }
}
