using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loom : MonoBehaviour
{
    //[SerializeField] public List<LoomLine> lines;
    public LoomLine loomLine;
    public GameObject parentOfLines;

    [SerializeField] public List<GameObject> lineSpawnSpots;
    public GameObject newLineSpawnSpotLeft;
    public GameObject newLineSpawnSpotRight;


    public List<LoomLine> lines;
    //public List<Color> colors;
    public List<ColorPair> colorPairs;
    
    [SerializeField] Color colorLeft;
    [SerializeField] Color colorRight;

    //lerp
    public float moveDistance = 0.5f;
    public float moveDuration = 0.2f;
    private int lineNr = 0;

    public List<LoomLine> linesToDestroy;

    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
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
            LoomLine tmpLoomLine = Instantiate(loomLine, lineSpawnSpots[i].transform.position, lineSpawnSpots[i].transform.rotation, parentOfLines.transform);
            lines.Add(tmpLoomLine);
            lineNr++;
        }
    } 


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

        int firstNumber = 0;
        Color tmpColor = new Color(1.0f,0.0f,0.0f,0.5f);

        ColorPair tmpColorPair = new ColorPair(tmpColor, tmpColor);
        if(GlobalVar.numberOfGames == 0) {
            //Debug.Log("First GAME");
            firstNumber = 3;
            tmpColorPair = new ColorPair(colorLeft, colorLeft);
            colorPairs.Add(tmpColorPair);
            tmpColorPair = new ColorPair(colorRight, colorRight);
            colorPairs.Add(tmpColorPair);
            tmpColorPair = new ColorPair(colorLeft, colorRight);
            colorPairs.Add(tmpColorPair);
        }

        for(int i = firstNumber; i < 1000; i++) {
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
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y - 0.5f, startPosition.z);
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
            tmpLoomLine = Instantiate(loomLine, newLineSpawnSpotLeft.transform.position, newLineSpawnSpotLeft.transform.rotation, parentOfLines.transform);
        } else {
            tmpLoomLine = Instantiate(loomLine, newLineSpawnSpotRight.transform.position, newLineSpawnSpotRight.transform.rotation, parentOfLines.transform);
        }

        lineNr++;
        //Debug.Log(lineNr);
        tmpLoomLine.lineLeft.GetComponent<SpriteRenderer>().color = colorPairs[lineNr].colourLeft;
        tmpLoomLine.lineRight.GetComponent<SpriteRenderer>().color = colorPairs[lineNr].colourRight;
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