using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public MathAnimations ma;
    public SpriteRenderer sp;
    Rigidbody2D rb;
    GameObject player;
    public float movementSpeed;
    public float followRange;
    public float attackRange;
    public bool inFollowRange;
    public bool inAttackRange;
    public bool attacking;
    public bool canMove = true;
    bool facing = true;
    public float speed;
    public Vector2 desiredDir;

    // COROUTINES
    private Coroutine damageCoroutine;
    private Coroutine attackCoroutine;

    public AnimationCurve animCurve;
    public float duration;

    public Sprite idle;
    public Sprite walk;
    public Sprite aim;
    public Sprite attack;
    public Sprite hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        inFollowRange = playerDistance <= followRange ? true : false;
        inAttackRange = playerDistance <= attackRange ? true : false;

        if (facing)
        {
            if (transform.position.x - player.transform.position.x <= 0.2f)
            {
                sp.flipX = false;
            }
            else if (transform.position.x - player.transform.position.x >= -0.2f)
            {
                sp.flipX = true;
            }
        }

        desiredDir = new Vector2(transform.position.x - player.transform.position.x, 0).normalized;

    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            if (!attacking)
            {
                if (inFollowRange)
                {
                    sp.sprite = walk;
                    if (inAttackRange)
                    {
                        attackCoroutine = StartCoroutine(AttackOne());
                    }

                    //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed);
                    //rb.velocity = -desiredDir * speed;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            rb.velocity = Vector2.zero;

            Vector2 attackDir = transform.position - collision.transform.position;
            print(attackDir);
            rb.AddForce(attackDir.normalized * 250f);



            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
            damageCoroutine = StartCoroutine(Damage(desiredDir));
        }
    }
    IEnumerator AttackOne()
    {
        ma.Wave(0.2f);

        sp.sprite = aim;

        facing = false;
        attacking = true;

        yield return new WaitForSeconds(1f);
        sp.sprite = attack;
        ma.wiggleY(0.2f);


        yield return new WaitForSeconds(1f);
        sp.sprite = idle;

        facing = true;
        attacking = false;
    }

    IEnumerator Damage(Vector2 direction)
    {
        sp.sprite = hit;
        //ma.wiggleY(0.2f);
        canMove = false;
        facing = false;
        attacking = false;
        sp.color = new Color(1f, 1f, 1f, 0.5f);
        
        yield return new WaitForSeconds(1f);
        
        sp.color = Color.white;
        canMove = true;
        facing = true;
    }
}