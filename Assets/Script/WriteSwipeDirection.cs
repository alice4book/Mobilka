using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteSwipeDirection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] textMeshes = new TextMeshProUGUI[2];

    private void Awake()
    {
        for (int i = 0; i < textMeshes.Length; i++)
        {
            //textMeshes[i] = transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
    }

    void Start()
    {
        SwipeDetection.OnSwipe += HandleSwipeDirection;
    }

    private void HandleSwipeDirection(int swipeIndex, int value)
    {
        if (swipeIndex < 0 || swipeIndex >= textMeshes.Length) return;

        string direction = "";
        switch (value)
        {
            case 1:
                Debug.Log("R");
                direction = "Right";
                break;
            case 2:
                Debug.Log("L");
                direction = "Left";
                break;
            case 3:
                Debug.Log("U");
                direction = "Up";
                break;
            case 4:
                Debug.Log("D");
                direction = "Down";
                break;
        }
        textMeshes[swipeIndex].text = direction;
    }
}
