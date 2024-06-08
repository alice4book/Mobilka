using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class SwipeDetection : MonoBehaviour
{
    #region Variables
    private Vector2 _start;
    private Vector2 _end;
    private Vector2 _swipeVector;
    private Vector2 _start2nd;
    private Vector2 _end2nd;
    private Vector2 _swipeVector2nd;
    private bool _bSingle;
    private bool _bDouble;
    [SerializeField] private float _minSwipeLength;
    public delegate void SwpieDelegate(int value);
    public static event SwpieDelegate OnSwipeDelegate;
    public delegate void VectorDelegate(string name,Vector2 value);
    public static event VectorDelegate OnVector;
    #endregion

    private void Awake()
    {
        _minSwipeLength = 0.1f;
        _bDouble = false;
        _bSingle = false;
        if (!EnhancedTouchSupport.enabled)
            EnhancedTouchSupport.Enable();
    }

    private Vector2 GetTouchPosition(int index)
    {
        if (Touch.activeTouches.Count > index)
        {
            Touch touch = Touch.activeTouches[index];
            return touch.screenPosition;
        }
        return Vector2.zero;
    }


    public void OnSwipe(InputAction.CallbackContext ctx)
    {
        if ((_bDouble && !_bSingle) || (!_bDouble && _bSingle) || (_bDouble && _bSingle)) {
            _bDouble = false ; _bSingle = false ;
            return; 
        }
        if (Touch.activeTouches.Count > 0)
        {
            Touch primary = Touch.activeTouches[0];
            // Dziwny bug nie widzi TouchPhase.Ended, ale na podniesieniu palca widzi Moved lub Stationary
            if (primary.phase == TouchPhase.Stationary 
                || primary.phase == TouchPhase.Moved 
                || primary.phase == TouchPhase.Canceled
                || primary.phase == TouchPhase.Ended)
            {
                _end = GetTouchPosition(0);
                _swipeVector = primary.screenPosition - primary.startScreenPosition;
                if (_swipeVector.magnitude > _minSwipeLength)
                {
                    CountSwipeDir();
                    _swipeVector = Vector3.zero;
                }
            }
        }
    }

    public void OnDoubleSwipe(InputAction.CallbackContext ctx)
    {
        if (Touch.activeTouches.Count > 1)
        {
            Touch primary = Touch.activeTouches[0];
            Touch secondary = Touch.activeTouches[1];

            if (primary.history.Count >= 1)
            {
                _swipeVector = primary.screenPosition - primary.startScreenPosition;
                _end = primary.screenPosition;
                _bSingle = true;
            }
            if (secondary.history.Count >= 1)
            { 
                _swipeVector2nd = secondary.screenPosition - secondary.startScreenPosition;
                _end2nd = secondary.screenPosition;
                _bDouble = true;
            }
            if(_bSingle && _bDouble && (secondary.phase == TouchPhase.Ended || secondary.phase == TouchPhase.Canceled || primary.phase == TouchPhase.Ended || primary.phase == TouchPhase.Canceled))
            {
                DoubleSwipeDir();
            }
        }
    }

    private void DoubleSwipeDir()
    {
        // Ruch w lewo & w prawo
       if ((_swipeVector.x > 0 && _swipeVector2nd.x < 0 && _end.x < _end2nd.x) || (_swipeVector2nd.x > 0 && _swipeVector.x < 0 && _end.x > _end2nd.x))
       {
            OnSwipeDelegate?.Invoke(5);
       }
    }

    private void CountSwipeDir()
    {
        // Only left & right
        //if (Mathf.Abs(_swipeVector.x) > Mathf.Abs(_swipeVector.y)){
        // Ruch w lewo lub w prawo
        if (_swipeVector.x > 0)
        {
            // Right
            OnSwipeDelegate?.Invoke(1);
        }
        else
        {
            // Left
            OnSwipeDelegate?.Invoke(2);
        }
        /*
        }
        else
        {
            // Up or down swipe
            if (_swipeVector.y > 0)
            {
                // Up
                OnSwipe?.Invoke(3);
            }
            else
            {
                // Down
                OnSwipe?.Invoke(4);
            }
        }
        */
    }
}