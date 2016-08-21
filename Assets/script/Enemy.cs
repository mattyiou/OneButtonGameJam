using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour {

	public GameObject explosionParticle;

	public float hp = 20;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.Equals (null)) {
			//laser collision
			hp-=.5f;
		} else if (col.tag.Equals ("Player")) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<avatarManager> ().GetHit (5);
		}
		if (hp <= 0) {
			Instantiate (explosionParticle, transform.position, explosionParticle.transform.rotation);
			Destroy (this.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
