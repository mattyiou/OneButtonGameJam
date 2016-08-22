using UnityEngine;
using System.Collections;

public class SmallRandomMovement : Enemy {

	public Vector3 center = new Vector3(0,2.5f,0);
	public float smallVariation = 0.8f;
	public float speedX;
	public float speedY;
	public float timeToAppear = .5f;
	private float currentTime =0f;
	private float timeToChangeCourse = 2f;
	Vector3 newPosition;
	float newPositionX;
	float newPositionY;
	// Use this for initialization
	void Start () {
		newPosition = new Vector3 (this.transform.position.x + Random.Range(-smallVariation,smallVariation),this.transform.position.y + Random.Range(-smallVariation,smallVariation),0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentTime += Time.fixedDeltaTime;
		if (currentTime > timeToChangeCourse) {
			currentTime = 0f;
			newPosition = new Vector3 (center.x + Random.Range(-smallVariation,smallVariation),center.y + Random.Range(-smallVariation,smallVariation),0);
		}
		newPositionX = Mathf.SmoothDamp (this.transform.position.x, newPosition.x, ref speedX, timeToAppear);
		newPositionY = Mathf.SmoothDamp (this.transform.position.y, newPosition.y, ref speedY, timeToAppear);
		this.transform.position = new Vector3(newPositionX,newPositionY,this.transform.position.z);

	}
}
