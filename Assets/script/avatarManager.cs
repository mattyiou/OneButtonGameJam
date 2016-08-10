using UnityEngine;
using System.Collections;

public class avatarManager : MonoBehaviour {

    private float currentForce;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private AttackState attackState;
    private DefenseState defenseState;

	// Use this for initialization
	void Start () {
        currentForce = 3f;
        rb = this.GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
        attackState = AvatarStateManager.attackState;
        defenseState = AvatarStateManager.defenseState;
        defenseState = AvatarStateManager.shieldedState;
        defenseState.EnterState(AvatarStateManager.MAX_HP);
	}
	
	// Update is called once per frame
	void Update () {
        //attackState.UpdateState();
        //defenseState.UpdateState();
        if (Input.GetKeyDown(KeyCode.E)) currentForce *= -1;
        velocity.x = currentForce;
        rb.velocity = velocity;
	}

    public void Attack(Collider2D col)
    {
        attackState.Attack(col);
    }

    public void GetHit(int damage)
    {
        defenseState.GetHit(damage);
    }

    public void PickUp(int hp, int sp)
    {
        defenseState.PickUp(hp, sp);
    }

}
