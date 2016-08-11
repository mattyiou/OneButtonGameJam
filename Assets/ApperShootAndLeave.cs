using UnityEngine;
using System.Collections;

public class ApperShootAndLeave : MonoBehaviour {

	const int PHASE_APPEAR=0;
	const int PHASE_SHOOT=1;
	const int PHASE_LEAVE=2;



	public int place=0; //can be from 1 to 5
	private int phase;
	private float speed;
	public float timeToAppear = 1f;
	public float timeToShoot = 2f;
	public float timeToLeave = 1f;
	private float currentTime = 0f;
	// Use this for initialization
	void Start () {
		phase = PHASE_APPEAR;
		if (place == 0) {
			place = Random.Range(1,6); //maximum is not included
		}
		if (Random.Range (0, 10) > 5) {
			this.transform.position = new Vector3 (-100, this.transform.position.y, 0);
		} else {
			this.transform.position = new Vector3 (Screen.width + 100, this.transform.position.y, 0);		
		}

	}
	
	// Update is called once per delta seconds
	void FixedUpdate () {
		currentTime = currentTime + Time.fixedDeltaTime;
		float newPosition;
		switch (phase) {
		case PHASE_APPEAR:
			newPosition = Mathf.SmoothDamp(this.transform.position.x,(place)-3.5f,ref speed,timeToAppear);
			this.transform.position = new Vector3(newPosition,this.transform.position.y,this.transform.position.z);
			if (currentTime > timeToAppear*8) {
				phase = PHASE_SHOOT;
				currentTime=0;
			}
			break;
		case PHASE_SHOOT:
			if (currentTime > timeToShoot) {
				phase=PHASE_LEAVE;
				currentTime=0;
			}
			break;
		case PHASE_LEAVE:
			newPosition = Mathf.SmoothDamp(this.transform.position.x,50,ref speed,timeToLeave);
			this.transform.position = new Vector3(newPosition,this.transform.position.y,this.transform.position.z);
			if (currentTime > timeToLeave*8) {
				Destroy(this.gameObject);
			}

			break;
		}
	}
}
