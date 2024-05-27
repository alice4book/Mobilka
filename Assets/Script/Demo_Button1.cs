using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Button1 : MonoBehaviour
{
    [SerializeField] SpriteRenderer target;
    Color _color1 = new Color(149.0f/255, 161.0f/255, 43.0f/255 , 1.0f);
    Color _color2;
    Color _color3 = new Color(128.0f / 255, 44.0f / 255, 192.0f / 255, 1.0f);
    public int randomnum;
    [SerializeField] Demo_MainPanel mainpanel;
    void OnMouseDown()
    {
        _color2 = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = _color1;
        if (mainpanel.colorflag)
        {
            mainpanel.score += 1;
            randomnum = Random.Range(0, 2);
            if (randomnum == 0)
            {
                target.GetComponent<SpriteRenderer>().color = _color2;
                mainpanel.colorflag = true;
            }
            else
            {
                target.GetComponent<SpriteRenderer>().color = _color3;
                mainpanel.colorflag = false;
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
