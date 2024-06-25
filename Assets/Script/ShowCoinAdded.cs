using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCoinAdded : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GlobalVar.OnAddMoney += StartCoinAnimation;
    }

    private void StartCoinAnimation()
    {
        _animator.SetTrigger("Plus one");
    }
}
