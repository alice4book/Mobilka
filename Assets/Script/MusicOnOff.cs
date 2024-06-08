using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnOff : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);   
    }

    private void Start()
    {
        if (musicSource != null)
        {
            if (GlobalVar.musicOn)
            {
                musicSource.Play();
            }
            else
            {
                musicSource.Stop();
            }
        }
    }

    public void MusicSettings(bool value)
    {
        if (musicSource != null)
        {
            if (value)
            {
                musicSource.Play();
            }
            else
            {
                musicSource.Stop();
            }
            GlobalVar.musicOn = value;
        }
    }
}
