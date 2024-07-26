using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissle : MonoBehaviour
{
    public GameObject[] cannons;
    public GameObject missle;

    public bool shootingComplete;

    float angle;

    private void Update()
    {
        if(transform.localScale.x > 0)
        {
            angle = 090;
        }
        if(transform.localScale.x < 0)
        {
            angle = -90f;
        }
    }
    public void Fire(int cannonNum)
    {
        Instantiate(missle, cannons[cannonNum].transform.position, Quaternion.Euler(0f, 0f, angle));
    }
}