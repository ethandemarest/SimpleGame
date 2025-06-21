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
        Debug.DrawRay(transform.position + new Vector3(pc.groundCastWidth, 0f), Vector2.down * pc.groundDetectionRange, Color.red);
        Debug.DrawRay(transform.position + new Vector3(-pc.groundCastWidth, 0f), Vector2.down * pc.groundDetectionRange, Color.red);

        //HANDS

        Debug.DrawRay(transform.position + pc.handsOffset, new Vector3(pc.lastMove, 0f) * pc.grabRange, Color.yellow);
    }
}
