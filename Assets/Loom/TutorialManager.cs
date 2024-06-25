using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialAnimations tutorialLeft;
    [SerializeField] private TutorialAnimations tutorialRight;
    [SerializeField] private TutorialUpAnimations tutorialUpOnRight;
    [SerializeField] private TutorialUpAnimations tutorialUpOnLeft;

    GameObject tutorialArrowLeft;
    GameObject tutorialArrowRight;
    GameObject tutorialArrowUpRight;
    GameObject tutorialArrowUpLeft;

    Color tmpColor;

    // Start is called before the first frame update
    void Start()
    {
        tutorialArrowLeft = tutorialLeft.gameObject;
        tutorialArrowRight = tutorialRight.gameObject;
        tutorialArrowUpRight = tutorialUpOnRight.gameObject;
        tutorialArrowUpLeft = tutorialUpOnLeft.gameObject;

        tutorialArrowLeft.SetActive(false);
        tutorialArrowRight.SetActive(false);
        tutorialArrowUpLeft.SetActive(false);
        tutorialArrowUpRight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalVar.linesMade  == 0 && GlobalVar.fromMenu == true) 
        {
            //Debug.Log("FIRST");
            tutorialArrowLeft.SetActive(true);
            tutorialLeft.SwipeFull();
        } 
        else if(GlobalVar.linesMade  == 1 && GlobalVar.fromMenu == true)
        {
            //Debug.Log("SECOND");
            tutorialArrowLeft.GetComponent<Animator>().enabled = false;
            tutorialArrowLeft.SetActive(false);

            tutorialArrowRight.SetActive(true);
            tutorialRight.SwipeFull();
        } 
        else if (GlobalVar.linesMade  == 2 && GlobalVar.fromMenu == true)
        {
            tutorialArrowRight.GetComponent<Animator>().enabled = false;
            tutorialArrowRight.SetActive(false);

            tutorialArrowUpLeft.SetActive(true);
            tutorialUpOnLeft.SwipeUp();

        }
        else if (GlobalVar.linesMade  == 3 && GlobalVar.fromMenu == true)
        {
            tutorialArrowUpLeft.GetComponent<Animator>().enabled = false;
            tutorialArrowUpLeft.SetActive(false);

            tutorialArrowUpRight.SetActive(true);
            tutorialUpOnRight.SwipeUp();

        } 
        else if (GlobalVar.linesMade  == 4 && GlobalVar.fromMenu == true)
        {
            tutorialArrowUpRight.GetComponent<Animator>().enabled = false;
            tutorialArrowUpRight.SetActive(false);

            tutorialArrowLeft.GetComponent<Animator>().enabled = true;
            tutorialArrowLeft.SetActive(true);
            tutorialArrowRight.GetComponent<Animator>().enabled = true;
            tutorialArrowRight.SetActive(true);

            tutorialLeft.SwipeHalf();
            tutorialRight.SwipeHalf();

        } 
        else if (GlobalVar.linesMade > 4 && GlobalVar.fromMenu == true)
        {
            //tutorialLeft.Idle();
            //tutorialRight.Idle();
            tutorialArrowLeft.GetComponent<Animator>().enabled = false;
            tutorialArrowRight.GetComponent<Animator>().enabled = false;
            tutorialArrowLeft.SetActive(false);
            tutorialArrowRight.SetActive(false);
        }

        if(GlobalVar.fromMenu == false) 
        {
            tutorialArrowLeft.GetComponent<Animator>().enabled = false;
            tutorialArrowRight.GetComponent<Animator>().enabled = false;
            tutorialArrowLeft.SetActive(false);
            tutorialArrowRight.SetActive(false);
        }
    }
}
