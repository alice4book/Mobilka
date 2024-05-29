using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Manu_NewGameButton : MonoBehaviour
{
    void OnMouseDown()
    {
       // SceneManager.UnloadSceneAsync("Menu");
        SceneManager.LoadScene("Demo2");

    }
}
