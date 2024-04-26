using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeColorOnTap : MonoBehaviour
{
    #region Variables
    private Color _white;
    private Color _black;
    private Color _current;
    [SerializeField] private SpriteRenderer _bg;
    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        _white = Color.white;
        _black = Color.black;
        _bg = GetComponent<SpriteRenderer>();
        _bg.color = _white;
    }
    public void OnTap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(_current == _white) { _current = _black; }
            else{ _current = _white; }
            _bg.color = _current;
        }
    }
}
