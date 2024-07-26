using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    public float delay;
    private void Start()
    {
        StartCoroutine(DelayedDestroy());
    }
    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
