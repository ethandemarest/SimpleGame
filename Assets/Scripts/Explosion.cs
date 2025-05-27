using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float timeAlive;
    public float timer;
    public float force;

    void Awake()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeAlive)
        {
            GetComponent<CircleCollider2D>().enabled = false;   
        }
        if(timer >= 2f)
        {
            Destroy(gameObject);
        }
    }
}
