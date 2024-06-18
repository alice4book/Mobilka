using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ResumeMenu : MonoBehaviour
{
    [SerializeField] GameObject Canvas1;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject Input;

    void OnMouseDown()
    {
        Input.SetActive(true);
        Canvas1.SetActive(false);
        Timer.GetComponent<TimerController>().isTimeRunning = true;

    }
}
