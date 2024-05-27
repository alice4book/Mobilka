using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Demo_Button2 : MonoBehaviour
{
    [SerializeField] SpriteRenderer target;
    Color _color1 = new Color(110.0f / 255, 59.0f / 255, 150.0f / 255, 1.0f);
    Color _color2;
    Color _color3 = new Color(203.0f / 255, 224.0f / 255, 4.0f / 255, 1.0f);
    int randomnum;
    [SerializeField] Demo_MainPanel mainpanel;
    void OnMouseDown()
    {
        _color2 = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = _color1;
        if (!mainpanel.colorflag)
        {
            mainpanel.score += 1;
            randomnum = Random.Range(0, 2);
            if(randomnum == 0)
            {
                target.GetComponent<SpriteRenderer>().color = _color2;
                mainpanel.colorflag = false;
            }
            else
            {
                target.GetComponent<SpriteRenderer>().color = _color3;
                mainpanel.colorflag = true;
            }

        }
        else
        {
            mainpanel.score = 0;
        }
    }

    void OnMouseUp()
    {
        gameObject.GetComponent<SpriteRenderer>().color = _color2;
    }

}