using UnityEngine;
using System.Collections;

public class avatarManager : MonoBehaviour {

    private float constantLeftForce;
    private float constantLeftForceMultipler;
    private float defaultLeftForce;
    private float appliedForce;
    private float tapToForceMultiplier; // might be better if its a function, with exp alg
    private float currentForce;
    private const float maxForce = 4;
    private const float minForce = -2;
    private Rigidbody2D rb;
    private Vector2 velocity;

	// Use this for initialization
	void Start () {
        constantLeftForce = -1f;
        constantLeftForceMultipler = 1.07f;
        defaultLeftForce = -1f;
        appliedForce = 0f;
        tapToForceMultiplier = 77f;
        currentForce = 0f;
        rb = this.GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentForce < 0 && constantLeftForce > minForce)
            constantLeftForce *= constantLeftForceMultipler;
        if (currentForce > 0 && constantLeftForce < defaultLeftForce)
            constantLeftForce = defaultLeftForce;
	}

    public void RecieveTaps(float taps, float frames)
    {
        appliedForce = taps / frames * tapToForceMultiplier;
        currentForce = constantLeftForce + appliedForce;
        if (currentForce > maxForce) currentForce = maxForce;
        else if (currentForce < minForce) currentForce = minForce;
        velocity.x = currentForce;
        rb.velocity = velocity;
    }
}
