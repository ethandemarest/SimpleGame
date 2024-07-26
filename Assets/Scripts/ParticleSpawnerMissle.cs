using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawnerMissle : MonoBehaviour
{
    GameObject effectsLayer;
    public GameObject particle;
    public GameObject exhaust;
    public float particleRate;

    private void Start()
    {
        effectsLayer = GameObject.Find("FX");
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        var newParticle = Instantiate(particle, exhaust.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        newParticle.transform.parent = effectsLayer.transform;
        yield return new WaitForSeconds(particleRate);
        StartCoroutine(Spawner());
    }
}
