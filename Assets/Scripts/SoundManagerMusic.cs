using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManagerMusic : MonoBehaviour
{
    public AnimationCurve animCurve;
    public AnimationCurve animCurve2;
    public AnimationCurve animCurve3;

    public float cutOffTime;
    public float volume;
    float startVolume;
    AudioSource audioSource;
    public AudioClip song;
    AudioLowPassFilter lpf;
    AudioReverbFilter reverb;

    bool musicOn;

    private void Start()
    {
        musicOn = true;
        startVolume = volume;
        audioSource = GetComponent<AudioSource>();
        lpf = GetComponent<AudioLowPassFilter>();
        reverb = GetComponent<AudioReverbFilter>();
        audioSource.clip = song;
    }
    private void Update()
    {
        audioSource.volume = volume;
    }

    public void ToggleMute()
    {
        if (musicOn)
        {
            audioSource.mute = enabled;
            musicOn = false;
        }
        else
        {
            audioSource.mute = !audioSource.mute;
            musicOn = true;
        }
    }   
    public void PlaySong()
    {
        audioSource.Play();
    }
    public void PauseScreen(float start, float end)
    {
        StartCoroutine(LowPassPause(start, end));
    }
    public void Compress(float compressionTime, float compressionAmount)
    {
        StartCoroutine(StartCompress(compressionTime, compressionAmount));
    }
    public void LowPass(float filterTime, float cutOff)
    {
        StartCoroutine(StartLowPass(filterTime, cutOff));
    }
    public void PitchBend()
    {
        StartCoroutine(StartPitch());
    }
    public void Reverb(float reverbTime)
    {
        StartCoroutine(StartReverb(reverbTime));
    }
    IEnumerator LowPassPause(float start, float end)
    {
        float timeElapsed = 0;
        while (timeElapsed < 0.6f)
        {
            float t = timeElapsed / 0.6f;
            t = animCurve3.Evaluate(t);
            lpf.cutoffFrequency = Mathf.Lerp(start, end, t);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        lpf.cutoffFrequency = end;
    }

    IEnumerator StartCompress(float compressionTime, float compressionAmount)
    {
        float timeElapsed = 0;
        while (timeElapsed < compressionTime)
        {
            float t = timeElapsed / compressionTime;
            t = animCurve.Evaluate(t);
            volume = Mathf.Lerp(startVolume*100, compressionAmount, t)/100;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        volume = startVolume;
    }
    IEnumerator StartLowPass(float filterTime, float cutOff)
    {
        float timeElapsed = 0;
        while (timeElapsed < filterTime)
        {
            float t = timeElapsed / filterTime;
            t = animCurve2.Evaluate(t);
            lpf.cutoffFrequency = Mathf.Lerp(22000, cutOff, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        lpf.cutoffFrequency = 22000;
    }
    IEnumerator StartPitch()
    {
        float timeElapsed = 0;
        while (timeElapsed < cutOffTime)
        {
            float t = timeElapsed / cutOffTime;
            t = animCurve.Evaluate(t);
            audioSource.pitch = Mathf.Lerp(1, -0.9f, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        audioSource.pitch = 1;
    }
    IEnumerator StartReverb(float reverbTime)
    {
        float timeElapsed = 0;
        while (timeElapsed < reverbTime)
        {
            float t = timeElapsed / reverbTime;
            t = animCurve.Evaluate(t);
            reverb.decayTime = Mathf.Lerp(0.1f, 3f, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        reverb.decayTime = 0.1f;
    }
}