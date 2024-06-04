using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuttle : MonoBehaviour
{
    
    private Animator animator;

    private int wrongMoveAnimation;
    private int correctMoveAnimation;
    private int idleAnimation;

    public bool leftShuttle;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if(leftShuttle) {
            wrongMoveAnimation = Animator.StringToHash("WrongMoveLeftShuttle");
            correctMoveAnimation = Animator.StringToHash("CorrectMoveLeftShuttle");
            idleAnimation = Animator.StringToHash("IdleLeftShuttle");
        } else {
            wrongMoveAnimation = Animator.StringToHash("WrongMoveRightShuttle");
            correctMoveAnimation = Animator.StringToHash("CorrectMoveRightShuttle");
            idleAnimation = Animator.StringToHash("IdleRightShuttle");
        }
    }

    public void Idle() 
    {
        animator.Play(idleAnimation,0,0);
    }

    public void WrongMove() 
    {
        animator.Play(wrongMoveAnimation,0,0);
    }

    public void CorrectMove()
    {
        animator.Play(correctMoveAnimation,0,0);
    }
}
