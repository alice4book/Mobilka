using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
    public static Color color1 = new Color(245.0f / 240, 242.0f / 255, 0);
    public static Color color2 = new Color(151.0f / 255, 1.0f / 255, 244.0f / 255);

    public static Color color3 = new Color(2.0f / 255, 0 , 244.0f / 255);
    public static Color color4 = new Color(30.0f / 255, 245.0f / 255, 1.0f / 255);

    public static int coins = 3000;

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

    //is music on
    public static bool musicOn = true;

    //was the game started from menu (true) or gameover (false)
    public static bool fromMenu = true;

    //list of unlocked
    public static List<bool> listUnlocked;

    // Delegate for animation of Coin added
    public delegate void AddMoney();

    public static event AddMoney OnAddMoney;

    static GlobalVar()
    {
        listUnlocked = new List<bool>{
        true, false, false,
        true, false, false,
        true, false, false,
        true, false, false,
        false, false, false,
        false, false, false,
        false, false, false
        };
    }

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
            comboMultiplier = (comboLower / 10) + 1;
        }
        //Debug.Log(comboMultiplier);

    }

    public static void ManageCoins()
    {
        if(linesMade % 20 == 0)
        {
            //Debug.Log("Made 20 lines");
            //Debug.Log(linesMade);
            coins += 10;
            OnAddMoney?.Invoke();
            //Debug.Log(coins);
        }
    }

}


