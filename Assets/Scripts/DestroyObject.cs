using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Spin"))
        {
            Destroy(gameObject);
        }
    }
}
