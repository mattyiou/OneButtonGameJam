using UnityEngine;
using System.Collections;

public class LaserCurve : MonoBehaviour {

    private LineRenderer lineRenderer;
    private const float initRebound = 100;
    private float reboundSpeed;
    private float reboundModifier;
    private int vertexCount;
    private int currentEndOfLaser;
    private int layerMask;
    private Vector3[] positionArray;
    private avatarManager player;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        reboundSpeed = initRebound;
        reboundModifier = 0.98f;
        vertexCount = 100;
        currentEndOfLaser = vertexCount;
        layerMask = 1 << 8;
        positionArray = new Vector3[vertexCount];
        float deltaY = 10f / vertexCount;
        float headY = this.transform.position.y;
        for (int i = 0; i < vertexCount; i++)
        {
            positionArray[i].y = headY + deltaY * i;
        }
        lineRenderer.SetVertexCount(vertexCount);
        player = GetComponent<avatarManager>();
    }
	
	// Update is called once per frame
	void Update () {
        positionArray[0] = this.transform.position;
        reboundSpeed = initRebound;
        int endOfLaser = RaycastLines();
        if (endOfLaser < currentEndOfLaser) currentEndOfLaser = endOfLaser;
        lineRenderer.SetVertexCount(currentEndOfLaser);
        for (int i = 1; i < currentEndOfLaser; i++)
        {
            if (positionArray[i].x != positionArray[0].x)
            {
                //positionArray[i].x = Mathf.Lerp(positionArray[i].x, positionArray[0].x, Time.deltaTime * reboundSpeed);
                positionArray[i].x = Mathf.SmoothStep(positionArray[i].x, positionArray[0].x, Time.deltaTime * reboundSpeed);
                reboundSpeed *= reboundModifier;
            }
        }

        for (int i = 0; i < currentEndOfLaser; i++)
        {
            lineRenderer.SetPosition(i, positionArray[i]);
        }

        if (currentEndOfLaser < vertexCount)
        {
            currentEndOfLaser++;
            positionArray[currentEndOfLaser-1].x = positionArray[currentEndOfLaser - 2].x;
        }
	}

    int RaycastLines()
    {
        int numRays = Mathf.FloorToInt(currentEndOfLaser / 5);
        for (int i = 0; i < numRays; i++)
        {
            int start = i * 5;
            int end = i * 5 + 4;
            if (i == numRays - 1) end = currentEndOfLaser-2;
            RaycastHit2D hit;
            hit = Physics2D.Raycast(positionArray[start], positionArray[end] - positionArray[start],
                (positionArray[start] - positionArray[end]).magnitude, layerMask);
            if (hit.collider != null)
            {
                for (int j = start; j < end; j++)
                {
                    hit = Physics2D.Raycast(positionArray[j], positionArray[j+1] - positionArray[j],
                        (positionArray[j] - positionArray[j+1]).magnitude, layerMask);
                    if (hit.collider != null)
                    {
                        //player.Attack(hit.collider);
                        hit.collider.SendMessage("OnTriggerEnter2D", GetComponent<BoxCollider2D>(), SendMessageOptions.DontRequireReceiver);
                        return j + 1;
                    }
                }
            }
        }
        return vertexCount;
    }
}
