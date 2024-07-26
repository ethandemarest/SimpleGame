using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wiggleObject : MonoBehaviour
{
    float speed = 40f;
    float duration = 0.5f;
    SpriteRenderer sp;
    Vector3 startPos;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    public void StartWiggle(float distance)
    {
        StopAllCoroutines();
        StartCoroutine(WiggleRoutine(distance));
    }

    IEnumerator WiggleRoutine(float distance)
    {
        transform.position = startPos;
        float sinVal;
        float time = 0f;

        while (time < duration)
        {
            sinVal = Mathf.Sin(time * speed) * distance;
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y + sinVal, transform.position.z), startPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
    }
}