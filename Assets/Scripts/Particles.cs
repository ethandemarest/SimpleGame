using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    Vector2 positionLerp;
    Vector2 startPos;
    Vector2 targetPos;

    public float lerpDuration = 3;
    float startRotation = 0;
    float endRotation = 360;

    float startOpac = 1;
    float endOpac = 0;

    public float scaleLerp;
    public float rotationLerp;
    public float opactiyLerp;

    SpriteRenderer sp;
    
    void Start()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        startPos = transform.position;
        targetPos = new Vector2(transform.position.x + Random.Range(-3.0f, 3.0f), transform.position.y + Random.Range(-3.0f, 3.0f));
        StartCoroutine(Lerp());
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.localScale = new Vector3(scaleLerp,scaleLerp,1f);
        transform.position = positionLerp;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationLerp);
        sp.color = new Color(1, 1, 1, opactiyLerp);
    }

    IEnumerator Lerp()
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            timeElapsed += Time.deltaTime;

            scaleLerp = Mathf.Lerp(0.1f, 0.6f, timeElapsed / lerpDuration);
            positionLerp = Vector2.Lerp(startPos, targetPos, timeElapsed / lerpDuration);
            rotationLerp = Mathf.Lerp(startRotation, endRotation, timeElapsed / lerpDuration);
            opactiyLerp = Mathf.Lerp(startOpac, endOpac, timeElapsed / lerpDuration);
            
            yield return null;
        }

        Destroy(gameObject);
    }

}
