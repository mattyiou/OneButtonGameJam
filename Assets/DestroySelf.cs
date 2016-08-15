using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {

	public float timeToBeDestroyed = 5f;
	void selfDestroy() {
		Destroy (this.gameObject);
	}
	// Use this for initialization
	void Start () {
		Invoke ("selfDestroy", timeToBeDestroyed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
