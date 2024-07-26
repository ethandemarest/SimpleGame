using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    Rigidbody2D playerRB;
    public GameObject player;
    private Vector3 velVec = Vector3.zero;
    private float lastMove;
    public Vector3 offset;
    private Vector3 playerVel;
    public Vector3 targetPosition;

    private float smoothTime = 0.5f;
    public float rbFactorX;
    public float rbFactorY;
    bool playerFound;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerRB = player.GetComponent<Rigidbody2D>();
            playerFound = true;
        }
    }

    private void FixedUpdate()
    {
        if (playerFound)
        {
            float motionX = playerRB.velocity.x * rbFactorX;
            float motionY = playerRB.velocity.y * rbFactorY;
            lastMove = player.GetComponent<PlayerInput>().lastMove;
            playerVel = new Vector3(motionX, motionY);

            targetPosition = player.transform.position + playerVel + new Vector3(lastMove * 3, 0) + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velVec, smoothTime);
        }
        else
        {
            Debug.Log("Camera cannot find Game Object with tag 'Player'");
        }
    }
}
