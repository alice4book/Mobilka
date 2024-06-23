using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwipeReaction : MonoBehaviour
{
    [SerializeField] Color colorLeft;
    [SerializeField] Color colorRight;

    Color colorLeft2;
    Color colorRight2;

    public Loom loom;
    public TimerController timerController;

    //public int score;
    [SerializeField] private TextMeshProUGUI textMesh;
    private float swipeDelay = 0.2f;
    private Coroutine swipeCoroutine;

    //bottom shuttles
    public Shuttle shuttleLeft;
    public Shuttle shuttleRight;

    //top shuttles
    public Shuttle shuttleLeft2;
    public Shuttle shuttleRight2;

    [SerializeField] public List<GameObject> shuttleSpots;

    void Start()
    {
        colorLeft = GlobalVar.color1;
        colorRight = GlobalVar.color2;
        colorLeft2 = GlobalVar.color3;
        colorRight2 = GlobalVar.color4;

        shuttleLeft.gameObject.transform.position = shuttleSpots[0].transform.position;
        shuttleRight.gameObject.transform.position = shuttleSpots[1].transform.position;
        shuttleLeft2.gameObject.transform.position = shuttleSpots[2].transform.position;
        shuttleRight2.gameObject.transform.position = shuttleSpots[3].transform.position;

        //score = 0;
        SwipeDetection.OnSwipeDelegate += HandleSwipeDirection;
    }

    private void HandleSwipeDirection(int value)
    {
        Color lineLeftColor = loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color;
        Color lineRightColor = loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color;

        Vector3 leftColorVec = new Vector3(colorLeft.r, colorLeft.g, colorLeft.b);
        Vector3 leftColorVec2 = new Vector3(colorLeft2.r, colorLeft2.g, colorLeft2.b);
        Vector3 rightColorVec = new Vector3(colorRight.r, colorRight.g, colorRight.b);
        Vector3 rightColorVec2 = new Vector3(colorRight2.r, colorRight2.g, colorRight2.b);

        Vector3 currentLeftColorVec = new Vector3(lineLeftColor.r, lineLeftColor.g, lineLeftColor.b);
        Vector3 currentRightColorVec = new Vector3(lineRightColor.r, lineRightColor.g, lineRightColor.b);

        if (currentLeftColorVec == currentRightColorVec)
        {
            Debug.Log(currentLeftColorVec);
            Debug.Log(rightColorVec2);
            Debug.Log(leftColorVec2);


            // Single swipe needed
            if ((value == 1 && currentLeftColorVec == leftColorVec) || (value == 2 && currentLeftColorVec == rightColorVec))
            {
                // Correct single swipe, value = direction
                if(value == 1 && currentLeftColorVec == leftColorVec) {
                    shuttleLeft.CorrectMove();
                }
                if(value == 2 && currentLeftColorVec == rightColorVec) {
                    shuttleRight.CorrectMove();
                }

                ProcessCorrectSwipe();
            }
            else if((value == 1 && currentLeftColorVec == leftColorVec2) || (value == 2 && currentLeftColorVec == rightColorVec2)) {
                
                if(value == 1 && currentLeftColorVec == leftColorVec2) {
                    //Debug.Log("HERE");
                    shuttleLeft2.CorrectMove();
                }
                if(value == 2 && currentLeftColorVec == rightColorVec2) {
                    shuttleRight2.CorrectMove();
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
        Color outlineLeftColor = lineLeftColor * loom.outlineBase;
        Color outlineRightColor = lineRightColor * loom.outlineBase;
        loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color = new Color(lineLeftColor.r, lineLeftColor.g, lineLeftColor.b, 1.0f);
        loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color = new Color(lineRightColor.r, lineRightColor.g, lineRightColor.b, 1.0f);
        loom.lines[0].outlineL.GetComponent<SpriteRenderer>().color = new Color(outlineLeftColor.r, outlineLeftColor.g, outlineLeftColor.b, 1.0f);
        loom.lines[0].outlineR.GetComponent<SpriteRenderer>().color = new Color(outlineRightColor.r, outlineRightColor.g, outlineRightColor.b, 1.0f);
        loom.MoveLines();
        textMesh.text = GlobalVar.gameScore.ToString();
    }

    void ProcessIncorrectSwipe()
    {
        GlobalVar.ResetCombo();
        timerController.DeleteTime();
        Color lineLeftColor = loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color;
        Color lineRightColor = loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color;
        //loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color = new Color(lineLeftColor.r, lineLeftColor.g, lineLeftColor.b, 0.75f);
        //loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color = new Color(lineRightColor.r, lineRightColor.g, lineRightColor.b, 0.75f);
        loom.lines[0].WrongMove();
        textMesh.text = GlobalVar.gameScore.ToString();
    }

    private void OnDestroy()
    {
        SwipeDetection.OnSwipeDelegate -= HandleSwipeDirection;
    }
}