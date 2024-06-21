using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerAnimation : MonoBehaviour
{
    private Animator animator;

    private int idleTimerAnimation;
    private int noTimeAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();

        idleTimerAnimation = Animator.StringToHash("IdleTime");
        noTimeAnimation = Animator.StringToHash("NoTime");

    }

    public void NoTime()
    {   
        animator.Play(noTimeAnimation);
    }

    public void IdleTime()
    {
        //Debug.Log("Here");
        animator.Play(idleTimerAnimation);
    }

}
