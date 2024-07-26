using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePlatform : MonoBehaviour
{
    public bool invert; 
    float flow;
    float rotateVal;
    public float speed;
    public float amplitude;
    Vector2 startPos;
    public float rotateAmount;
    public float offset;

    public bool rotate;


    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, flow, transform.position.z);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotateVal);
    }
    private void FixedUpdate()
    {
        if (invert)
        {
            flow = Mathf.Sin((Time.time + offset) * speed) * -amplitude + startPos.y;
        }
        else
        {
            flow = Mathf.Sin((Time.time + offset) * speed) * amplitude + startPos.y;
        }
        if (rotate)
        {
            rotateVal = Mathf.Cos((Time.time + offset) * speed) * rotateAmount;
        }
    }
}
