using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;

public class RopeControl : MonoBehaviour
{
    #region Variables
    private Camera _mainCamera;
    private SplineContainer _rope;
    private SplineExtrude _ropeExtrude;
    BezierKnot _heldKnot;
    #endregion

    private void Awake()
    {
        _mainCamera = Camera.main;
        _rope = transform.GetChild(0).GetComponent<SplineContainer>();
        _ropeExtrude= transform.GetChild(0).GetComponent<SplineExtrude>();
    }

    private void OnMouseDown()
    {
        if (_rope != null)
        {
            _heldKnot = new BezierKnot();
            _heldKnot.Position = GetTouchPosition();
            _rope.Spline.Add(_heldKnot);

        }
    }

    private void OnMouseDrag()
    {
        if (_rope != null) {
            _heldKnot.Position = _rope.transform.InverseTransformPoint(GetTouchPosition());
            _ropeExtrude.Spline.SetKnot(1, _heldKnot);
        }
    }

    private Vector3 GetTouchPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Assuming the first touch
            return _mainCamera.ScreenToWorldPoint(touch.position) + new Vector3(0,0,10);
        }
        return Vector3.zero; // Or handle if there's no touch input
    }
}
