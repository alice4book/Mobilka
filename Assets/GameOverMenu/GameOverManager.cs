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
        GlobalVar.numberOfGames++;
        Debug.Log(GlobalVar.numberOfGames);
        //GlobalVar.highScore = GlobalVar.gameScore;
        scoreText.text = GlobalVar.gameScore.ToString();
        linesMadeText.text = GlobalVar.linesMade.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
