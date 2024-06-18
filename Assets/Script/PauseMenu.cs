using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject Canvas1;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject Input;

    void OnMouseDown()
    {
        Input.SetActive(false);
        Canvas1.SetActive(true);
        Timer.GetComponent<TimerController>().isTimeRunning = false;

    }
}
