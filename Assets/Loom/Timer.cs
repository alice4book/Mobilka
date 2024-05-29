using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public float startingTime = 120;
    public float timeRemaining = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
        timeRemaining = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsRunning) {

            if(timeRemaining >= 0) {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
        }
    }

    void DisplayTime(float timeToDisplay)  
    {
        timeToDisplay -=1;
        timeText.text = timeToDisplay.ToString("F0");
    }
}
