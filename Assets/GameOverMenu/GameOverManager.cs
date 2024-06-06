using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text linesMadeText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.numberOfGames++;
        Debug.Log(ScoreManager.numberOfGames);
        //ScoreManager.highScore = ScoreManager.gameScore;
        scoreText.text = ScoreManager.gameScore.ToString();
        linesMadeText.text = ScoreManager.linesMade.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
