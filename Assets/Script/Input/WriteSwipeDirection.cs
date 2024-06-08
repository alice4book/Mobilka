using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteSwipeDirection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshe;
    private string _lastDirection;
    private string _all;
    void Start()
    {
        SwipeDetection.OnSwipeDelegate += HandleSwipeDirection;
        _all = "";
    }


    private void OnDestroy()
    {
        SwipeDetection.OnSwipeDelegate -= HandleSwipeDirection;
    }

    private void HandleVectorCheck(string name, Vector2 value)
    {
        _all += name + " " + value.ToString() + "\n";
        _textMeshe.text = _all;
    }

    private void HandleSwipeDirection( int value)
    {
        string direction = "";
        switch (value)
        {
            case 1:
                direction += "Right";
                break;
            case 2:
                direction += "Left";
                break;
            case 3:
                direction += "Up";
                break;
            case 4:
                direction += "Down";
                break;
            case 5:
                direction += "Both";
                break;
            default:
                direction += "Error";
                break;
        }
        _all += direction + "\n";
        _textMeshe.text = _all;
    }
}
