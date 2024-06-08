using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinDisplayer : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = GlobalVar.coins.ToString();
    }
}
