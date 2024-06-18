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
    private float swipeDelay = 0.2f;
    private Coroutine swipeCoroutine;

    //shuttles
    public Shuttle shuttleLeft;
    public Shuttle shuttleRight;

    void Start()
    {
        colorLeft = GlobalVar.color1;
        colorRight = GlobalVar.color2;

        //score = 0;
        SwipeDetection.OnSwipeDelegate += HandleSwipeDirection;
    }

    private void HandleSwipeDirection(int value)
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
                if(value == 5)
                {
                    shuttleLeft.WrongMove();
                    shuttleRight.WrongMove();
                }
                ProcessIncorrectSwipe();
            }
        }
        else
        {
            // Double swipe
            if (value == 5)
            {
                shuttleLeft.CorrectHalfMove();
                shuttleRight.CorrectHalfMove();
                // Both swipes are correct
                ProcessCorrectSwipe();
            } else
            if (value == 1)
            {
                shuttleLeft.WrongMove();
                ProcessIncorrectSwipe();
            }
            else
            if (value == 2)
            {
                shuttleRight.WrongMove();
                ProcessIncorrectSwipe();
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

    void ResetCorrectSwipesDelay(GameObject loomLine)
    {
        if(loom.lines[0].gameObject == loomLine)
        {
            GlobalVar.ResetCombo();
            timerController.DeleteTime();
            loom.lines[0].WrongMove();
        }
    }

    void ProcessCorrectSwipe()
    {
        GlobalVar.IncreaseLinesMade();
        GlobalVar.IncreaseCombo();
        //GlobalVar.CalculateComboMultiplier();
        GlobalVar.AddToScore();
        GlobalVar.ManageCoins();

        if((GlobalVar.linesMade  == 3 && GlobalVar.fromMenu == true) || (GlobalVar.linesMade  == 1 && GlobalVar.fromMenu == false)) {
            timerController.isTimeRunning = true;
        }

        timerController.AddTime();
        Color lineLeftColor = loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color;
        Color lineRightColor = loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color;
        loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color = new Color(lineLeftColor.r, lineLeftColor.g, lineLeftColor.b, 1.0f);
        loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color = new Color(lineRightColor.r, lineRightColor.g, lineRightColor.b, 1.0f);
        loom.MoveLines();
        textMesh.text = GlobalVar.gameScore.ToString();
    }

    void ProcessIncorrectSwipe()
    {
        GlobalVar.ResetCombo();
        timerController.DeleteTime();
        Color lineLeftColor = loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color;
        Color lineRightColor = loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color;
        loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color = new Color(lineLeftColor.r, lineLeftColor.g, lineLeftColor.b, 0.75f);
        loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color = new Color(lineRightColor.r, lineRightColor.g, lineRightColor.b, 0.75f);
        loom.lines[0].WrongMove();
        textMesh.text = GlobalVar.gameScore.ToString();
    }

    private void OnDestroy()
    {
        SwipeDetection.OnSwipeDelegate -= HandleSwipeDirection;
    }
}