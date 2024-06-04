using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        //ScoreManager.highScore = ScoreManager.gameScore;
        scoreText.text = ScoreManager.gameScore.ToString();
        highScoreText.text = ScoreManager.highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
