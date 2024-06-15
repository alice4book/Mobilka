using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialAnimations : MonoBehaviour
{

    private Animator animator;

    private int idleArrowAnimation;
    private int swipeFullAnimation;
    private int swipeHalfAnimation;

    public bool isLeft = false;

    // Start is called before the first frame update
    void Start()
    {

        //this.gameObject.SetActive(false);

        animator = GetComponent<Animator>();

        Debug.Log(animator);

        if(isLeft) 
        {
            idleArrowAnimation = Animator.StringToHash("IdleArrowLeft");
            swipeFullAnimation = Animator.StringToHash("FullSwipeLeft");
            swipeHalfAnimation = Animator.StringToHash("HalfSwipeLeft");
        }
        else 
        {
            idleArrowAnimation = Animator.StringToHash("IdleArrowRight");
            swipeFullAnimation = Animator.StringToHash("FullSwipeRight");
            swipeHalfAnimation = Animator.StringToHash("HalfSwipeRight");
        }


    }

    public void Idle()
    {   
        //Debug.Log("HERE");
        animator.Play(idleArrowAnimation);
    }

    public void SwipeFull()
    {
        //Debug.Log("Here");
        animator.Play(swipeFullAnimation);
    }

    public void SwipeHalf()
    {
        animator.Play(swipeHalfAnimation);
    }

}
