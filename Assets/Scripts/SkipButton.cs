using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    AudioSource audioSource;
    public float speed;
    public float duration;
    public float distance;
    Vector2 startPos;
    public AnimationCurve animCurve;
    float opacity;

    SpriteRenderer sp;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.color = Color.clear;
        StartCoroutine(FadeIn());
        startPos = transform.position;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        sp.color = new Color(1f, 1f, 1f, opacity);
        if (Input.anyKeyDown)
        {
            StopCoroutine(WiggleRoutine());
            transform.position = startPos;
            StartCoroutine(WiggleRoutine());
        }
    }
    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(2f);
        float timeElapsed = 0;
        while (timeElapsed < 1f)
        {
            float t = timeElapsed / 1f;
            t = animCurve.Evaluate(t);
            opacity = Mathf.Lerp(0f, 1f, t); ;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator WiggleRoutine()
    {
        audioSource.Play();
        float sinVal;
        float time = 0f;

        while (time < duration)
        {
            sinVal = Mathf.Sin(time * speed) * distance;
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y + sinVal), startPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
    }


}
