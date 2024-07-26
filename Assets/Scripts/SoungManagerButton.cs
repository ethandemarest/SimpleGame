using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoungManagerButton : MonoBehaviour
{
    public AudioClip press;


    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void psPress()
    {
        audioSource.PlayOneShot(press);
    }
}