using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCoinAdded : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GlobalVar.OnAddMoney += StartCoinAnimation;
    }

    private void StartCoinAnimation()
    {
        _audio.Play();
        _animator.SetTrigger("Plus one");
    }
}
