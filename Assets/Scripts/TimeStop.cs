using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    Coroutine timeCoroutine;

    public void Stop(float duration)
    {
        if(timeCoroutine != null)
        {
            StopCoroutine(timeCoroutine);
        }
        timeCoroutine = StartCoroutine(RunStopTime(duration));
    }

    IEnumerator RunStopTime(float stopDuration)
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Important for physics

        yield return new WaitForSecondsRealtime(stopDuration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f; // Re
    }

}
