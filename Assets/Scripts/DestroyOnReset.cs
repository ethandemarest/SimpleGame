using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnReset : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Reset"))
        {
            Destroy(gameObject);
        }
    }
}
