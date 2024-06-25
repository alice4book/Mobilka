using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom : MonoBehaviour
{
    //[SerializeField] public List<LoomLine> lines;
    public LoomLine loomLine;
    [SerializeField] private GameObject loomLinePrefab1;
    [SerializeField] private GameObject loomLinePrefab2;
    public GameObject parentOfLines;

    [SerializeField] public List<GameObject> lineSpawnSpots;
    public GameObject newLineSpawnSpotLeft;
    public GameObject newLineSpawnSpotRight;


    public List<LoomLine> lines;
    //public List<Color> colors;
    public List<ColorPair> colorPairs;
    
    [SerializeField] Color colorLeft;
    [SerializeField] Color colorLeft2;
    [SerializeField] Color colorRight;
    [SerializeField] Color colorRight2;

    [SerializeField] public Color outlineBase;

    //lerp
    public float moveDistance = 0.5f;
    public float moveDuration = 0.2f;
    private int lineNr = 0;

    public List<LoomLine> linesToDestroy;

    bool isMoving = false;

    public float distanceToMove = 0.4f;

    // Start is called before the first frame update
    void Start()
    {

        colorLeft = GlobalVar.color1;
        colorLeft.a = 0.5f;
        colorRight = GlobalVar.color2;
        colorRight.a = 0.5f;

        //colors = new List<Color>();
        colorPairs = new List<ColorPair>();
        lines = new List<LoomLine>();
        linesToDestroy = new List<LoomLine>();

        //GenerateColorList();
        GenerateLines();
        GenerateColorPairList();
        assignLineColors();

        GlobalVar.gameScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateLines()
    {
        for(int i = 0; i < lineSpawnSpots.Count; i++)
        {
            if(i % 2 == 0) {
                GameObject tmpLoomLine1 = Instantiate(loomLinePrefab1, lineSpawnSpots[i].transform.position, lineSpawnSpots[i].transform.rotation, parentOfLines.transform);
                lines.Add(tmpLoomLine1.GetComponent<LoomLine>());
                lineNr++;
            } 
            else {
                GameObject tmpLoomLine2 = Instantiate(loomLinePrefab2, lineSpawnSpots[i].transform.position, lineSpawnSpots[i].transform.rotation, parentOfLines.transform);
                lines.Add(tmpLoomLine2.GetComponent<LoomLine>());
                lineNr++;
            }

        }
    } 


    public void assignLineColors() {
        int colorNr = 0;
        if(colorPairs.Count > lines.Count) {
            for(int i = 0; i < lines.Count; i++) {
                lines[i].lineLeft.GetComponent<SpriteRenderer>().color = colorPairs[colorNr].colourLeft;
                lines[i].lineRight.GetComponent<SpriteRenderer>().color = colorPairs[colorNr].colourRight;
                lines[i].outlineL.GetComponent<SpriteRenderer>().color = colorPairs[colorNr].colourLeft * outlineBase;
                lines[i].outlineR.GetComponent<SpriteRenderer>().color = colorPairs[colorNr].colourRight * outlineBase;
                colorNr++;
            }
        }
    }

    void GenerateColorPairList() {

        int rangeNumber = 12;
        int firstNumber = 0;
        Color tmpColor = new Color(1.0f,0.0f,0.0f,0.5f);

        ColorPair tmpColorPair = new ColorPair(tmpColor, tmpColor);
        if(GlobalVar.fromMenu == true) {
            //Debug.Log("First GAME");
            firstNumber = 5;
            tmpColorPair = new ColorPair(colorLeft2, colorLeft2);
            colorPairs.Add(tmpColorPair);
            tmpColorPair = new ColorPair(colorRight2, colorRight2);
            colorPairs.Add(tmpColorPair);
            tmpColorPair = new ColorPair(colorLeft, colorLeft);
            colorPairs.Add(tmpColorPair);
            tmpColorPair = new ColorPair(colorRight, colorRight);
            colorPairs.Add(tmpColorPair);
            tmpColorPair = new ColorPair(colorLeft, colorRight);
            colorPairs.Add(tmpColorPair);
        }

        int random = 0;
        for(int i = firstNumber; i < 1000; i++) {
            if(i < 50)
            {
                rangeNumber = 5;
            } 
            else if(i < 75)
            {
                rangeNumber = 10;
            }
            else
            {
                rangeNumber = 12;
            }
            random = Random.Range(0,rangeNumber);
            switch(random) 
            {
                //ColorPair = (left, left); color1
                case 0:
                    //tmpColor = redColor;
                    tmpColorPair = new ColorPair(colorLeft, colorLeft);
                    break;

                //ColorPair = (left, left); color1
                case 1:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorLeft, colorLeft);
                    break;

                //ColorPair = (right, right); color2
                case 2:
                    //tmpColor = redColor;
                    tmpColorPair = new ColorPair(colorRight, colorRight);
                    break;

                //ColorPair = (right, right); color2
                case 3:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorRight, colorRight);
                    break;
  
                //ColorPair = (left, right); color1 color2
                case 4:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorLeft, colorRight);
                    break;

                //ColorPair = (left, left);  color3 color3
                case 5:
                    //tmpColor = redColor;
                    tmpColorPair = new ColorPair(colorLeft2, colorLeft2);
                    break;

                //ColorPair = (left, left); color3 color3
                case 6:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorLeft2, colorLeft2);
                    break;

                //ColorPair = (right, right); color4 color4
                case 7:
                    //tmpColor = redColor;
                    tmpColorPair = new ColorPair(colorRight2, colorRight2);
                    break;

                //ColorPair = (right, right); color4 color4
                case 8:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorRight2, colorRight2);
                    break;

                //ColorPair = (left, right); color3 color4
                case 9:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorLeft2, colorRight2);
                    break;

                //ColorPair = (left, right); color1 color4
                case 10:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorLeft, colorRight2);
                    break;
                
                //ColorPair = (left, right); color3 color2
                case 11:
                    //tmpColor = blueColor;
                    tmpColorPair = new ColorPair(colorLeft2, colorRight);
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

    public void MoveLines()
    {
        if(!isMoving) 
        {
            isMoving = true;
            lines[0].Idle();
            SpawnLine();
            linesToDestroy.Add(lines[0]);
            StartCoroutine(MoveLinesCoroutine());
            lines.RemoveAt(0);
            DestroyLines();
            lines[0].CurrentLine();
        }
    }

    private IEnumerator MoveLinesCoroutine()
    {
        Vector3 startPosition = parentOfLines.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y - distanceToMove, startPosition.z);
        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
            parentOfLines.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        parentOfLines.transform.position = endPosition;
        isMoving = false;
    }

    void SpawnLine()
    {

        LoomLine tmpLoomLine;
        if(lineNr % 2 == 0) {
            tmpLoomLine = Instantiate(loomLinePrefab1.GetComponent<LoomLine>(), newLineSpawnSpotLeft.transform.position, newLineSpawnSpotLeft.transform.rotation, parentOfLines.transform);
        } else {
            tmpLoomLine = Instantiate(loomLinePrefab2.GetComponent<LoomLine>(), newLineSpawnSpotRight.transform.position, newLineSpawnSpotRight.transform.rotation, parentOfLines.transform);
        }

        lineNr++;
        //Debug.Log(lineNr);
        tmpLoomLine.lineLeft.GetComponent<SpriteRenderer>().color = colorPairs[lineNr].colourLeft;
        tmpLoomLine.lineRight.GetComponent<SpriteRenderer>().color = colorPairs[lineNr].colourRight;
        tmpLoomLine.outlineL.GetComponent<SpriteRenderer>().color = colorPairs[lineNr].colourLeft * outlineBase;
        tmpLoomLine.outlineR.GetComponent<SpriteRenderer>().color = colorPairs[lineNr].colourRight * outlineBase;
        lines.Add(tmpLoomLine);
    }

    void DestroyLines()
    {
        
        //Debug.Log("DestoryLines");
        if(linesToDestroy.Count > 10)
        {
            LoomLine tmpLine = linesToDestroy[0];
            linesToDestroy.RemoveAt(0);
            Destroy(tmpLine.gameObject);
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