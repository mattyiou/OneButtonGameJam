using UnityEngine;
using System.Collections;

public class DefenseState {

    protected static int hp;
    protected static int sp;
    protected static bool isInvulnerable = false;

    public void InitializeHpSp(int h, int s)
    {
        hp = h;
        sp = s;
    }

    public virtual void UpdateState() { }
    public virtual void GetHit(int damage)
    {
        if (isInvulnerable)
            return;
    }
    public virtual void PickUp(int h, int s)
    {
        hp += h;
        sp += s;
        if (hp > AvatarStateManager.MAX_HP)
            hp = AvatarStateManager.MAX_HP;
        if (s > AvatarStateManager.MAX_SP)
            sp = AvatarStateManager.MAX_SP;
    }
    public virtual void EnterState(int h)
    {
        //hp = h;
    }
    protected IEnumerator InvulnerableFor(float time)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(time);
        isInvulnerable = false;
    }
}
