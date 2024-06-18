using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private AudioSource _audioSource;
    private Coroutine _vignetteCoroutine;
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
        _vignetteCoroutine = null;
        _audioSource = GetComponent<AudioSource>();
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
            if (_bWarning && _vignetteCoroutine != null  && _whenWarningStarts < timeRemaining)
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
        _audioSource.Play();
        if (vignette != null)
        {
            _vignetteCoroutine = StartCoroutine(Vignette(vignette));
        }
    }

    void StopWarning()
    {
        //Stop Warning
        StopCoroutine(_vignetteCoroutine);
        _vignetteCoroutine = null;
        _bWarning = false;
        _audioSource.Stop();

        Camera mainCam = Camera.main;
        GameObject mainCamObj = mainCam.gameObject;
        VignetteController vignette = mainCamObj.GetComponent<VignetteController>();
        vignette.radius = 1.25f;
    }

    IEnumerator Vignette(VignetteController vignette)
    {
        float initialRadius = vignette.radius;

        while (vignette.radius > vignette.minRadius)
        {
            yield return new WaitForSeconds(0.01f);

            // Obliczanie proporcji pozosta³ego czasu do czasu rozpoczêcia ostrze¿enia
            float t = Mathf.Clamp01(timeRemaining / _whenWarningStarts);

            // Interpolacja liniowa miêdzy initialRadius a minRadius
            vignette.radius = Mathf.Lerp(vignette.minRadius, initialRadius, t);

            // Zmniejszanie timeRemaining
            timeRemaining -= 0.01f;
        }
    }
}
