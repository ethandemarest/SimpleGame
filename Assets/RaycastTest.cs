using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    public float raycastDistance;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, raycastDistance);
        Debug.DrawRay(transform.position, Vector2.right, Color.green, raycastDistance);

        if (hit)
        {
            print("hit");
        }
    }
}
