using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerController pc;
    ParticleSpawner ps;
    SoundPlayer playerSound;
    bool hasFallen;
    public bool debugAnimation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
        playerSound = GetComponent<SoundPlayer>();
        ps = GetComponent<ParticleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!debugAnimation)//GROUND CHECK
        {
            animator.SetBool("Grounded", pc.grounded);
            
            if (pc.grounded)//RUNNING & IDLE
            {
                if (pc.movement.x >= 0.2 || pc.movement.x <= -0.2)
                {
                    animator.SetBool("Running", true);
                }
                else
                {
                    animator.SetBool("Running", false);
                }
            }
            else if (!pc.grounded) //FALLING
            {
                if (pc.falling && !pc.grounded && !hasFallen)
                {
                    Fall();
                }
            }
        }
    }
    public void Dash()
    {
        CameraShaker.Instance.ShakeOnce(5f, 5f, 0.1f, 1f);
        playerSound.psDash();
        ps.Dash();
        animator.SetBool("Boost", true);
    }
    public void Jump()
    {
        if(pc.grounded)
        {
            animator.SetBool("Jump", true);
            playerSound.psJump();
            ps.Jump();
        }
        else if(!pc.grounded)
        {
            animator.SetBool("JumpTwo", true);
            playerSound.psJump2();
            ps.Dust();
        }
    }
    public void Fall()
    {
        animator.SetTrigger("Fall");
        hasFallen = true;
    }
    public void OnWall()
    {
        animator.SetBool("Wall Grab", true);
        playerSound.psFootStepTwo();
    }

    public void Death()
    {
        CameraShaker.Instance.ShakeOnce(10f, 5f, 0.1f, 2f);
        if (playerSound)
        {
            playerSound.psExplosion();
        }
    }
}
