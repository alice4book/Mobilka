using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom : MonoBehaviour
{
    [SerializeField] public List<GameObject> lines;
    public List<Color> colors;
    [SerializeField] Color colorRight;
    [SerializeField] Color colorLeft;

    // Start is called before the first frame update
    void Start()
    {
        colors = new List<Color>();

        GenerateColorList();
        assignLineColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateColorList() {
        Color tmpColor = new Color(1.0f,0.0f,0.0f,0.5f);
        Color redColor = new Color(1.0f,0.0f,0.0f,0.5f);
        Color blueColor = new Color(0.0f,0.0f,1.0f,0.5f);
        for(int i = 0; i < 1000; i++) {
            int random = Random.Range(0,2);
            switch(random) 
            {
                case 0:
                    //tmpColor = redColor;
                    tmpColor = colorLeft;
                    break;
                case 1:
                    //tmpColor = blueColor;
                    tmpColor = colorRight;
                    break;
                default:
                    //tmpColor = redColor;
                    tmpColor = colorLeft;
                    break;
            }

            colors.Add(tmpColor);
        }
    }

    public void assignLineColors() {
        int colorNr = 0;
        if(colors.Count > lines.Count) {
            for(int i = 0; i < lines.Count; i++) {
                lines[i].GetComponent<SpriteRenderer>().color = colors[colorNr];
                colorNr++;
            }
        }
    }
}
