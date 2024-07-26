using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCamera : MonoBehaviour
{
    public bool playOnStart;
    public float duration;
    public AnimationCurve animCurve;

    Vector3 startPos;
    public Vector3 endPos;

    Vector3 startRot;
    public Vector3 endRot;
    // Start is called before the first frame update
    void Start()
    {
        if (playOnStart)
        {
            StartCoroutine(LerpPosition(startPos, endPos));
            StartCoroutine(LerpRotation(startRot, endRot));
        }
        startRot = transform.rotation.eulerAngles;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StopAllCoroutines();
            StartCoroutine(LerpPosition(transform.position, endPos));
            StartCoroutine(LerpRotation(startRot, endRot));
        }
    }

    IEnumerator LerpPosition(Vector3 start, Vector3 end)
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
    }
    IEnumerator LerpRotation(Vector3 start, Vector3 end)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = animCurve.Evaluate(t);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(start), Quaternion.Euler(end), t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.Euler(end);
    }
}
