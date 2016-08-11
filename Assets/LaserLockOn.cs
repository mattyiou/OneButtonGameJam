using UnityEngine;
using System.Collections;

public class LaserLockOn : MonoBehaviour {

    private LineRenderer lineRenderer;
    private Vector3[] vertexArray;
    private int vertexCount;
    private int currentEndOfLaser;
    private Vector2 enemyPos;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        vertexCount = 100;
        vertexArray = new Vector3[vertexCount];
        float deltaY = 10f / vertexCount;
        float headY = this.transform.position.y;
        for (int i = 0; i < vertexCount; i++)
        {
            vertexArray[i].y = headY + deltaY * i;
        }
        currentEndOfLaser = 50;
        lineRenderer.SetVertexCount(currentEndOfLaser);
        enemyPos = new Vector2(-2f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        float dx = this.transform.position.x - enemyPos.x;
        float rL = -(Mathf.PI / 2) / (currentEndOfLaser - 1);
        vertexArray[0].x = this.transform.position.x;
        for (int i = 1; i < currentEndOfLaser; i++)
        {
            //type 1
            float newX = dx * Mathf.Sin(rL * i) + this.transform.position.x;
            vertexArray[i].x = Mathf.SmoothStep(vertexArray[i].x, newX, Time.deltaTime * 150);
            //type 2
            //float newX = dx / (currentEndOfLaser - 1);
            //newX *= i * -1;
            //newX += this.transform.position.x;
            //vertexArray[i].x = Mathf.SmoothStep(vertexArray[i].x, newX, Time.deltaTime * 150);
        }

        for (int i = 0; i < currentEndOfLaser; i++)
        {
            lineRenderer.SetPosition(i, vertexArray[i]);
        }
    }
}
