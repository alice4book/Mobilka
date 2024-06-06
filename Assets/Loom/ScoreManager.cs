using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //score based on lines made and comboMultiplier
    public static int gameScore;

    //number of lines made in this game
    public static int linesMade;

    //highest number of correct lines without any wrong moves
    public static int highestScore;

    //by how much to multiply the points we get for a correct move
    public static int comboMultiplier;

    //how may lines are correct without a wrong move
    public static int combo;

    //is it first game, number of games played
    public static int numberOfGames;

    public static void AddToScore()
    {
        gameScore = gameScore + (1 * comboMultiplier);
    }

    public static void IncreaseLinesMade()
    {
        linesMade++;
    }

    public static void IncreaseCombo()
    {
        combo++;
        CalculateComboMultiplier();
    }

    public static void ResetCombo()
    {
        combo = 0;
        CalculateComboMultiplier();
    }

    //zaczynajac od 20 poprawnych linii, co 10 poprawnych linii comboMultiplier zwieksza sie o 1
    public static void CalculateComboMultiplier()
    {
        if(combo < 10) {
            comboMultiplier = 1;
        } else {
            int comboLower = (combo/10) * 10;
            comboMultiplier = comboLower / 10;
        }
        Debug.Log(comboMultiplier);

    }

}
