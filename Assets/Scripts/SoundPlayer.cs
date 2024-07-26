﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    public float volume;
    public AudioClip footstepOne;
    public AudioClip footstepTwo;
    public AudioClip jump;
    public AudioClip jump2;
    public AudioClip jump3;
    public AudioClip dash;
    public AudioClip land;
    public AudioClip explosion;

    public AudioClip crowdAww;

    AudioSource audioSource;

    SoundManagerMusic soundManagerMusic;

    private void Start()
    {
        audioSource = GameObject.Find("Camera Holder").GetComponent<AudioSource>();
        soundManagerMusic = GameObject.Find("Music Manager").GetComponent<SoundManagerMusic>();
    }
    public void psFootStepOne()
    {
        audioSource.PlayOneShot(footstepOne, 1f * volume);
        RandomPitch();
    }
    public void psFootStepTwo()
    {
        audioSource.PlayOneShot(footstepTwo, 1f * volume);
        RandomPitch();
    }
    public void psJump()
    {
        audioSource.PlayOneShot(jump, 1f * volume);
        RandomPitch();

    }
    public void psJump2()
    {
        float randomVal = Random.Range(0f, 10);
        if(randomVal >= 8)
        {
            audioSource.PlayOneShot(jump3, 1f * volume);
        }
        else
        {
            audioSource.PlayOneShot(jump2, 1f * volume);
        }
        RandomPitch();
    }
    public void psDash()
    {
        audioSource.PlayOneShot(dash, 1f * volume);
    }
    public void psLand()
    {
        audioSource.PlayOneShot(land, 1f * volume);
    }
    public void psExplosion()
    {
        audioSource.PlayOneShot(crowdAww, 0.5f * volume);
        audioSource.PlayOneShot(explosion, 2f * volume);

        RandomPitch();
        //soundManagerMusic.GetComponent<AudioSource>().Pause();
        soundManagerMusic.Compress(1.5f,50);
        soundManagerMusic.LowPass(2f,500);
        soundManagerMusic.PitchBend(); 
    }
    void RandomPitch()
    {
        audioSource.pitch = 1 + Random.Range(-0.05f, 0.05f);
    }

    //WORKS FOR ALL SOUNDS EXCLUDING RESET & DEATH
    IEnumerator DelaySound(AudioClip audioClip, float clipVolume, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        audioSource.PlayOneShot(audioClip, clipVolume * volume);
    }
}
