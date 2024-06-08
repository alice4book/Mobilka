using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShoopItems : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pricetext;
    public int price;
    public bool unlocked = false;
    public Color  color;
    [SerializeField] GameObject padlock;
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 position;

    public void Start()
    {
        GetComponent<SpriteRenderer>().color = color;
        pricetext.text = price.ToString();
        if (unlocked)
        {
            padlock.SetActive(false);
            pricetext.enabled = false;
        }
    }

    public void OnMouseDown()
    {
        position = transform.position;
        if (unlocked)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
        }
        else
        {
            if(GlobalVar.coins >= price)
            {
                unlocked = true;
                padlock.SetActive(false);
                pricetext.enabled = false;
                GlobalVar.coins = GlobalVar.coins - price;
            }
            else
            {
                //shake text
            }
        }
    }

    public void OnMouseUp()
    {
        dragging = false;
        transform.position = position;
    }

    public void Update()
    {
        if(dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    public bool GetDragged()
    {
        return dragging;
    }

    public Color GetColor()
    {
        return color;
    }
}
