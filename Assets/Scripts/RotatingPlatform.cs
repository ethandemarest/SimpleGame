using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public GameObject[] rotatingPlatforms;

    public AnimationCurve animCurve;
    public AudioClip rotateSound;

    private float delayTimer;
    public float delayTime;
    public float rotater = 0f;
    public float animationTime;

    bool holding = false;

    private void Start()
    {
        rotatingPlatforms = GameObject.FindGameObjectsWithTag("Rotating Object");

        StartCoroutine(LerpPosition(0f, 90f));
    }

    private void FixedUpdate()
    {
        //HOLD TIMER
        if (holding && delayTimer < delayTime)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= delayTime)
            {
                holding = false;
                StartCoroutine(LerpPosition(rotater, rotater + 90f));
                delayTimer = 0.0f;
            }
        }

        foreach (GameObject gameObject in rotatingPlatforms)
        {
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rotater);
        }
    }

    IEnumerator LerpPosition(float start, float end)
    {
        foreach (GameObject gameObject in rotatingPlatforms)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(rotateSound);
            gameObject.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.05f, 0.05f);
        }

        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            rotater = Mathf.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        rotater = end;
        holding = true;
        if(rotater == 360)
        {
            rotater = 0f;
        }
    }
}
