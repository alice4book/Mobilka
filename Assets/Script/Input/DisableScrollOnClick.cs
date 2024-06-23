using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableScrollOnClick : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    public void Start()
    {
        scrollRect = gameObject.transform.parent?.parent?.parent?.gameObject.GetComponent<ScrollRect>();
    }


    public void OnMouseDown()
    {
        if (scrollRect)
        {
            scrollRect.enabled = false;
        }
    }

    public void OnMouseUp()
    {
        if (scrollRect)
        {
            scrollRect.enabled = true;
        }
    }
}
