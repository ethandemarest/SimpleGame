using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionWave : MonoBehaviour
{
    GameObject effectsLayer;
    public GameObject explosion;
    public GameObject spawnerL;
    public GameObject spawnerR;

    public float distance;
    float lerpDuration = 0.4f;
    float currentPositionR;
    float currentPositionL;
    float timeElapsed = 0;

    void Start()
    {
        effectsLayer = GameObject.Find("FX");
        StartCoroutine(Lerp());
    }

    IEnumerator Lerp()
    {
        StartCoroutine(SpawnFire());

        float distanceR = spawnerR.transform.localPosition.x + distance;
        float distanceL = spawnerL.transform.localPosition.x - distance;

        while (timeElapsed < lerpDuration)
        {
            currentPositionR = Mathf.Lerp(0, distanceR, timeElapsed / lerpDuration);
            currentPositionL = Mathf.Lerp(0, distanceL, timeElapsed / lerpDuration);

            timeElapsed += Time.deltaTime;

            Vector2 movementR = new Vector2(currentPositionR, spawnerR.transform.localPosition.y);
            Vector2 movementL = new Vector2(currentPositionL, spawnerL.transform.localPosition.y);

            spawnerR.transform.localPosition = movementR;
            spawnerL.transform.localPosition = movementL;

            yield return null;
        }

        Destroy(gameObject);
    }
    IEnumerator SpawnFire()
    {
        var fireR = Instantiate(explosion, spawnerR.transform.position, Quaternion.Euler(0f, 0f, 0f));
        var fireL = Instantiate(explosion, spawnerL.transform.position, Quaternion.Euler(0f, 180f, 0f));

        fireR.transform.parent = effectsLayer.transform;
        fireL.transform.parent = effectsLayer.transform;

        yield return new WaitForSeconds(0.04f);

        if(timeElapsed < lerpDuration)
        {
            StartCoroutine(SpawnFire());
        }
    }
}
