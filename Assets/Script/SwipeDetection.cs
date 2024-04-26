using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{

    #region Variables
    private Camera _mainCamera;
    private Vector3 _start;
    private Vector3 _end;
    private Vector3 _swipeVector;
    private float _minSwipeLenght;
    public delegate void ObjectAddedDelegate(int addedObject);
    public static event ObjectAddedDelegate OnSwipe;
    #endregion


    private void Awake()
    {
        _mainCamera = Camera.main;
        _minSwipeLenght = 0.5f;
    }

    private Vector3 GetTouchPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Assuming the first touch
            return _mainCamera.ScreenToWorldPoint(touch.position);
        }
        return Vector3.zero; // Or handle if there's no touch input
    }


    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _start = GetTouchPosition();
        }
        else if (ctx.canceled)
        {
            _end = GetTouchPosition();
            _swipeVector = _end - _start;
            if(_swipeVector.magnitude > _minSwipeLenght)
                CountSwipeDir();
        }
    }

    private void CountSwipeDir()
    {
        if (Mathf.Abs(_swipeVector.x) > Mathf.Abs(_swipeVector.y))
        {
            // Ruch w lewo lub w prawo
            if (_swipeVector.x > 0)
            {
                //Right
                OnSwipe?.Invoke(1);
            }
            else
            {
                //Left
                OnSwipe?.Invoke(2);
            }
        }
        else
        {
            // Ruch w górê lub w dó³
            if (_swipeVector.y > 0)
            {
                //Up
                OnSwipe?.Invoke(3);
            }
            else
            {
                //Down
                OnSwipe?.Invoke(4);
            }
        }
    }
}
