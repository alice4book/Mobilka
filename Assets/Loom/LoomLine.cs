using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoomLine : MonoBehaviour
{
    [SerializeField] public GameObject lineLeft;
    [SerializeField] public GameObject lineRight;
    
    private Animator animator;

    private int wrongMoveAnimation;
    private int idleAnimation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        wrongMoveAnimation = Animator.StringToHash("WrongMoveAnimation");
        idleAnimation = Animator.StringToHash("IdleAnimation");
    }

    public void Idle() 
    {
        animator.Play(idleAnimation,0,0);
    }

    public void WrongMove() 
    {
        animator.Play(wrongMoveAnimation,0,0);
    }
}
