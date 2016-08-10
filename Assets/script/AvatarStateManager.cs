using UnityEngine;
using System.Collections;

public static class AvatarStateManager {

    public static DefenseState defenseState;
    public static ShieldedState shieldedState = new ShieldedState();
    public static UnshieldedSate unshieldedState = new UnshieldedSate();
    public static DeadState deadState = new DeadState();

    public static AttackState attackState;

    public static int MAX_SP = 2;
    public static int MAX_HP = 4;
}
