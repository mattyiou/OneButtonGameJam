using UnityEngine;
using System.Collections;

public class PickUpController : MonoBehaviour {

    public float speed = 2f;
	
	// Update is called once per frame
	void Update () {
        this.transform.position += Vector3.down * speed * Time.deltaTime;
        if (this.transform.position.y < -6)
            Destroy(this.gameObject);
	}
}
