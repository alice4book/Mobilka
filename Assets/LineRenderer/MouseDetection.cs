using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetection : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private bool isHeld = false;
    private bool mouseOverSprite = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && mouseOverSprite)
        {
            isHeld = true;
            //Debug.Log("held");

            Vector3 mousePosition = Input.mousePosition;

            // Convert the screen coordinates to world coordinates
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

            transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);

        } else {
            isHeld = false;
        }
    }

    void OnMouseEnter() {
        mouseOverSprite = true;
    }

    void OnMouseExit() {
        mouseOverSprite = false;
    }

    bool IsMouseOverSprite() {
        
        Vector3 mousePosition = Input.mousePosition;

        return spriteRenderer.bounds.Contains(mousePosition);
    }
}
