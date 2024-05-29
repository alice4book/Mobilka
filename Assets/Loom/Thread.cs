using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Thread : MonoBehaviour
{
    [SerializeField] Color color;
    public Loom loom;
    public TimerController timerController;

    int score;
    [SerializeField] private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        //if(loom.colors.Count > 20) {
            score = int.Parse(textMesh.text);
            Color tmpLineColor = loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color;
            Vector3 lineColor = new Vector3(tmpLineColor.r, tmpLineColor.g, tmpLineColor.b);
            Vector3 threadColor = new Vector3(color.r,color.g,color.b);
            if(lineColor == threadColor) {
                score++;
                timerController.AddTime();
                loom.lines[0].lineLeft.GetComponent<SpriteRenderer>().color = color;
                loom.lines[0].lineRight.GetComponent<SpriteRenderer>().color = color;
                loom.colorPairs.RemoveAt(0);
                loom.assignLineColors();
            } else {
                score = 0;
            }
            textMesh.text = score.ToString();
        //}

  
    }
}
