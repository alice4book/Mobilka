using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialAnimations tutorialTextAnimations;
    [SerializeField] private TutorialAnimations tutorialDoubleLeftTextAnimations;
    [SerializeField] private TutorialAnimations tutorialDoubleRightTextAnimations;

    TextMeshProUGUI tutorialText;
    TextMeshProUGUI tutorialDoubleLeftText;
    TextMeshProUGUI tutorialDoubleRightText;

    Color tmpColor;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText = tutorialTextAnimations.gameObject.GetComponent<TextMeshProUGUI>();
        tutorialDoubleLeftText = tutorialDoubleLeftTextAnimations.gameObject.GetComponent<TextMeshProUGUI>();
        tutorialDoubleRightText = tutorialDoubleRightTextAnimations.gameObject.GetComponent<TextMeshProUGUI>();


        //tutorialTextAnimations.gameObject.SetActive(false);
        //tutorialDoubleLeftTextAnimations.gameObject.SetActive(false);
        //tutorialDoubleRightTextAnimations.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreManager.linesMade  == 0 && ScoreManager.numberOfGames == 0) 
        {
            //Debug.Log("FIRST");
            tutorialTextAnimations.gameObject.SetActive(true);
            tutorialTextAnimations.SwipeRight();
        } 
        else if(ScoreManager.linesMade  == 1 && ScoreManager.numberOfGames == 0)
        {
            //Debug.Log("SECOND");
            tutorialText.text = "Swipe left";
            tutorialTextAnimations.SwipeLeft();
        } 
        else if (ScoreManager.linesMade  == 2 && ScoreManager.numberOfGames == 0)
        {
            tutorialTextAnimations.gameObject.GetComponent<Animator>().enabled = false;
            tutorialTextAnimations.gameObject.SetActive(false);

            tmpColor = tutorialDoubleLeftText.color;
            tmpColor.a = 1;
            tutorialDoubleLeftText.color = tmpColor;

            tmpColor = tutorialDoubleRightText.color;
            tmpColor.a = 1;
            tutorialDoubleRightText.color = tmpColor;

            //tutorialDoubleLeftTextAnimations.gameObject.SetActive(true);
            //tutorialDoubleRightTextAnimations.gameObject.SetActive(true);
            tutorialDoubleLeftTextAnimations.SwipeRightHalf();
            tutorialDoubleRightTextAnimations.SwipeLeftHalf();

        } else if (ScoreManager.linesMade > 2 && ScoreManager.numberOfGames == 0)
        {
            //tutorialDoubleLeftTextAnimations.Idle();
            //tutorialDoubleRightTextAnimations.Idle();
            tutorialDoubleLeftTextAnimations.gameObject.GetComponent<Animator>().enabled = false;
            tutorialDoubleRightTextAnimations.gameObject.GetComponent<Animator>().enabled = false;
            tutorialDoubleLeftTextAnimations.gameObject.SetActive(false);
            tutorialDoubleRightTextAnimations.gameObject.SetActive(false);
        }

        if(ScoreManager.numberOfGames > 0) 
        {
            tutorialTextAnimations.gameObject.GetComponent<Animator>().enabled = false;
            tutorialDoubleLeftTextAnimations.gameObject.GetComponent<Animator>().enabled = false;
            tutorialDoubleRightTextAnimations.gameObject.GetComponent<Animator>().enabled = false;
            tutorialTextAnimations.gameObject.SetActive(false);
            tutorialDoubleLeftTextAnimations.gameObject.SetActive(false);
            tutorialDoubleRightTextAnimations.gameObject.SetActive(false);
        }
    }
}
