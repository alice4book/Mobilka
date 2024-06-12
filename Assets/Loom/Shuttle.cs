using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuttle : MonoBehaviour
{
    
    private Animator animator;

    private int wrongMoveAnimation;
    private int correctMoveAnimation;
    private int idleAnimation;
    private int correctMoveHalfAnimation;

    public bool leftShuttle;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if(leftShuttle) {
            GetComponentInChildren<SpriteRenderer>().color = GlobalVar.color1;
        } else {
            GetComponentInChildren<SpriteRenderer>().color = GlobalVar.color2;
        }

        if(leftShuttle) {
            wrongMoveAnimation = Animator.StringToHash("WrongMoveLeftShuttle");
            correctMoveAnimation = Animator.StringToHash("CorrectMoveLeftShuttle");
            idleAnimation = Animator.StringToHash("IdleLeftShuttle");
            correctMoveHalfAnimation = Animator.StringToHash("CorrectHalfMoveLeftShuttle");
        } else {
            wrongMoveAnimation = Animator.StringToHash("WrongMoveRightShuttle");
            correctMoveAnimation = Animator.StringToHash("CorrectMoveRightShuttle");
            idleAnimation = Animator.StringToHash("IdleRightShuttle");
            correctMoveHalfAnimation = Animator.StringToHash("CorrectHalfMoveRightShuttle");
        }
    }

    public void Idle() 
    {
        animator.Play(idleAnimation,0,0);
    }

    public void WrongMove() 
    {
        animator.Play(wrongMoveAnimation,0,0);
        Handheld.Vibrate();
    }

    public void CorrectMove()
    {
        animator.Play(correctMoveAnimation,0,0);
    }

    public void CorrectHalfMove()
    {
        animator.Play(correctMoveHalfAnimation,0,0);
    }
}
