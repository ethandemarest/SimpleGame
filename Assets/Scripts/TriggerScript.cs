using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public bool triggered;
    public float triggerDelay;

    bool timerDone;

    public void Trigger(bool activated)
    {
        StopAllCoroutines();
        StartCoroutine(Delay(activated));
        if (timerDone)
        {
            triggered = activated;
            timerDone = false;
        }
    }

    IEnumerator Delay(bool status)
    {
        yield return new WaitForSeconds(triggerDelay);
        
        timerDone = true;
        Trigger(status);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        triggered = false;
    }
}
