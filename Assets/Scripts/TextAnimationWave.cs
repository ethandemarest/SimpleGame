using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimationWave : MonoBehaviour
{
    public float _frequency = 4f;
    public float _amplitude = 1f;
    public float _extraOffset;
    float offset;
    float posY;

    Vector3 startPos;
    private void Awake()
    {
        offset = Time.realtimeSinceStartup;
        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        float x = transform.position.x;
        float y = Mathf.Sin(Time.realtimeSinceStartup * _frequency - offset + _extraOffset) * _amplitude;
        float z = transform.position.z;

        posY = transform.position.y;

        transform.position = new Vector3(x, startPos.y + y, z);
    }
}
