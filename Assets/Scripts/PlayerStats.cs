using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float speed;
    public float groundInertia;
    public float airInertia;
    public float dashInertia;
    public float groundAcceleration;
    public float airAcceleration;
    public float gravity;
    public float maxJumpSpeed;
    public float maxFallSpeed;
    public float wallSlideSpeed;
    public float jumpSpeed = 25f;
    public float dashTime;
    public float dashSpeed = 45f;

    public float throwSpeed;
}