using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    PlayerController pc;
    GameObject effectsLayer;

    public GameObject poof;
    public GameObject dash;
    public GameObject line;

    Vector3 chestSpawn;
    Vector3 lastMove;
    Vector3 lastMoveInvert;
    Vector3 offset = new Vector3(0f, 0.8f);

    public float particleRate;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        effectsLayer = GameObject.Find("FX");
        chestSpawn = transform.position + new Vector3(0f, 1f);
    }

    private void Update()
    {
        chestSpawn = transform.position + offset;

        if (pc.lastMove == 1)
        {
            lastMove = new Vector2(1, 1f);
            lastMoveInvert = new Vector2(1, -180f);
        }
        else
        {
            lastMove = new Vector2(1, -180f);
            lastMoveInvert = new Vector2(1, 1f);
        }
    }

    public void Jump()
    {
        StopAllCoroutines();
        var newParticle = Instantiate(dash, transform.position, Quaternion.Euler(lastMove));
        newParticle.transform.parent = effectsLayer.transform;
    }
    public void WallJump(Vector2 launchDir)
    {
        if (launchDir.x == 1)
        {
            var newParticle = Instantiate(dash, transform.position + new Vector3(-launchDir.x/2, 1), Quaternion.Euler(new Vector3(180f, 0f, -90f)));
            newParticle.transform.parent = effectsLayer.transform;
        }
        if (launchDir.x == -1)
        {
            var newParticle = Instantiate(dash, transform.position + new Vector3(-launchDir.x/2,  1), Quaternion.Euler(new Vector3(0f, 0f, 90f)));
            newParticle.transform.parent = effectsLayer.transform;
        }
    }
    public void Dust()
    {
        StopAllCoroutines();
        var newParticle = Instantiate(poof, transform.position, new Quaternion());
        newParticle.transform.parent = effectsLayer.transform;
    }
    public void Dash()
    {
        StopAllCoroutines();
        StartCoroutine(GroundBoost());
    }

    public void WallGrab()
    {
        StopAllCoroutines();
        StartCoroutine(WallSlide());
    }

    public void Line()
    {
        Vector3 offset = new Vector3(0f, 1f, 0f);
        var newParticle = Instantiate(line, transform.position + offset, new Quaternion());
        newParticle.transform.parent = effectsLayer.transform;
    }

    public IEnumerator GroundBoost()
    {
        float currentTime = 0.0f;
        float spawnDelay = 1f;

        while (currentTime <= 0.4f)
        {
            spawnDelay += Time.deltaTime;
            currentTime += Time.deltaTime;

            if (spawnDelay >= 0.1f)
            {
                if (pc.grounded)
                {
                    var newParticle = Instantiate(dash, transform.position, Quaternion.Euler(lastMoveInvert));
                    newParticle.transform.parent = effectsLayer.transform;
                }
                else
                {
                    var newParticle = Instantiate(poof, chestSpawn, Quaternion.Euler(lastMoveInvert));
                    newParticle.transform.parent = effectsLayer.transform;
                }
                spawnDelay = 0.0f;
            }
            yield return null;
        }
    }
    public IEnumerator WallSlide()
    {
        float currentTime = 0.0f;
        float spawnDelay = 1f;

        while (currentTime <= 0.3f)
        {
            float randomAngle = Random.Range(0f,360f);

            spawnDelay += Time.deltaTime;
            currentTime += Time.deltaTime;

            if (spawnDelay >= 0.1f)
            {
                var newParticle = Instantiate(poof, chestSpawn + new Vector3(pc.lastMove * 0.3f, 0), Quaternion.Euler(0f,0f, randomAngle));
                newParticle.transform.parent = effectsLayer.transform;
                spawnDelay = 0.0f;
            }
            yield return null;
        }
    }

}
    