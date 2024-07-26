using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    public AudioClip[] missleSound;

    private void Awake()
    {
        int randomVal = Random.Range(0, 3);
        GetComponent<AudioSource>().PlayOneShot(missleSound[randomVal]);
        GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.08f, 0.08f);
    }
}
