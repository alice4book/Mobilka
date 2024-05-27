using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Demo_MainPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ui;
    public int score = 0;
    public bool colorflag = true;
    //false - fioletowy
    //true - zolty

    void Update()
    {
        ui.text = score.ToString();
    }
}
