using UnityEngine;
using System.Collections;

public class EnemySuicider : MonoBehaviour {

	const int ENEMY_SUICIDER = 0;
	const int ENEMY_ZIGZAG = 1;
	const int ENEMY_CIRCLE = 2;

	public int enemyType = -1;
	public int speedMultiplier=1;
	private Vector3 speed = Vector3.zero;
	private Vector3 center = Vector3.zero;
	private float currentAngle = 0;
	private float radius = 1f;
	// Use this for initialization
	void Start () {
		if (enemyType == -1) {
			enemyType = Random.Range (0,2);
		}
		enemyType = ENEMY_ZIGZAG;
		switch (enemyType) {
		case ENEMY_SUICIDER:
			Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;
			speed = new Vector3((target.x - transform.position.x)/100*speedMultiplier,(target.y - transform.position.y)/100*speedMultiplier,0);
			Vector3 vectorToTarget = target - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = q;
			break;
		case ENEMY_ZIGZAG:
		case ENEMY_CIRCLE:
			center = transform.position;
			currentAngle = 0;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (enemyType) {
			case ENEMY_SUICIDER:
			transform.position = transform.position + speed;
			break;
		case ENEMY_ZIGZAG:
			transform.position = new Vector3 (center.x+Mathf.Cos(currentAngle) * radius,center.y,0 );
			center = new Vector3(center.x,center.y-.05f,center.z);
			currentAngle += .1f;
			break;
		case ENEMY_CIRCLE:
			transform.position = new Vector3 (center.x+Mathf.Cos(currentAngle) * radius,center.y+Mathf.Sin(currentAngle) * radius,0 );
			center = new Vector3(center.x,center.y-.05f,center.z);
			currentAngle += .1f;
			break;
		}
	}
}
