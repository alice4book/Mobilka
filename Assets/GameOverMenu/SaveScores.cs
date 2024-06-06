using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveScores : MonoBehaviour
{
    public TMP_Text highestScoreRecordText;

    private void Start()
    {
        int highestScoreSaved = GetSavedValue();
        //Debug.Log(highestScoreSaved);
        //Debug.Log(ScoreManager.gameScore);
        if(highestScoreSaved < ScoreManager.gameScore)
        {
            SetNewSavedValue(ScoreManager.gameScore);
            highestScoreRecordText.text = ScoreManager.gameScore.ToString();
        } else {
            highestScoreRecordText.text = highestScoreSaved.ToString();
        }
    }

    private int GetSavedValue()
    {
        return PlayerPrefs.GetInt("LinesMadeRecord", 0);
    }

    private void SetNewSavedValue(int score)
    {
        PlayerPrefs.SetInt("LinesMadeRecord", score);
        PlayerPrefs.Save();
    }

}
