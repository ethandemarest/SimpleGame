using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFade : MonoBehaviour
{
    SpriteRenderer sp;
    public float animationTime;
    public AnimationCurve animCurve;
    bool hasBeenTriggered;
    public bool fadeIn;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        if (fadeIn)
        {
            sp.color = Color.clear;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenTriggered)
        {
            if (fadeIn)
            {
                StartCoroutine(Fade(0f,1f));
            }
            else if (!fadeIn)
            {
                StartCoroutine(Fade(1f, 0f));
            }
            hasBeenTriggered = true;
        }
    }
    public IEnumerator Fade(float startCol, float endCol)
    {
        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            float opacity = Mathf.Lerp(startCol, endCol, t); ;
            sp.color = new Color(1f, 1f, 1f, opacity);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sp.color = new Color(1f, 1f, 1f, endCol);
    }

    public void TriggerFade()
    {
        StartCoroutine(Fade(0f, 1f));
    }
}
