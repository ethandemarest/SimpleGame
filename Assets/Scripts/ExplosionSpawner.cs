using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    GameObject effectsLayer;
    public GameObject explosion;

    private void Start()
    {
        effectsLayer = GameObject.Find("FX");
    }
    public void Spawn(Vector3 spawnRot)
    {
        if(spawnRot.x > 0)
        {
            var newExplosion = Instantiate(explosion, transform.position, Quaternion.Euler(Vector3.zero));
            newExplosion.transform.parent = effectsLayer.transform;
        }
        if (spawnRot.x < 0)
        {
            var newExplosion = Instantiate(explosion, transform.position, Quaternion.Euler(0f,180f,0f));
            newExplosion.transform.parent = effectsLayer.transform;
        }
    }
}
