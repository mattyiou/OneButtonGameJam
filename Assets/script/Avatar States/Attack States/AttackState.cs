using UnityEngine;
using System.Collections;

public class AttackState {
    public virtual void UpdateState() { }
    public virtual void Attack(Collider2D col) { }
}
