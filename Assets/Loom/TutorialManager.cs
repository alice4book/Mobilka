using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialAnimations tutorialLeft;
    [SerializeField] private TutorialAnimations tutorialRight;

    GameObject tutorialArrowLeft;
    GameObject tutorialArrowRight;

    Color tmpColor;

    // Start is called before the first frame update
    void Start()
    {
        tutorialArrowLeft = tutorialLeft.gameObject;
        tutorialArrowRight = tutorialRight.gameObject;

        tutorialArrowLeft.SetActive(false);
        tutorialArrowRight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalVar.linesMade  == 0 && GlobalVar.numberOfGames == 0) 
        {
            //Debug.Log("FIRST");
            tutorialArrowLeft.SetActive(true);
            tutorialLeft.SwipeFull();
        } 
        else if(GlobalVar.linesMade  == 1 && GlobalVar.numberOfGames == 0)
        {
            //Debug.Log("SECOND");
            tutorialArrowLeft.GetComponent<Animator>().enabled = false;
            tutorialArrowLeft.SetActive(false);

            tutorialArrowRight.SetActive(true);
            tutorialRight.SwipeFull();
        } 
        else if (GlobalVar.linesMade  == 2 && GlobalVar.numberOfGames == 0)
        {
            tutorialArrowLeft.GetComponent<Animator>().enabled = true;
            tutorialArrowLeft.SetActive(true);

            //tmpColor = tutorialArrowLeft.color;
            //tmpColor.a = 1;
            //tutorialArrowLeft.color = tmpColor;

            //tmpColor = tutorialArrowRight.color;
            //tmpColor.a = 1;
            //tutorialArrowRight.color = tmpColor;

            //tutorialLeft.gameObject.SetActive(true);
            //tutorialRight.gameObject.SetActive(true);

            tutorialLeft.SwipeHalf();
            tutorialRight.SwipeHalf();

        } else if (GlobalVar.linesMade > 2 && GlobalVar.numberOfGames == 0)
        {
            //tutorialLeft.Idle();
            //tutorialRight.Idle();
            tutorialArrowLeft.GetComponent<Animator>().enabled = false;
            tutorialArrowRight.GetComponent<Animator>().enabled = false;
            tutorialArrowLeft.SetActive(false);
            tutorialArrowRight.SetActive(false);
        }

        if(GlobalVar.numberOfGames > 0) 
        {
            tutorialArrowLeft.GetComponent<Animator>().enabled = false;
            tutorialArrowRight.GetComponent<Animator>().enabled = false;
            tutorialArrowLeft.SetActive(false);
            tutorialArrowRight.SetActive(false);
        }
    }
}
