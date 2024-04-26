using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteSwipeDirection : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textMesh;
    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SwipeDetection.OnSwipe += HandleSwipeDirection;
    }

    private void HandleSwipeDirection(int value)
    {
        string direction = "";
        switch (value)
        {
            case 1:
                direction = "Right";
                break;
            case 2:
                direction = "Left";
                break;
            case 3:
                direction = "Up";
                break;
            case 4:
                direction = "Down";
                break;
        }
        textMesh.text = direction;
    }
}
