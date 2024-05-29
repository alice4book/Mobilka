using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    //https://www.youtube.com/watch?v=9dYDBomQpBQ
    public GameObject GameOverMenuObj;
    public bool isPaused;

    void Start()
    {
        GameOverMenuObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver() {
        GameOverMenuObj.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RestartGame() {
        SceneManager.LoadScene("Loom");
    }

}