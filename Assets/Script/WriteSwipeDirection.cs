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
        textMesh.text = direction;
    }
}
