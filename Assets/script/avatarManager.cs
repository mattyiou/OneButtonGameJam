using UnityEngine;
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
        AvatarStateManager.unshieldedState.InitializeHpSp(1, 0);
        //defenseState.EnterState(AvatarStateManager.MAX_HP);
	}
	
	void FixedUpdate () {
        velocity.x = currentForce;
        rb.velocity = velocity;
	}

    void Update()
    {
        //attackState.UpdateState();
        //defenseState.UpdateState();
        if (currentForce != 0)
            lastCurrentForce = currentForce;
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log(tapState);
            //Debug.Log(Time.realtimeSinceStartup);
            tapState = TapState.DOWN;
            currentForce = lastCurrentForce;
            currentForce *= -1;
        }
        else if (Input.GetKeyUp(KeyCode.E))
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
}
