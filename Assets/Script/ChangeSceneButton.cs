using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manu_NewGameButton : MonoBehaviour
{
    public bool Menu = false;
    public bool Demo2 = false;
    public bool Shop = false;
    void OnMouseDown()
    {
        if (Demo2)
        {
            if(SceneManager.GetActiveScene().name == "Menu") 
            {
                GlobalVar.fromMenu = true;
            } 
            else 
            {
                GlobalVar.fromMenu = false;
            }
            SceneManager.LoadScene("Demo2");
        }
        if (Menu)
        {
            SceneManager.LoadScene("Menu");
        }
        if (Shop)
        {
            SceneManager.LoadScene("Shop");
        }


    }
}
