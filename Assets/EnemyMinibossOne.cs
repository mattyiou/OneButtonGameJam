using UnityEngine;
using System.Collections;

public class EnemyMinibossOne : MonoBehaviour {

    private float phase = 0;
    private float radius = 1f;
    private float speed = 1f;
    private bool invert = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += AIMovePatterns.FigureEight(ref phase, ref invert, radius, speed);
	}
}
