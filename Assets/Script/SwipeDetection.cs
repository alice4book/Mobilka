using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    #region Variables
    private Camera _mainCamera;
    private Vector3[] _startPositions = new Vector3[2];
    private Vector3[] _endPositions = new Vector3[2];
    private Vector3[] _swipeVectors = new Vector3[2];
    private bool[] _isSwiping = new bool[2];
    private float _minSwipeLength;
    public delegate void ObjectAddedDelegate(int swipeIndex, int direction);
    public static event ObjectAddedDelegate OnSwipe;
    #endregion

    private void Awake()
    {
        Debug.Log("SwipeDetection");
        _mainCamera = Camera.main;
        _minSwipeLength = 0.5f;
    }

    private Vector3 GetTouchPosition(int touchIndex)
    {
        if (Input.touchCount > touchIndex)
        {
            Touch touch = Input.GetTouch(touchIndex);
            return _mainCamera.ScreenToWorldPoint(touch.position);
        }
        return Vector3.zero;
    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            for (int i = 0; i < 2; i++)
            {
                if (Input.touchCount > i)
                {
                    _startPositions[i] = GetTouchPosition(i);
                    _isSwiping[i] = true;
                    Debug.Log($"Start swipe {i}");
                }
            }
        }
        else if (ctx.canceled)
        {
            for (int i = 0; i < 2; i++)
            {
                if (_isSwiping[i] && Input.touchCount > i)
                {
                    _endPositions[i] = GetTouchPosition(i);
                    _swipeVectors[i] = _endPositions[i] - _startPositions[i];
                    if (_swipeVectors[i].magnitude > _minSwipeLength)
                    {
                        CountSwipeDir(i);
                    }
                    _isSwiping[i] = false;
                }
            }
        }
    }

    private void CountSwipeDir(int index)
    {
        int direction = 0;
        if (Mathf.Abs(_swipeVectors[index].x) > Mathf.Abs(_swipeVectors[index].y))
        {
            // Left or right swipe
            if (_swipeVectors[index].x > 0)
            {
                // Right
                direction = 1;
            }
            else
            {
                // Left
                direction = 2;
            }
        }
        else
        {
            // Up or down swipe
            if (_swipeVectors[index].y > 0)
            {
                // Up
                direction = 3;
            }
            else
            {
                // Down
                direction = 4;
            }
        }
        OnSwipe?.Invoke(index, direction);
    }
}