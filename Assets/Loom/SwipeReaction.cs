using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwipeReaction : MonoBehaviour
{
    [SerializeField] Color colorLeft;
    [SerializeField] Color colorRight;
    public Loom loom;
    public TimerController timerController;

    //public int score;
    [SerializeField] private TextMeshProUGUI textMesh;
    private bool[] correctSwipe;
    private float swipeDelay = 0.2f;
    private Coroutine swipeCoroutine;

    //shuttles
    public Shuttle shuttleLeft;
    public Shuttle shuttleRight;

    void Start()
    {
        //score = 0;
        correctSwipe = new bool[2] { false, false };
        SwipeDetection.OnSwipe += HandleSwipeDirectionn;
    }

    private void HandleSwipeDirectionn(int swipeIndex, int value)
    {
        Color lineLeftColor = loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color;
        Color lineRightColor = loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color;
        Vector3 leftColorVec = new Vector3(colorLeft.r, colorLeft.g, colorLeft.b);
        Vector3 rightColorVec = new Vector3(colorRight.r, colorRight.g, colorRight.b);

        Vector3 currentLeftColorVec = new Vector3(lineLeftColor.r, lineLeftColor.g, lineLeftColor.b);
        Vector3 currentRightColorVec = new Vector3(lineRightColor.r, lineRightColor.g, lineRightColor.b);

        if (currentLeftColorVec == currentRightColorVec)
        {
            // Single swipe needed
            if ((value == 1 && currentLeftColorVec == leftColorVec) || (value == 2 && currentLeftColorVec == rightColorVec))
            {
                // Correct single swipe
                if(value == 1 && currentLeftColorVec == leftColorVec) {
                    shuttleLeft.CorrectMove();
                }
                if(value == 2 && currentLeftColorVec == rightColorVec) {
                    shuttleRight.CorrectMove();
                }
                ProcessCorrectSwipe();
            }
            else
            {
                // Incorrect swipe
                if(value == 1 && currentLeftColorVec != leftColorVec) {
                    shuttleLeft.WrongMove();
                }
                if(value == 2 && currentLeftColorVec != rightColorVec) {
                    shuttleRight.WrongMove();
                }
                ProcessIncorrectSwipe();
            }
        }
        else
        {
            // Double swipe needed
            if (value == 1 && currentLeftColorVec == leftColorVec)
            {
                correctSwipe[0] = true;
            }
            else if (value == 2 && currentRightColorVec == rightColorVec)
            {
                correctSwipe[1] = true;
            }

            if (correctSwipe[0] && correctSwipe[1])
            {
                // Both swipes are correct
                ProcessCorrectSwipe();
                ResetCorrectSwipes();
            }
            else
            {
                if (swipeCoroutine != null)
                {
                    StopCoroutine(swipeCoroutine);
                }
                swipeCoroutine = StartCoroutine(ResetCorrectSwipesAfterDelay(swipeDelay, loom.lines[0].gameObject));
            }
        }
    }

    IEnumerator ResetCorrectSwipesAfterDelay(float delay, GameObject loomLine)
    {
        yield return new WaitForSeconds(delay);
        ResetCorrectSwipesDelay(loomLine);
    }

    void ResetCorrectSwipes()
    {

        correctSwipe[0] = false;
        correctSwipe[1] = false;

    }

    void ResetCorrectSwipesDelay(GameObject loomLine)
    {

        correctSwipe[0] = false;
        correctSwipe[1] = false;

        if(loom.lines[0].gameObject == loomLine)
        {
            ScoreManager.ResetCombo();
            timerController.DeleteTime();
            loom.lines[0].WrongMove();
        }

    }

    void ProcessCorrectSwipe()
    {
        ScoreManager.IncreaseLinesMade();
        ScoreManager.IncreaseCombo();
        //ScoreManager.CalculateComboMultiplier();
        ScoreManager.AddToScore();

        timerController.AddTime();
        Color lineLeftColor = loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color;
        Color lineRightColor = loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color;
        loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color = new Color(lineLeftColor.r, lineLeftColor.g, lineLeftColor.b, 1.0f);
        loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color = new Color(lineRightColor.r, lineRightColor.g, lineRightColor.b, 1.0f);
        loom.MoveLines();
        textMesh.text = ScoreManager.gameScore.ToString();
    }

    void ProcessIncorrectSwipe()
    {
        ScoreManager.ResetCombo();
        timerController.DeleteTime();
        Color lineLeftColor = loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color;
        Color lineRightColor = loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color;
        loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color = new Color(lineLeftColor.r, lineLeftColor.g, lineLeftColor.b, 0.75f);
        loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color = new Color(lineRightColor.r, lineRightColor.g, lineRightColor.b, 0.75f);
        loom.lines[0].WrongMove();
        textMesh.text = ScoreManager.gameScore.ToString();
    }

    private void OnDestroy()
    {
        SwipeDetection.OnSwipe -= HandleSwipeDirectionn;
    }
}