using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMananger : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    void Start()
    {
        toggle.isOn = GlobalVar.musicOn;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 0)
        {
            MusicOnOff musicScript = objs[0].GetComponent<MusicOnOff>();
            toggle.onValueChanged.AddListener(musicScript.MusicSettings);
        }
    }

}
