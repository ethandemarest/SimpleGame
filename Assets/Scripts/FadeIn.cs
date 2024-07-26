using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    SpriteRenderer sp;
    TriggerScript ts;

    float opacity = 0;
    float changeValue = 0.05f;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        ts = GetComponent<TriggerScript>();
    }

    private void Update()
    {
        sp.color = new Color(1f, 1f, 1f, opacity);

        if (ts.triggered)
        {
            if(opacity < 1)
            {
                opacity += changeValue;
            }
        }
        else
        {
            if(opacity > 0f)
            {
                opacity -= changeValue;
            }
        }
    }
}
