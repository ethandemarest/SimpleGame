using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    GameObject effectsLayer;
    public GameObject wind;
    public float spawnDelay;

    public float xRange;
    public float yRange;

    Vector3 SpawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        effectsLayer = GameObject.Find("FX");
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float randomX = transform.parent.position.x + Random.Range(-xRange, xRange);
        float randomY = transform.parent.position.y + Random.Range(-yRange, yRange);
        float randomZ = Random.Range(-10, 10);
        SpawnLocation = new Vector3(randomX, randomY, randomZ);
    
        yield return new WaitForSeconds(spawnDelay);
        
        var newWind = Instantiate(wind, SpawnLocation, Quaternion.Euler(Vector3.zero));
        newWind.transform.parent = effectsLayer.transform;

        StartCoroutine(Spawn());
    }
}
