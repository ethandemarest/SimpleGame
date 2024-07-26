using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeIn : MonoBehaviour
{
    public float animationTime;
    public AnimationCurve animCurve;
    SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            float opacity = Mathf.Lerp(1f, 0f, t); ;
            sp.color = new Color(0f, 0f, 0f, opacity);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sp.color = new Color(0f, 0f, 0f, 0f);
        Destroy(gameObject);
    }
}
