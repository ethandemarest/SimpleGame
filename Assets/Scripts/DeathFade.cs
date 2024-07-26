using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFade : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeOut()
    {
        animator.SetBool("FadeOut", true);
    }
    public void FadeIn()
    {
        animator.SetBool("FadeIn", true);
    }
}
