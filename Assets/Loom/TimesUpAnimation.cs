using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimesUpAnimation : MonoBehaviour
{
    private Animator animator;

    private int idleAnimation;
    private int appearAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();

        idleAnimation = Animator.StringToHash("Idle");
        appearAnimation = Animator.StringToHash("Appear");

    }

    public void Appear()
    {   
        animator.Play(appearAnimation);
    }

    public void Idle()
    {
        animator.Play(idleAnimation);
    }
}
