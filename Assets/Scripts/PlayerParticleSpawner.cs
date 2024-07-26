using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleSpawner : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController pc;
    GameObject fxLayer;
    public GameObject smoke;
    public GameObject launch;
    float spawnTimer;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        fxLayer = GameObject.Find("FX");
        pc = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        spawnTimer += Time.deltaTime;
        angle = Random.Range(0f, 360);


        //RUNNING
        if (pc.grounded && pc.movement.x != 0)
        {
            if(spawnTimer >= 0.1 && pc.dashing == false)
            {
                SpawnSmoke(transform.position, new Vector3(0f, 0f, angle));
                spawnTimer = 0;
            }
        }

        //DASHING
        if (spawnTimer >= 0.05f && pc.dashing)
        {
            if (pc.grounded)
            {
                SpawnSmoke(transform.position, new Vector3(0f, 0f, angle));
                spawnTimer = 0;
            }
            if (!pc.grounded)
            {
                SpawnSmoke(transform.position + new Vector3(0f,0.5f), new Vector3(0f, 0f, angle));
                spawnTimer = 0;
            }
        }
    }
    public void WallSlide()
    {
        if (spawnTimer >= 0.3f)
        {
            SpawnSmoke(transform.position, new Vector3(0f, 0f, angle));
        }
    }
    public void Jump(Vector3 spawnPosition, Vector3 angle)
    {
        var newLaunch = Instantiate(launch, spawnPosition, Quaternion.Euler(angle));
        newLaunch.transform.parent = fxLayer.transform;
    }
    void SpawnSmoke(Vector3 spawnPosition, Vector3 angle)
    {
        var newSmoke = Instantiate(smoke, spawnPosition, Quaternion.Euler(angle));
        newSmoke.transform.parent = fxLayer.transform;
    }
}
