﻿using UnityEngine;
using System.Collections;

public class avatarManager : MonoBehaviour {

    const int FRAMES_TILL_HELD = 2;
    const int FRAMES_TILL_HELD_LONGER = 10;
    private float currentForce;
    private float lastCurrentForce;
    private Rigidbody2D rb;
    private Vector2 velocity;
    public float laserDamage = 10f;
    private int heldCount = 0;
	public GameObject explodeParticle;
	public GameObject gameOverScreen;
	public GameObject playerShield;

	public GameObject[] hearts;
	public GameObject[] shields;

	private float invulTiltTime = 0f;

    private enum TapState
    {
        HELD,
        HELD_LONGER,
        DOWN,
        UP
    };
    private TapState tapState;

	// Use this for initialization
	void Start () {
        tapState = TapState.UP;
        currentForce = 3f;
        lastCurrentForce = currentForce;
        rb = this.GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
        AvatarStateManager.defenseState = AvatarStateManager.shieldedState;
		AvatarStateManager.unshieldedState.InitializeHpSp(4, 2,this.gameObject);
        //defenseState.EnterState(AvatarStateManager.MAX_HP);

	}

	public void die() {
		Instantiate (explodeParticle, this.transform.position, explodeParticle.transform.rotation);
		gameOverScreen.SetActive (true);
		Destroy (this.gameObject);
	}
	
	void FixedUpdate () {
        velocity.x = currentForce;
        rb.velocity = velocity;
	}

    void Update()
    {
        //attackState.UpdateState();
        AvatarStateManager.defenseState.UpdateRegen();
        if (currentForce != 0)
            lastCurrentForce = currentForce;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(tapState);
            //Debug.Log(Time.realtimeSinceStartup);
            tapState = TapState.DOWN;
            currentForce = lastCurrentForce;
            currentForce *= -1;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (tapState == TapState.HELD)
                currentForce = lastCurrentForce;
            else if (tapState == TapState.HELD_LONGER)
                currentForce = -lastCurrentForce;
            tapState = TapState.UP;
            heldCount = 0;
        }
        else if (tapState == TapState.DOWN)
        {
            //Debug.Log(tapState);
            //Debug.Log(Time.realtimeSinceStartup);
            heldCount++;
            if (heldCount == FRAMES_TILL_HELD)
            {
                tapState = TapState.HELD;
                currentForce = 0f;
            }
        }
        else if (tapState == TapState.HELD)
        {
            heldCount++;
            if (heldCount == FRAMES_TILL_HELD_LONGER)
            {
                tapState = TapState.HELD_LONGER;
            }
        }

		//if its invul, then appear and disappear
		if (AvatarStateManager.defenseState.getIsInvulnerable ()) {
			if (Time.time > invulTiltTime + .2f) {
				invulTiltTime = Time.time;
				if (GetComponent<SpriteRenderer> ().color.a == 0) {
					GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);
				} else {
					GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
				}
			}
		} else {
			GetComponent<SpriteRenderer> ().color = new Color(1,1,1,1);
		}
        
    }

    public void Attack(Collider2D col)
    {
        //col.gameObject.GetComponent<EnemyHP>().LoseHP(laserDamage * Time.deltaTime);
        // use this if w end up using more complex states
        // attackState.Attack(col);
    }

    public void GetHit(int damage)
    {
        AvatarStateManager.defenseState.GetHit(damage);
    }

    public void PickUp(int hp, int sp)
    {
        AvatarStateManager.defenseState.PickUp(hp, sp);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name.StartsWith("PickUp"))
        {
            Item item = col.GetComponent<Item>();
            PickUp(item.hp, item.sp);
        }
    }

	public void ShowHideShield(bool shieldVisibility) {
		playerShield.SetActive (shieldVisibility);
	}
}
