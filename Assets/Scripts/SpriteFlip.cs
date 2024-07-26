using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{
    PlayerController pc;
    SpriteRenderer sp;
    public string direcition;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (pc.lastMove >= 0.2f)
        {
            sp.flipX = false;
            direcition = "facing right";
        }
        else if (pc.lastMove <= -0.2f)
        {
            sp.flipX = true;
            direcition = "facing left";
        }
    }
}