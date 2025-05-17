using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public Vector2 attackDirection;
    BoxCollider2D bc;
    SpriteRenderer sp;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();

        sp.color = Color.clear;
        bc.enabled = false;
        attackDirection = Vector2.zero;
    }
    public void Attack(Vector2 attackDir)
    {
        attackDirection = attackDir;
        StartCoroutine(FlashAttack());
    }
    IEnumerator FlashAttack()
    {
        bc.enabled = true;
        sp.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sp.color = Color.clear;
        bc.enabled = false;
        attackDirection = Vector2.zero;
    }
}
