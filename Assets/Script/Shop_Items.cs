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
    [SerializeField] private int index;

    [SerializeField] private Animator padlockAnim;
    [SerializeField] private Animator priceAnim;

    public void Start()
    {
        GetComponent<SpriteRenderer>().color = color;
        pricetext.text = price.ToString();
        unlocked = GlobalVar.listUnlocked[index];
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
                GlobalVar.listUnlocked[index] = unlocked;
                padlock.SetActive(false);
                pricetext.enabled = false;
                GlobalVar.coins = GlobalVar.coins - price;
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dragging = true;
            }
            else
            {
                padlockAnim.SetTrigger("CantBuy");
                priceAnim.SetTrigger("CantBuy");
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
