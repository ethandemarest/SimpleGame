using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroFade : MonoBehaviour
{
    public GameObject fadeOut;
    public string levelName;
    public float animationTime;
    public float startDelay;
    public float onScreenTime;
    float opacity;

    bool canSkip;
    public AnimationCurve animCurve;
    SpriteRenderer sp;
    public Sprite[] sprites;

    public int currentImg;
    private void Start()
    {
        fadeOut.SetActive(true);
        fadeOut.GetComponent<SpriteRenderer>().color = Color.black;
        canSkip = true;
        sp = GetComponent<SpriteRenderer>();
        sp.color = Color.clear;
        StartCoroutine(StartDelay());
    }

    private void Update()
    {
        sp.color = new Color(1f, 1f, 1f, opacity);

        if (Input.anyKeyDown && canSkip)
        {
            StopAllCoroutines();
            NextImage();
        }
    }
    void NextImage()
    {
        sp.color = Color.clear;
        currentImg++;
        if(currentImg >= 3)
        {
            canSkip = false;
            StartCoroutine(StartLevel());
        }
        else
        {
            StartCoroutine(FadeIn());
            sp.sprite = sprites[currentImg];
        }

    }
    public IEnumerator StartDelay()
    {
        float fadeOpac;
        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            fadeOpac = Mathf.Lerp(1f, 0f, t); ;
            fadeOut.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, fadeOpac);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(startDelay);

        StartCoroutine(FadeIn());
    }
    IEnumerator StartLevel()
    {
        float fadeOpac;
        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            fadeOpac = Mathf.Lerp(0f, 1f, t); ;
            fadeOut.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, fadeOpac);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        SceneManager.LoadScene(levelName);

    }
    public IEnumerator FadeIn()
    {
        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            opacity = Mathf.Lerp(0f, 1f, t); ;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(onScreenTime);

        float timeElapsed2 = 0;
        while (timeElapsed2 < animationTime)
        {
            float t = timeElapsed2 / animationTime;
            t = animCurve.Evaluate(t);
            opacity = Mathf.Lerp(1f, 0f, t); ;
            timeElapsed2 += Time.deltaTime;
            yield return null;
        }
        NextImage();
    }
}
