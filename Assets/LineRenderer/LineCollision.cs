using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineController), typeof(PolygonCollider2D))]

public class LineCollision : MonoBehaviour
{
    LineController lc;

    List<Vector2> colliderPoints = new List<Vector2>();

    //The collider for the line
    PolygonCollider2D polygonCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        lc = GetComponent<LineController>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //colliderPoints = CalculateColliderPoints();
        //polygonCollider2D.SetPath(0, colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));

        //Get All positions on the line renderer
        Vector3[] positions = lc.GetPositions();

        if(positions.Length >= 2) {

            //Get the number of line between two points
            int numberOfLines = positions.Length - 1;

            //Make as many paths for wach different line as we have lines
            polygonCollider2D.pathCount = numberOfLines;

            //Get collider points for each different line as we have lines
            for (int i = 0; i < numberOfLines; i++) {

                //Get the two next points
                List<Vector2> currentPositions = new List<Vector2> {
                    positions[i],
                    positions[i+1]
                };

                List<Vector2> currentColliderPoints = CalculateColliderPoints(currentPositions);
                polygonCollider2D.SetPath(i, currentColliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
            }
        }
        else {
            polygonCollider2D.pathCount = 0;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        if (colliderPoints != null) colliderPoints.ForEach(p => Gizmos.DrawSphere(p, 0.1f));
    }

    private List<Vector2> CalculateColliderPoints(List<Vector2> positions) {

        //Get the Wisth of the Line
        float width = lc.GetWidth();

        //m = (y2 - y1) / (x2 - x1)
        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        //Calculate the Offset from each point to the collision vertex
        Vector2[] offsets = new Vector2[2];
        offsets[0] = new Vector2(-deltaX, deltaY);
        offsets[1] = new Vector2(deltaX, -deltaY);

        //Generate the Colliders Vertices
        List<Vector2> colliderPositions = new List<Vector2> {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };

        return colliderPositions;
    }
}
