using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathAnimations : MonoBehaviour
{
    public AnimationCurve wiggleCurve;
    public AnimationCurve waveDown;

    public Coroutine wiggleCoroutineY;
    public Coroutine waveDownCoroutine;

    public float animationStrength;


    public void wiggleY(float animTime)
    {

        if (wiggleCoroutineY != null)
        {
            StopCoroutine(wiggleCoroutineY);
        }
        wiggleCoroutineY = StartCoroutine(WigglePos(transform.localPosition.y, animationStrength, animTime));
    }
    public void Wave(float animTime)
    {
        if (waveDownCoroutine != null)
        {
            StopCoroutine(waveDownCoroutine);
        }
        waveDownCoroutine = StartCoroutine(WaveDown(transform.localPosition.y, animationStrength, animTime));
    }
    IEnumerator WigglePos(float start, float end, float animTime)
    {
        float timeElapsed = 0;
        while (timeElapsed < animTime)
        {
            float t = timeElapsed / animTime;
            t = wiggleCurve.Evaluate(t);
            float val = Mathf.Lerp(start, end, t);
            transform.localPosition = new Vector3(transform.localPosition.x, val, 0f);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = Vector3.zero;
    }
    IEnumerator WaveDown(float start, float end, float animTime)
    {
        float timeElapsed = 0;
        while (timeElapsed < animTime)
        {
            float t = timeElapsed / animTime;
            t = waveDown.Evaluate(t);
            float val = Mathf.Lerp(start, end, t);
            transform.localPosition = new Vector3(transform.localPosition.x, val, 0f);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = Vector3.zero;
    }
    IEnumerator WiggleScale(float start, float end, float animTime)
    {
        float timeElapsed = 0;
        while (timeElapsed < animTime)
        {
            float t = timeElapsed / animTime;
            t = wiggleCurve.Evaluate(t);
            float val = Mathf.Lerp(start, end, t);
            transform.localPosition = new Vector3(transform.localPosition.x, val, 0f);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = Vector3.zero;
    }
}
