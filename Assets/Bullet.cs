using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Vector3 speed = Vector3.zero;

	// Use this for initialization
	void Start () {
		if (speed == Vector3.zero) {
			speed = new Vector3(0f,-.05f,0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += speed;
	}
}
