using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float stopTime;

    public void Stop()
    {
        StartCoroutine(TimeFreeze());
    }

    IEnumerator TimeFreeze()
    {
        Time.timeScale = 0f;

        float timeElapsed = 0;
        while (timeElapsed < stopTime)
        {
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1f;
    }
}
