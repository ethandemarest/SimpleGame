using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("||Vectors||")]
    PlayerStats stats;
    PlayerControls controls;

    public AnimationCurve animCurve;
    public float duration;

    public GameObject currentHeldObj;

    //COMPONENTS
    GameObject ObjectInReach;
    public GameObject hands;
    public GameObject attackZone;
    Rigidbody2D rb;

    //SCRIPTS
    SpriteFlip spriteFlip;
    PlayerAnimator playerAnimator;

    //RAYCASTS
    public RaycastHit2D groundHit;
    public RaycastHit2D foodHit;
    public LayerMask groundMask;
    public LayerMask foodMask;
    public float groundCastWidth;
    public float groundDetectionRange = 0.01f;
    public float foodRange;
    public Vector3 handsOffset;

    //VECTORS
    [Header("||Vectors||")]
    public Vector2 newVelocity;
    public Vector2 movement;
    public float lastMove;

    public int jumpCount;
    [HideInInspector]
    public bool controlEnabled;
    [HideInInspector]
    public bool grab;
    public bool jump;
    bool attack;
    public bool falling;
    [HideInInspector]
    public bool checkingGround = true;
    public bool canGrab = true;
    bool canAttack = true;

    bool hasDied;
    public bool inLauncher;

    [Header("||Bools||")]
    public bool grounded;

    public Transform platform;
    private Vector3 lastPlatformPos;
    private bool isOnPlatform = false;

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

    void Update()
    {
        //INPUT
        jump = controls.Gameplay.Jump.triggered;
        grab = controls.Gameplay.Dash.triggered;
        attack = controls.Gameplay.Attack.triggered;


        attackZone.transform.position = transform.position + new Vector3(lastMove*1.5f, 0) + new Vector3(0f,1f,0f);
        groundHit = Physics2D.BoxCast(transform.position, new Vector2(groundCastWidth,1), 0f, Vector2.down, groundDetectionRange, groundMask);
        foodHit = Physics2D.Raycast(transform.position + new Vector3(0f,1f), new Vector3(lastMove, 0f) * foodRange, 2, foodMask);

        //PICK UP ITEM
        if (!currentHeldObj && grab && canGrab && foodHit)
        {
            StartCoroutine(ThrowDelay());
            currentHeldObj = foodHit.transform.gameObject;
            currentHeldObj.GetComponent<ExplosiveObject>().PickUp(hands.transform);
        }
        //DROP ITEM
        if (currentHeldObj && grab && canGrab)
        {
            currentHeldObj.GetComponent<ExplosiveObject>().Drop();
            StartCoroutine(ThrowDelay());
            currentHeldObj = null;
        }

        if (attack)
        {
            //SWORD SWING
            if (!currentHeldObj && canAttack)
            {
                Vector2 attackDir = new Vector2(lastMove, 1);
                attackZone.GetComponent<AttackBox>().Attack(attackDir);
                StartCoroutine(AttackDelay());
                newVelocity = new Vector2(-lastMove * 15, 0f);
            }

            //THROW ITEM
            if (currentHeldObj && canGrab)
            {
                StartCoroutine(ThrowDelay());
                currentHeldObj.GetComponent<ExplosiveObject>().Throw(new Vector2(lastMove, 0.5f) * stats.throwSpeed);
                if (currentHeldObj.GetComponent<ExplosiveObject>() != null)
                {
                    currentHeldObj.GetComponent<ExplosiveObject>().PrimeExplosive();
                }
                currentHeldObj = null;
            }
        }


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

        //Moving Platform Info
        if (isOnPlatform && platform != null)
        {
            Vector3 platformMovement = platform.position - lastPlatformPos;
            transform.position += platformMovement;
        }
        if (platform != null)
        {
            lastPlatformPos = platform.position;
        }
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
        if (!grounded && !falling) // JUMPING
        {
            newVelocity.y = Mathf.MoveTowards(newVelocity.y, -1f, stats.maxJumpSpeed * Time.fixedDeltaTime);
        }
        if (falling) // FALLING
        {
            newVelocity.y = Mathf.MoveTowards(newVelocity.y, stats.gravity, stats.maxFallSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            if (!hasDied)
            {
                //hasDied = true;
                //DestroyPlayer();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && groundHit)            
        {
            GameObject gameObjectA = collision.gameObject;
            GameObject gameObjectB = groundHit.transform.gameObject;

            if (gameObjectA == gameObjectB)
            {
                JumpReset();
                grounded = true;
                platform = gameObjectB.transform;
                lastPlatformPos = platform.position;
                isOnPlatform = true;
            }
        }
    }

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!groundHit)
        {
            grounded = false;
            LevelOut();
            Fall();

            if (collision.gameObject.CompareTag("Ground"))
            {
                platform = null;
                isOnPlatform = false;
            }
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

    IEnumerator ThrowDelay()
    {
        canGrab = false;
        yield return new WaitForSeconds(0.2f);
        canGrab = true;
    }
    IEnumerator JumpDelay()
    { 
        checkingGround = false;
        yield return new WaitForSecondsRealtime(0.2f);
        checkingGround = true;
    }
    IEnumerator AttackDelay()
    {
        canAttack = false;
        yield return new WaitForSecondsRealtime(0.2f);
        canAttack = true;
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
} 