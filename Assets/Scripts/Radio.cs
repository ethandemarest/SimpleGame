using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public void ToggleMusic(bool status)
    {
        GetComponent<AudioSource>().enabled = status;
    }
}
