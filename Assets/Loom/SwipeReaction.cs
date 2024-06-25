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

    public Shuttle activeLeftShuttle;
    public Shuttle activeRightShuttle;

    [SerializeField] public List<GameObject> shuttleSpots;

    bool firstUpdate = true;

    void Start()
    {
        colorLeft = GlobalVar.color1;
        colorRight = GlobalVar.color2;
        colorLeft2 = GlobalVar.color3;
        colorRight2 = GlobalVar.color4;

        if(GlobalVar.fromMenu) {

            shuttleLeft.gameObject.transform.position = shuttleSpots[2].transform.position;
            shuttleRight.gameObject.transform.position = shuttleSpots[3].transform.position;
            shuttleLeft2.gameObject.transform.position = shuttleSpots[0].transform.position;
            shuttleRight2.gameObject.transform.position = shuttleSpots[1].transform.position;
            activeLeftShuttle = shuttleLeft2;
            activeRightShuttle = shuttleRight2;
        } 
        else
        {
            shuttleLeft.gameObject.transform.position = shuttleSpots[0].transform.position;
            shuttleRight.gameObject.transform.position = shuttleSpots[1].transform.position;
            shuttleLeft2.gameObject.transform.position = shuttleSpots[2].transform.position;
            shuttleRight2.gameObject.transform.position = shuttleSpots[3].transform.position;
            activeLeftShuttle = shuttleLeft;
            activeRightShuttle = shuttleRight;
        }


        //shuttleLeft2.IdleStatic();
        //shuttleRight2.IdleStatic();

        //score = 0;
        SwipeDetection.OnSwipeDelegate += HandleSwipeDirection;
    }

    void Update()
    {
        if(firstUpdate)
        {
            firstUpdate = false;
            shuttleLeft.IdleStatic();
            shuttleRight.IdleStatic();
        }
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


        if(value == 3 || value == 4)
        {
            Debug.Log("changed left");
            if(activeLeftShuttle == shuttleLeft) {
                activeLeftShuttle = shuttleLeft2;
                activeLeftShuttle.gameObject.transform.position = shuttleSpots[0].transform.position;
                shuttleLeft.gameObject.transform.position = shuttleSpots[2].transform.position;
                activeLeftShuttle.Idle();
                shuttleLeft.IdleStatic();
            } else {
                activeLeftShuttle = shuttleLeft;
                activeLeftShuttle.gameObject.transform.position = shuttleSpots[0].transform.position;
                shuttleLeft2.gameObject.transform.position = shuttleSpots[2].transform.position;
                activeLeftShuttle.Idle();
                shuttleLeft2.IdleStatic();
            }
            
            Debug.Log(activeLeftShuttle);
        } 
        else if(value == 6 || value == 7)
        {
            Debug.Log("changed right");
            if(activeRightShuttle == shuttleRight) {
                activeRightShuttle = shuttleRight2;
                activeRightShuttle.gameObject.transform.position = shuttleSpots[1].transform.position;
                shuttleRight.gameObject.transform.position = shuttleSpots[3].transform.position;
                activeRightShuttle.Idle();
                shuttleRight.IdleStatic();
            } else {
                activeRightShuttle = shuttleRight;
                activeRightShuttle.gameObject.transform.position = shuttleSpots[1].transform.position;
                shuttleRight2.gameObject.transform.position = shuttleSpots[3].transform.position;
                activeRightShuttle.Idle();
                shuttleRight2.IdleStatic();
            }

        }

        if (currentLeftColorVec == currentRightColorVec)
        {


            // Single swipe needed
            if ((value == 1 && currentLeftColorVec == leftColorVec && activeLeftShuttle == shuttleLeft) || (value == 2 && currentLeftColorVec == rightColorVec && activeRightShuttle == shuttleRight))
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
            else if((value == 1 && currentLeftColorVec == leftColorVec2 && activeLeftShuttle == shuttleLeft2) || (value == 2 && currentLeftColorVec == rightColorVec2 && activeRightShuttle == shuttleRight2)) {

                if(value == 1 && currentLeftColorVec == leftColorVec2) {
                    //Debug.Log("HERE");
                    shuttleLeft2.CorrectMove();
                }
                if(value == 2 && currentLeftColorVec == rightColorVec2) {
                    shuttleRight2.CorrectMove();
                }
                
                ProcessCorrectSwipe();
            }
            else if (value != 3 && value != 4 && value != 6 && value != 7)
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
                //color1 & color2
                if(currentLeftColorVec == leftColorVec && currentRightColorVec == rightColorVec && activeLeftShuttle == shuttleLeft && activeRightShuttle == shuttleRight)
                {
                    shuttleLeft.CorrectHalfMove();
                    shuttleRight.CorrectHalfMove();
                    // Both swipes are correct
                    ProcessCorrectSwipe();
                }
                //color1 & color4
                else if(currentLeftColorVec == leftColorVec && currentRightColorVec == rightColorVec2 && activeLeftShuttle == shuttleLeft && activeRightShuttle == shuttleRight2)
                {
                    shuttleLeft.CorrectHalfMove();
                    shuttleRight2.CorrectHalfMove();
                    // Both swipes are correct
                    ProcessCorrectSwipe();
                }
                //color2 & color3
                else if(currentLeftColorVec == leftColorVec2 && currentRightColorVec == rightColorVec && activeLeftShuttle == shuttleLeft2 && activeRightShuttle == shuttleRight)
                {
                    shuttleLeft2.CorrectHalfMove();
                    shuttleRight.CorrectHalfMove();
                    // Both swipes are correct
                    ProcessCorrectSwipe();
                }
                //color2 & color3
                else if(currentLeftColorVec == leftColorVec2 && currentRightColorVec == rightColorVec2 && activeLeftShuttle == shuttleLeft2 && activeRightShuttle == shuttleRight2)
                {
                    shuttleLeft2.CorrectHalfMove();
                    shuttleRight2.CorrectHalfMove();
                    // Both swipes are correct
                    ProcessCorrectSwipe();
                }
                else
                {
                    activeLeftShuttle.WrongMove();
                    activeRightShuttle.WrongMove();
                    ProcessIncorrectSwipe();
                }

            } 
            else if (value == 1)
            {
                activeLeftShuttle.WrongMove();
                ProcessIncorrectSwipe();
            }
            else if (value == 2)
            {
                activeRightShuttle.WrongMove();
                ProcessIncorrectSwipe();
            }
            else if (value != 3 && value != 4 && value != 6 && value != 7)
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

        if((GlobalVar.linesMade  == 5 && GlobalVar.fromMenu == true) || (GlobalVar.linesMade  == 1 && GlobalVar.fromMenu == false)) {
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