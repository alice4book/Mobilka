using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using TMPro;

public class SwipeDetection : MonoBehaviour
{
    #region Variables
    private Vector2 _swipeVector;
    private Vector2 _swipeVector2nd;
    private bool _bSecond;
    private bool _bDouble;
    [SerializeField] private float _minSwipeLength;
    public delegate void SwpieDelegate(int value);
    public static event SwpieDelegate OnSwipeDelegate;
    public delegate void VectorDelegate(string name, Vector2 value);
    public static event VectorDelegate OnVector;
    private Touch _primary;
    private Touch _secondary;
    #endregion

    public TMP_Text testText;
    public TMP_Text testText2;
    public TMP_Text testText3;
    public TMP_Text testText4;

    private void Awake()
    {
        _minSwipeLength = 0.1f;
        _bDouble = false;
        _bSecond = false;
        if (!EnhancedTouchSupport.enabled)
            EnhancedTouchSupport.Enable();

        _primary = new Touch();
        _secondary = new Touch();
    }

    private void Update()
    {
        //testText.text = Touch.activeTouches.Count.ToString();

        //testText3.text = _primary.phase.ToString();
        //testText4.text = _secondary.phase.ToString();

        if(_bDouble && Touch.activeTouches.Count < 2)
        {
            //testText2.text = "double LESS";
            DoubleSwipeDir();
            _primary = new Touch();
            _secondary = new Touch();
            _bSecond = true;
            _bDouble = false;
            
        } 
        /*
        else {
            if(_bDouble) {
                testText2.text = "double TWO";
            } else 
            {
                testText2.text = "not";
            }   
        }
        */
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
        if (_bDouble) 
            return;
        if (_bSecond)
        {
            _bSecond = false;
            return;
        }
        
        if (Touch.activeTouches.Count > 0)
        {
            Touch touch = Touch.activeTouches[0];
            // Dziwny bug nie widzi TouchPhase.Ended, ale na podniesieniu palca widzi Moved lub Stationary
            if (touch.phase == TouchPhase.Stationary 
                || touch.phase == TouchPhase.Moved 
                || touch.phase == TouchPhase.Canceled
                || touch.phase == TouchPhase.Ended)
            {
                _swipeVector = touch.screenPosition - touch.startScreenPosition;
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
            _primary = Touch.activeTouches[0];
            _secondary = Touch.activeTouches[1];
            _bDouble = true;

            /*
            if(_bDouble && 
                (_secondary.phase == TouchPhase.Ended || _secondary.phase == TouchPhase.Canceled ||
                _primary.phase == TouchPhase.Ended || _primary.phase == TouchPhase.Canceled))
            {
                DoubleSwipeDir();
                _primary = new Touch();
                _secondary = new Touch();
                _bSecond = true;
                _bDouble = false;
            }
            */
        }
    }

    private void DoubleSwipeDir()
    {
        //if ((_swipeVector.x >= 0 && _swipeVector2nd.x <= 0) || (_swipeVector2nd.x >= 0 && _swipeVector.x <= 0))
        //{
             OnSwipeDelegate?.Invoke(5);
        //}
    }

    public void DebugDoubleSwipeDir(InputAction.CallbackContext ctx)
    {
        if (Touch.activeTouches.Count > 0)
        {
            Touch primary = Touch.activeTouches[0];
            // Dziwny bug nie widzi TouchPhase.Ended, ale na podniesieniu palca widzi Moved lub Stationary
            if (primary.phase == TouchPhase.Stationary
                || primary.phase == TouchPhase.Moved
                || primary.phase == TouchPhase.Canceled
                || primary.phase == TouchPhase.Ended)
            {
                _swipeVector = primary.screenPosition - primary.startScreenPosition;
                if (_swipeVector.magnitude < _minSwipeLength)
                {
                    OnSwipeDelegate?.Invoke(5);
                    _swipeVector = Vector3.zero;
                }
            }
        }
    }

    private void CountSwipeDir()
    {
        // Only left & right
        if (Mathf.Abs(_swipeVector.x) > Mathf.Abs(_swipeVector.y)){
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
        
        }
        else
        {
            // Up or down swipe
            if (_swipeVector.y > 0)
            {
                // Up
                OnSwipeDelegate?.Invoke(3);
            }
            else
            {
                // Down
                OnSwipeDelegate?.Invoke(4);
            }
        }
        
    }
}