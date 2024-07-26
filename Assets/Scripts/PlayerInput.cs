using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float lastMoveThreshold;
    public Vector2 movement;
    public float lastMove;
    public bool jump;
    public bool dash;

    private void Start()
    {
        lastMove = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //INPUT
        movement.x = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
        dash = Input.GetButtonDown("Dash");

        //LAST MOVE
        if (movement.x > lastMoveThreshold || movement.x < -lastMoveThreshold)
        {
            lastMove = movement.normalized.x;
        }
    }

}   
