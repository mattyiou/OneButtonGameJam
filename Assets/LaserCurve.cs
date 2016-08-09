using UnityEngine;
using System.Collections;

public class LaserCurve : MonoBehaviour {

    private LineRenderer lineRenderer;
    private const float initRebound = 200;
    private float reboundSpeed;
    private float reboundModifier;
    private int vertexCount;
    private Vector3[] positionArray;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        reboundSpeed = initRebound;
        reboundModifier = 0.95f;
        vertexCount = 100;
        positionArray = new Vector3[vertexCount];
        float deltaY = 10f / vertexCount;
        float headY = this.transform.position.y;
        for (int i = 0; i < vertexCount; i++)
        {
            positionArray[i].y = headY + deltaY * i;
        }
        lineRenderer.SetVertexCount(vertexCount);
    }
	
	// Update is called once per frame
	void Update () {
        positionArray[0] = this.transform.position;
        reboundSpeed = initRebound;
        for (int i = 1; i < vertexCount; i++)
        {
            if (positionArray[i].x != positionArray[0].x)
            {
                positionArray[i].x = Mathf.Lerp(positionArray[i].x, positionArray[0].x, Time.deltaTime * reboundSpeed);
                reboundSpeed *= reboundModifier;
            }
        }
        lineRenderer.SetPositions(positionArray);
	}
}
