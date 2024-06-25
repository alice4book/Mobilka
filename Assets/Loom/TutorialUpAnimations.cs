using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUpAnimations : MonoBehaviour
{
    private Animator animator;

    private int idleUpArrowAnimation;
    private int swipeUpAnimation;

    //public bool isLeft = false;

    // Start is called before the first frame update
    void Start()
    {

        //this.gameObject.SetActive(false);

        animator = GetComponent<Animator>();

        //Debug.Log(animator);

        idleUpArrowAnimation = Animator.StringToHash("IdleArrowUp");
        swipeUpAnimation = Animator.StringToHash("SwipeUp");


    }

    public void Idle()
    {   
        animator.Play(idleUpArrowAnimation);
    }

    public void SwipeUp()
    {
        animator.Play(swipeUpAnimation);
    }
}
