using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManagerEnemy1 : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip footstepTwo;
    public AudioClip jump;
    public AudioClip dash;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void psFootStepOne()
    {
        audioSource.PlayOneShot(clip);
    }

}
