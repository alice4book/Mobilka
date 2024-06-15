using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class TimerController : MonoBehaviour
{
    public Image timerFill;
    public TMP_Text timeText;

    public float timeRemaining;
    public float startingTime = 5;

    public float timeSpeed = 1.0f;
    public float minTimeSpeed = 1.0f;
    public float maxTimeSpeed = 8.0f;

    #region Warning
    [SerializeField] private float _whenWarningStarts;
    private bool _bWarning;
    private Coroutine vignetteCoroutine;
    #endregion


    public Loom loom;

    public bool isTimeRunning;
    //public GameOverMenu gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        isTimeRunning = false;
        timeRemaining = startingTime;
        GlobalVar.linesMade = 0;
        vignetteCoroutine = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimeRunning) {
            ChangeTimeSpeed();

            if(timeRemaining >= 1) {
                timeRemaining -= Time.deltaTime * timeSpeed;
                DisplayTime(timeRemaining);
            } else {
                SceneManager.LoadScene("GameOver");
                //gameOverMenu.GameOverMenuObj.SetActive(true);
            }

            if (!_bWarning && _whenWarningStarts >= timeRemaining)
            {
                StartWarning();
            }
            if (_bWarning && vignetteCoroutine != null  && _whenWarningStarts < timeRemaining)
            {
                StopWarning();
            }
        }
    }

    void DisplayTime(float timeToDisplay)  
    {
        timeToDisplay -=1;
        timerFill.fillAmount = timeToDisplay / startingTime;
        timeText.text = timeToDisplay.ToString("F0");
    }

    public void AddTime() 
    {
        //Debug.Log("AddTime");
        if(isTimeRunning) {
            timeRemaining += 1;
            if(timeRemaining > startingTime) {
                timeRemaining = startingTime;
            }
        }
    }

    public void DeleteTime() 
    {
        //Debug.Log("AddTime");
        if(isTimeRunning) {
            timeRemaining -= 2;
            if(timeRemaining <= 0) {
                timeRemaining = 0;
            }
        }
    }

    void ChangeTimeSpeed()
    {
        int itemCount = GlobalVar.linesMade;
        float ratio = (float)itemCount / 1000.0f;
        timeSpeed = Mathf.Lerp(minTimeSpeed, maxTimeSpeed, ratio);
    }

    void StartWarning()
    {
        _bWarning = true;
        Camera mainCam = Camera.main;
        GameObject mainCamObj = mainCam.gameObject;
        VignetteController vignette = mainCamObj.GetComponent<VignetteController>();
        vignette.radius = 1.25f;
        if (vignette != null)
        {
            vignetteCoroutine = StartCoroutine(Vignette(vignette));
        }
    }

    void StopWarning()
    {
        //Stop Warning
        StopCoroutine(vignetteCoroutine);
        vignetteCoroutine = null;
        _bWarning = false;

        Camera mainCam = Camera.main;
        GameObject mainCamObj = mainCam.gameObject;
        VignetteController vignette = mainCamObj.GetComponent<VignetteController>();
        vignette.radius = 1.25f;
    }

    IEnumerator Vignette(VignetteController vignette)
    {
        while (vignette.maxRadius < vignette.radius)
        {
            yield return new WaitForSeconds(0.01f);
            vignette.radius -= 0.01f;
        }
    }
}
