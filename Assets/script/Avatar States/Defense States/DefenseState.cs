using UnityEngine;
using System.Collections;

public class DefenseState {

    protected static int hp;
    protected static int sp;
    protected static bool isInvulnerable = false;
	protected static GameObject player;
	protected static GameObject[] hearts;
	protected static GameObject[] shields;
    protected static int regenStartHP;
    protected static int regenStartSP;
    protected static float regenTime = 5f;
    protected static float currentRegenTime = 0f;
	public bool getIsInvulnerable() {
		return isInvulnerable;
	}

	public void InitializeHpSp(int h, int s, GameObject p)
    {
        hp = h;
        sp = s;
		player = p;
		hearts = GameObject.FindGameObjectsWithTag ("Heart Icon");
		shields = GameObject.FindGameObjectsWithTag ("Shield Icon");
        isInvulnerable = false;
    }

    public virtual void UpdateState() { 
		/*for (int i = hearts.Length-1; i >= 0; i--) {
			hearts [i].SetActive (i < hp);
		}
		for (int i = shields.Length-1; i >= 0; i--) {
			shields [i].SetActive (i < sp);
		}*/

        
        
		for (int i = 0; i < hearts.Length; i++) {
			hearts [hearts.Length - 1 - i].SetActive (i < hp);
		}
		for (int i = 0; i < shields.Length; i++) {
			shields [shields.Length - 1 - i].SetActive (i < sp);
		}

		player.GetComponent<avatarManager> ().ShowHideShield (sp > 0);
	}

    public virtual void UpdateRegen()
    {
        if (currentRegenTime == 0f)
        {
            regenStartHP = hp;
            regenStartSP = sp;
        }
        currentRegenTime += Time.deltaTime;
        if (currentRegenTime >= regenTime)
        {
            if (regenStartHP == hp && regenStartSP == sp && sp != AvatarStateManager.MAX_SP)
                sp++;
            if (sp == 1)
                AvatarStateManager.defenseState = AvatarStateManager.shieldedState;
            currentRegenTime = 0f;
            UpdateState();
        }
    }

    public virtual void GetHit(int damage)
    {
        if (isInvulnerable)
            return;
		UpdateState ();
		ProccessDamage (damage);
		player.GetComponent<MonoBehaviour> ().StartCoroutine (InvulnerableFor (2));
    }

	public virtual void ProccessDamage(int damage) {

	}

    public virtual void PickUp(int h, int s)
    {
        hp += h;
        sp += s;
        if (hp > AvatarStateManager.MAX_HP)
            hp = AvatarStateManager.MAX_HP;
        if (s > AvatarStateManager.MAX_SP)
            sp = AvatarStateManager.MAX_SP;
		UpdateState ();
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
