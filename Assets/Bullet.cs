using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Vector3 speed = Vector3.zero;

	void removeSelf() {
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		Invoke ("removeSelf", 9f);
		if (speed == Vector3.zero) {
			speed = new Vector3(0f,-.05f,0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += speed;
	}
}
