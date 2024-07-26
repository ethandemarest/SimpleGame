using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebug : MonoBehaviour
{
    PlayerController pc;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
    }

    void Update()
    {
        //LAST MOVE
        Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), new Vector3(pc.lastMove, 0f) * 2, Color.magenta);
        //GROUND DETECTION
        Debug.DrawRay(transform.position + new Vector3(pc.groundDetectionOffset, 0f, 0f), Vector3.down * pc.groundDetectionRange, Color.red);
        //GROUND DETECTION
        Debug.DrawRay(transform.position + new Vector3(-pc.groundDetectionOffset, 0f, 0f), Vector3.down * pc.groundDetectionRange, Color.green);
        //WALL JUMP DIRECTION
        Debug.DrawRay(transform.position, pc.launchDir * 3, Color.black);
    }
}
