using UnityEngine;
using System.Collections;

public class UnshieldedSate : ShieldedState {

    public override void GetHit(int damage)
    {
        base.GetHit(damage);
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            AvatarStateManager.defenseState = AvatarStateManager.deadState;
            //Debug.Log("YOU DIED!");
			player.GetComponent<avatarManager>().die();
		}
    }

    public override void PickUp(int h, int s)
    {
        base.PickUp(h, s);
        if (s > 0)
            AvatarStateManager.defenseState = AvatarStateManager.shieldedState;
    }

}
