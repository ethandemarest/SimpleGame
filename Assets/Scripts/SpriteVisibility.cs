using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteVisibility : MonoBehaviour
{
    MasterDebug masterDebug;
    SpriteRenderer sp;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        masterDebug = GameObject.Find("MasterDebug").GetComponent<MasterDebug>();
        if (masterDebug.showDebugSprites == true)
        {
            sp = GetComponent<SpriteRenderer>();
            sp.enabled = true;
        }
        else
        {
            sp.enabled = false;
        }
    }
}
