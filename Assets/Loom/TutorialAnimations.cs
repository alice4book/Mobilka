using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialAnimations : MonoBehaviour
{

    private Animator animator;

    private int idleTextAnimation;
    private int swipeLeftTextAnimation;
    private int swipeLeftTextHalfAnimation;
    private int swipeRightTextAnimation;
    private int swipeRightTextHalfAnimation;

    // Start is called before the first frame update
    void Start()
    {

        //this.gameObject.SetActive(false);

        animator = GetComponent<Animator>();

        idleTextAnimation = Animator.StringToHash("IdleTextAnimation");
        swipeLeftTextAnimation = Animator.StringToHash("SwipeLeftTextAnimation");
        swipeLeftTextHalfAnimation = Animator.StringToHash("SwipeLeftTextHalfAnimation");
        swipeRightTextAnimation = Animator.StringToHash("SwipeRightTextAnimation");
        swipeRightTextHalfAnimation = Animator.StringToHash("SwipeRightTextHalfAnimation");
    }

    public void Idle()
    {   
        //Debug.Log("HERE");
        animator.Play(idleTextAnimation);
    }

    public void SwipeLeft()
    {
        animator.Play(swipeLeftTextAnimation);
    }

    public void SwipeLeftHalf()
    {
        animator.Play(swipeLeftTextHalfAnimation);
    }

    public void SwipeRight()
    {
        animator.Play(swipeRightTextAnimation);
    }

    public void SwipeRightHalf()
    {
        animator.Play(swipeRightTextHalfAnimation);
    }
}
