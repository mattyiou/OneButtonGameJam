using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Vector3 speed = Vector3.zero;
	public GameObject pewEffect;

	void removeSelf() {
		Destroy (this.gameObject);
	}

	void OnCollisionEnter(Collision col) {
		col.collider.GetComponent<avatarManager>().GetHit(2);
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		Instantiate (pewEffect, transform.position, transform.rotation);
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
