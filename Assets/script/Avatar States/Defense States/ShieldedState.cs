using UnityEngine;
using System.Collections;

public class ShieldedState : DefenseState
{

    public override void ProccessDamage(int damage)
    {
		base.ProccessDamage(damage);

		//sp -= damage;
        sp -= 1;
		Debug.Log ("Lost shield");

        if (sp <= 0)
        {
            sp = 0;
            AvatarStateManager.defenseState = AvatarStateManager.unshieldedState;
        }
		UpdateState ();
    }

    public override void PickUp(int h, int s)
    {
        hp += h;
        if (hp > AvatarStateManager.MAX_HP)
            hp = AvatarStateManager.MAX_HP;
        sp += s;
        if (sp > AvatarStateManager.MAX_SP)
            sp = AvatarStateManager.MAX_SP;
		UpdateState ();
    }
}
