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

    int score;
    [SerializeField] private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("SwipeReaction");
        score = 0;
        SwipeDetection.OnSwipe += HandleSwipeDirectionn;
    }

    private void HandleSwipeDirectionn(int value)
    {
        //string direction = "";
        switch (value)
        {
            case 1:
                //direction = "Right";
                Vector3 threadColorL = new Vector3(colorLeft.r,colorLeft.g,colorLeft.b);
                MoveLoomLine(threadColorL, colorLeft);
                break;
            case 2:
                //direction = "Left";
                Vector3 threadColorR = new Vector3(colorRight.r,colorRight.g,colorRight.b);
                MoveLoomLine(threadColorR, colorRight);
                break;
            case 3:
                //direction = "Up";
                break;
            case 4:
                //direction = "Down";
                break;
        }

        //textMesh.text = direction;
    }

    void MoveLoomLine(Vector3 threadColor, Color color) {
        //if(loom.colors.Count > 20) {
            Color tmpLineColor = loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color;
            Vector3 lineColor = new Vector3(tmpLineColor.r, tmpLineColor.g, tmpLineColor.b);
            //Vector3 threadColor = new Vector3(color.r,color.g,color.b);
            if(lineColor == threadColor) {
                score++;
                //Debug.Log("score++");
                timerController.AddTime();
                loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color = color;
                loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color = color;
                loom.colorPairs.RemoveAt(0);
                loom.assignLineColors();
            } else 
            {
                score = 0;
            }
            textMesh.text = score.ToString();
        //}  
    }

}
