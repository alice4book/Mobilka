using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveScores : MonoBehaviour
{
    public TMP_Text linesMadeRecordText;

    private void Start()
    {
        
        int linesMadeSaved = GetSavedValue();
        Debug.Log(linesMadeSaved);
        Debug.Log(ScoreManager.linesMade);
        if(linesMadeSaved < ScoreManager.linesMade)
        {
            SetNewSavedValue(ScoreManager.linesMade);
            linesMadeRecordText.text = ScoreManager.linesMade.ToString();
        } else {
            linesMadeRecordText.text = linesMadeSaved.ToString();
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
