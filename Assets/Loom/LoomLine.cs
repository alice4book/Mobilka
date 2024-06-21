using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoomLine : MonoBehaviour
{
    [SerializeField] public GameObject lineLeft;
    [SerializeField] public GameObject lineRight;
    [SerializeField] public GameObject outline;
    
    private Animator animator;

    private int wrongMoveAnimation;
    private int idleAnimation;
    private int currentLineAnimation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        wrongMoveAnimation = Animator.StringToHash("WrongMoveAnimation");
        idleAnimation = Animator.StringToHash("IdleAnimation");
        currentLineAnimation = Animator.StringToHash("CurrentLineAnimation");
    }

    public void Idle() 
    {
        animator.Play(idleAnimation,0,0);
    }

    public void WrongMove() 
    {
        animator.Play(wrongMoveAnimation,0,0);

    }
    public void CurrentLine()
    {
        animator.Play(currentLineAnimation,0,0);
    }
}
