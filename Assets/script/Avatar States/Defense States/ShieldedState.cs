using UnityEngine;
using System.Collections;

public class ShieldedState : DefenseState
{

    public override void GetHit(int damage)
    {
        sp -= damage;
        if (sp <= 0)
        {
            sp = 0;
            AvatarStateManager.defenseState = AvatarStateManager.unshieldedState;
            AvatarStateManager.defenseState.EnterState(hp);
        }
    }

    public override void PickUp(int h, int s)
    {
        hp += h;
        if (hp > AvatarStateManager.MAX_HP)
            hp = AvatarStateManager.MAX_HP;
        sp += s;
        if (sp > AvatarStateManager.MAX_SP)
            sp = AvatarStateManager.MAX_SP;
    }
}
