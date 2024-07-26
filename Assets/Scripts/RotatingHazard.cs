using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingHazard : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        transform.Rotate(direction * speed * Time.deltaTime);
    }
}
