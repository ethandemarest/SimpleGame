using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("COMPONENTS")]
    public MathAnimations ma;
    public SpriteRenderer sp;
    public LayerMask groundLayer;
    Rigidbody2D rb;
    public Animator animator;

    [Header("GAME OBJECTS")]
    TimeStop timeStop;
    public GameObject attackZone;
    public GameObject[] bodyparts;
    GameObject player;

    [Header("STATS")]
    public float followRange;
    public float attackRange;
    public float jumpDetectionRange;
    bool inFollowRange;
    bool inAttackRange;
    public bool decidingNextBehavior = true;
    bool facing = true;
    bool hasDied;
    bool canJump = true;
    public int health;
    public float speed;

    [Header("ANIMATIONS")]
    public AnimationCurve jumpCurveX;
    public AnimationCurve jumpCurveY;

    // COROUTINES
    Coroutine currentBehavior;
    RaycastHit2D ledgeHit;
    RaycastHit2D groundHit;

    Vector2 desiredDir;
    Vector2 enemyMov;
    Vector2 playerCounterForce;

    // Start is called before the first frame update
    void Start()
    {
        timeStop = GameObject.Find("Time Manager").GetComponent<TimeStop>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 1f), -desiredDir * followRange, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0, 0.8f), -desiredDir * attackRange, Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, 0.6f), -desiredDir * jumpDetectionRange, Color.blue);

        //CHECKS
        ledgeHit = Physics2D.Raycast(transform.position + new Vector3(0, 1f), -desiredDir, jumpDetectionRange, groundLayer);
        //groundHit = Physics2D.Raycast(transform.position + new Vector3(0, 1f), -desiredDir, jumpDetectionRange, groundLayer);

        desiredDir = new Vector2(transform.position.x - player.transform.position.x, 0).normalized;

        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        inFollowRange = playerDistance <= followRange;
        inAttackRange = playerDistance <= attackRange;

        if (facing)
        {
            attackZone.transform.position = transform.position + new Vector3(-desiredDir.x * 1.5f, 0) + new Vector3(0f, 1f, 0f);
            bool flip = transform.position.x - player.transform.position.x <= 0.0f ? false : true;
            sp.flipX = flip;
        }
        
        if (decidingNextBehavior)
        {
            if (inFollowRange)
            {
                if (ledgeHit && canJump) //JUMP
                {
                    if (currentBehavior != null)
                    {
                        StopCoroutine(currentBehavior);
                    }
                    StartCoroutine(JumpDelay());
                    currentBehavior = StartCoroutine(Jump(ledgeHit.transform.gameObject));
                }
                if (inAttackRange) //ATTACKING
                {
                    if (currentBehavior != null)
                    {
                        StopCoroutine(currentBehavior);
                    }
                    currentBehavior = StartCoroutine(AttackOne());
                }
                else
                {
                    if (currentBehavior != null) //FOLLOW
                    {
                        StopCoroutine(currentBehavior);
                    }
                    currentBehavior = StartCoroutine(FollowPlayer());
                }
            }
            else
            {
                if (currentBehavior != null) //IDLE
                {
                    StopCoroutine(currentBehavior);
                }
                currentBehavior = StartCoroutine(Idle());
                
            }
        }
    }

    void MoveEnemy()
    {
        float direction = (transform.position.x - player.transform.position.x) >= 0 ? -1 : 1;
        enemyMov.x = direction * speed; 
        enemyMov.y = Mathf.MoveTowards(transform.position.y, transform.position.y-1f, speed); // Gravity
        rb.velocity = enemyMov + playerCounterForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage") || collision.CompareTag("Explosion"))
        {
            if (currentBehavior != null)
            {
                StopCoroutine(currentBehavior);
            }
            if (collision.CompareTag("Damage"))
            {
                Vector2 hitDirection = collision.GetComponent<AttackBox>().attackDirection;
                if (hitDirection.x > 0)
                {
                    currentBehavior = StartCoroutine(Damage(Vector2.right, 1));
                }
                if (hitDirection.x < 0)
                {
                    currentBehavior = StartCoroutine(Damage(Vector2.left, 1));
                }
            }
            if (collision.CompareTag("Explosion"))
            {
                //timeStop.Stop(0.2f);
                currentBehavior = StartCoroutine(Damage(Vector2.zero, 3));
            }
        }
    }
    IEnumerator Idle()
    {
        decidingNextBehavior = false;

        animator.SetBool("Idle", true);
        while (!inFollowRange)
        {
            //DO Idle behavior
            if (inFollowRange)
            {
                yield break;
            }
            yield return null;
        }

        decidingNextBehavior = true;
    }

    IEnumerator FollowPlayer()
    {
        decidingNextBehavior = false;
        animator.SetBool("Walk", true);
        while (inFollowRange)
        {
            MoveEnemy();

            if (inAttackRange)
            {
                currentBehavior = StartCoroutine(AttackOne());
                yield break;
            }
            if (ledgeHit)
            {
                currentBehavior = StartCoroutine(Jump(ledgeHit.transform.gameObject));
                yield break;
            }
            yield return null;
        }
        decidingNextBehavior = true;
    }

    IEnumerator Jump(GameObject platform)
    {
        animator.SetBool("Crouch", true);
        facing = false;
        rb.velocity = Vector2.zero;
        decidingNextBehavior = false;
        BoxCollider2D platformCol = platform.GetComponent<BoxCollider2D>();

        float platformDistance = (platformCol.bounds.max.y - transform.position.y)/5;
        float posX;
        float landingZone = platform.GetComponent<BoxCollider2D>().bounds.max.y;
        float tempX;
        float tempY;
        float landingPos;
        float timeElapsed = 0;

        Vector2 startPos = transform.position;

        //FIND PLATFORM LEDGE
        posX = transform.position.x > platform.transform.position.x ?
            platformCol.bounds.center.x + (platform.GetComponent<BoxCollider2D>().bounds.extents.x - 1):
            platformCol.bounds.center.x - (platform.GetComponent<BoxCollider2D>().bounds.extents.x - 1);

        yield return new WaitForSeconds(0.2f);

        animator.SetBool("Jump", true);

        while (timeElapsed < platformDistance)
        {
            float t1 = timeElapsed / platformDistance;
            t1 = jumpCurveX.Evaluate(t1);
            float t2 = timeElapsed / platformDistance;
            t2 = jumpCurveY.Evaluate(t2);

            landingPos = Mathf.Lerp(startPos.y, landingZone, t1);
            tempX = Mathf.Lerp(startPos.x, posX, t1);
            tempY = Mathf.Lerp(landingPos, landingZone+2, t2);

            transform.position = new Vector3(tempX, tempY);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        animator.SetBool("Crouch", true);

        yield return new WaitForSeconds(0.2f);
        facing = true;
        decidingNextBehavior = true;
    }

    IEnumerator JumpDelay()
    {
        canJump = false;
        yield return new WaitForSeconds(1f);
        canJump = true;
    }

    IEnumerator AttackOne()
    {
        decidingNextBehavior = false;
        rb.velocity = Vector2.zero;
        animator.SetBool("Aim", true);
        facing = false;

        yield return new WaitForSeconds(2.5f);

        animator.SetBool("Attack", true);
        ma.wiggleY(0.2f);
        attackZone.GetComponent<AttackBox>().Attack(Vector2.zero);

        yield return new WaitForSeconds(1f);
        animator.SetBool("Idle", true);
        facing = true;
        decidingNextBehavior = true;
    }

    IEnumerator Damage(Vector2 damageDir, int damageVal)
    {
        health -= damageVal;
        if (health <= 0 && !hasDied)
        {
            hasDied = true;
            Death();
        }

        decidingNextBehavior = false;
        //Hit Animation
        facing = false;
        sp.color = new Color(1f, 1f, 1f, 0.5f);

        float intForce = 20;
        float duration = 0.2f;
        float timer = 0f;
        float t;

        while (timer < duration)
        {
            t = timer / duration;
            float dampedForce = Mathf.Lerp(intForce, 0, t); // gradually ease out
            transform.position += (Vector3)(damageDir.normalized * dampedForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        sp.color = Color.white;
        facing = true;
        decidingNextBehavior = true;
    }

    void Death()
    {
        foreach (GameObject gameObject in bodyparts)
        {
            var newPiece = Instantiate(gameObject, transform.position, Quaternion.Euler(Vector2.zero));
            newPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), 1f) * 20f;
            newPiece.GetComponent<Rigidbody2D>().AddTorque(Random.Range(25f, 100f));

        }
        Destroy(gameObject);
    }
}