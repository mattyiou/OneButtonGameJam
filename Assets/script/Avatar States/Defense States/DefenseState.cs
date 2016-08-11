using UnityEngine;
using System.Collections;

public class DefenseState {

    protected int hp;
    protected int sp;

    public virtual void UpdateState() { }
    public virtual void GetHit(int damage) { }
    public virtual void PickUp(int h, int s) { }
    public virtual void EnterState(int h)
    {
        hp = h;
    }
}
