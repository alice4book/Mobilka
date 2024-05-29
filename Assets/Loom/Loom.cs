using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom : MonoBehaviour
{
    [SerializeField] public List<LoomLine> lines;
    //public List<Color> colors;
    public List<ColorPair> colorPairs;
    
    [SerializeField] Color colorLeft;
    [SerializeField] Color colorRight;

    // Start is called before the first frame update
    void Start()
    {
        //colors = new List<Color>();
        colorPairs = new List<ColorPair>();

        //GenerateColorList();
        GenerateColorPairList();
        assignLineColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    void GenerateColorList() {
        Color tmpColor = new Color(1.0f,0.0f,0.0f,0.5f);
        //Color redColor = new Color(1.0f,0.0f,0.0f,0.5f);
        //Color blueColor = new Color(0.0f,0.0f,1.0f,0.5f);
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
    */

    public void assignLineColors() {
        int colorNr = 0;
        if(colorPairs.Count > lines.Count) {
            for(int i = 0; i < lines.Count; i++) {
                lines[i].lineLeft.GetComponent<SpriteRenderer>().color = colorPairs[colorNr].colourLeft;
                lines[i].lineRight.GetComponent<SpriteRenderer>().color = colorPairs[colorNr].colourRight;
                colorNr++;
            }
        }
    }

    void GenerateColorPairList() {
        Color tmpColor = new Color(1.0f,0.0f,0.0f,0.5f);
        ColorPair tmpColorPair = new ColorPair(tmpColor, tmpColor);
        for(int i = 0; i < 1000; i++) {
            int random = Random.Range(0,5);
            switch(random) 
            {
                //ColorPair = (left, left);
                case 0:
                    //tmpColor = redColor;
                    tmpColorPair = new ColorPair(colorLeft, colorLeft);
                    break;

                //ColorPair = (left, left);
                case 1:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorLeft, colorLeft);
                    break;

                //ColorPair = (right, right);
                case 2:
                    //tmpColor = redColor;
                    tmpColorPair = new ColorPair(colorRight, colorRight);
                    break;

                //ColorPair = (right, right);
                case 3:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorRight, colorRight);
                    break;
                //ColorPair = (left, right);
                case 4:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorLeft, colorRight);
                    break;

                //ColorPair = (left, left);
                default:
                    //tmpColor = redColor;
                    tmpColorPair = new ColorPair(colorLeft, colorLeft);
                    break;
            }

            colorPairs.Add(tmpColorPair);
        }
    }
}


public class ColorPair
{
    public Color colourLeft;
    public Color colourRight;

    public ColorPair(Color left, Color right) 
    {
        colourLeft = left;
        colourRight = right;
    }
}