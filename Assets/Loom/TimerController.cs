using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public Image timerFill;
    public TMP_Text timeText;

    public float timeRemaining;
    public float startingTime = 5;

    //public GameOverMenu gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining >= 1) {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        } else {
            SceneManager.LoadScene("GameOver");
            //gameOverMenu.GameOverMenuObj.SetActive(true);
        }
    }

    void DisplayTime(float timeToDisplay)  
    {
        timeToDisplay -=1;
        timerFill.fillAmount = timeToDisplay / startingTime;
        timeText.text = timeToDisplay.ToString("F0");
    }

    public void AddTime() 
    {
        //Debug.Log("AddTime");
        timeRemaining += 1;
        if(timeRemaining > startingTime) {
            timeRemaining = startingTime;
        }
    }

        public void DeleteTime() 
    {
        //Debug.Log("AddTime");
        timeRemaining -= 2;
        if(timeRemaining <= 0) {
            timeRemaining = 0;
        }
    }
}
