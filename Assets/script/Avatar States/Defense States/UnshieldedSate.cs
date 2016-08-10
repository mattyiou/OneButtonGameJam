using UnityEngine;
using System.Collections;

public class UnshieldedSate : ShieldedState{

    public override void GetHit(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            AvatarStateManager.defenseState = AvatarStateManager.deadState;
        }
    }

    public override void PickUp(int h, int s)
    {
        hp += h;
        if (hp > AvatarStateManager.MAX_HP)
            hp = AvatarStateManager.MAX_HP;
        if (s > 0)
        {
            AvatarStateManager.defenseState = AvatarStateManager.shieldedState;
            AvatarStateManager.defenseState.EnterState(hp);
            AvatarStateManager.defenseState.PickUp(0, s);
        }  
    }

}
