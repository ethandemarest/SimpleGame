using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("||Vectors||")]
    PlayerStats stats;
    PlayerControls controls;

    //COMPONENTS
    Rigidbody2D rb;

    //SCRIPTS
    SpriteFlip spriteFlip;
    PlayerAnimator playerAnimator;

    public float groundDetectionOffset = 0.4f;
    public float groundDetectionRange = 0.01f;

    //VECTORS
    [Header("||Vectors||")]
    public Vector2 newVelocity;
    public Vector2 movement;
    public float lastMove;
    public Vector2 launchDir;

    public string groundName;

    [HideInInspector]
    public int jumpCount;
    [HideInInspector]
    public bool controlEnabled;
    [HideInInspector]
    public bool jump;
    public bool falling;
    [HideInInspector]
    public bool checkingGround = true;

    bool reset;
    bool hasDied;

    public bool inLauncher;

    [Header("||Bools||")]
    public bool grounded;
    public bool dashing;
    public bool launching;

    public RaycastHit2D facingWall;

    void Awake()
    {
        controls = new PlayerControls();
        stats = GetComponent<PlayerStats>();
        EnableControls();

        //MOVEMENT
        controls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
    }

    public void EnableControls()
    {
        controls.Gameplay.Enable();
        controlEnabled = true;
    }
    public void DisableControls()
    {
        controls.Gameplay.Disable();
        controlEnabled = false;
    }

    void Start()
    {
        spriteFlip = GetComponent<SpriteFlip>();
        playerAnimator = GetComponent<PlayerAnimator>();
        rb = GetComponent<Rigidbody2D>();

        lastMove = 1;
        grounded = false;
    }

    private void FixedUpdate()
    {
        //APPLY MOTION TO RIGIDBODY
        rb.velocity = newVelocity;

        // HORIZONTAL CONTROLS
        if (movement.x == 0)
        {
            //DECELERATE TO ZERO WITH NO INPUT
            float inertia = grounded ? stats.groundInertia : stats.airInertia;
            newVelocity.x = Mathf.MoveTowards(newVelocity.x, 0, inertia * Time.fixedDeltaTime);
        }
        else
        {
            //ACCELERATE TO MOVEMENT SPEED WHEN INPUTTING
            var acceleration = grounded ? stats.groundAcceleration : stats.airAcceleration;
            newVelocity.x = Mathf.MoveTowards(newVelocity.x, movement.x * stats.speed, acceleration * Time.fixedDeltaTime);
        }
        //VERTICAL CONTROLS
        if (grounded) //IDLE
        {
            newVelocity.y = Mathf.MoveTowards(newVelocity.y, -1.5f, 1000 * Time.fixedDeltaTime);
        }
        if (!grounded && !dashing && !falling) // JUMPING
        {
            newVelocity.y = Mathf.MoveTowards(newVelocity.y, -1f, stats.maxJumpSpeed * Time.fixedDeltaTime);
        }
        if (falling && !dashing) // FALLING
        {
            newVelocity.y = Mathf.MoveTowards(newVelocity.y, stats.gravity, stats.maxFallSpeed * Time.fixedDeltaTime);
        }
    }

    void Update()
    {
        //INPUT
        reset = controls.Gameplay.Reset.triggered;
        jump = controls.Gameplay.Jump.triggered;

        //JUMP
        if (jump && jumpCount <= 1)
        {
            Jump(stats.jumpSpeed);
        }

        //FALLING
        falling = newVelocity.y < 0 ? true : false;

        //LAST MOVE
        if (movement.x > 0.2 || movement.x < -0.2)
        {
            lastMove = movement.normalized.x;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            if (!hasDied)
            {
                hasDied = true;
                DestroyPlayer();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missle"))
        {
            if (!hasDied)
            {
                hasDied = true;
                DestroyPlayer();
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.parent = collision.gameObject.transform;
            ContactPoint2D[] contacts = collision.contacts;

            if (contacts[0].normal == Vector2.down || contacts[1].normal == Vector2.down) //CEILING
            {
                newVelocity = new Vector2(newVelocity.x, 0f);
            }
            else if (contacts[0].normal == Vector2.up || contacts[1].normal == Vector2.up) //GROUND
            {
                newVelocity = new Vector2(newVelocity.x, 0f);
            }
            else
            {
                newVelocity = new Vector2(0f, newVelocity.y);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && checkingGround) //GROUND CHECK
        {
            ContactPoint2D[] contacts = collision.contacts;

            if (contacts[0].normal == Vector2.up || contacts[1].normal == Vector2.up) //GROUND
            {
                JumpReset();
                grounded = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        LevelOut();
        if (!dashing)
        {
            Fall();
        }
    }
    void JumpReset()
    {
        jumpCount = 0;
    }

    void Fall()
    {
        playerAnimator.Fall();
    }
    void LevelOut()
    {
        spriteFlip.enabled = true;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    void Jump(float jumpForce)
    {
        jumpCount++;
        LevelOut();
        StopCoroutine(JumpDelay());
        StartCoroutine(JumpDelay());
        transform.parent = null;

        playerAnimator.Jump();
        newVelocity = new Vector2(rb.velocity.x, jumpForce);
        grounded = false;
    }
    IEnumerator JumpDelay()
    { 
        checkingGround = false;
        yield return new WaitForSecondsRealtime(0.2f);
        checkingGround = true;
    }
    
    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
} 