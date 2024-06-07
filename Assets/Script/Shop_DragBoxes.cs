using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_DragBoxes : MonoBehaviour
{
    [SerializeField] GameObject yarn;
    public bool color2;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ShoopItems>().GetDragged())
        {
            if (color2)
            {
                if(GlobalVar.color1 != collision.gameObject.GetComponent<ShoopItems>().GetColor())
                {
                    GlobalVar.color2 = collision.gameObject.GetComponent<ShoopItems>().GetColor();
                    yarn.GetComponent<SpriteRenderer>().color = collision.gameObject.GetComponent<ShoopItems>().GetColor();
                }

            }
            else
            {
                if (GlobalVar.color2 != collision.gameObject.GetComponent<ShoopItems>().GetColor())
                {
                    GlobalVar.color1 = collision.gameObject.GetComponent<ShoopItems>().GetColor();
                    yarn.GetComponent<SpriteRenderer>().color = collision.gameObject.GetComponent<ShoopItems>().GetColor();
                }
            }


        }
    }
    public void Start()
    {
        Debug.Log(yarn.GetComponent<SpriteRenderer>().color);
        if (color2)
        {
            yarn.GetComponent<SpriteRenderer>().color = GlobalVar.color2;

        }
        else
        {
            yarn.GetComponent<SpriteRenderer>().color = GlobalVar.color1;
        }
        
    }
}
