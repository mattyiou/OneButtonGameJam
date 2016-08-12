using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour {

    public float hp;
    
    public void LoseHP(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            // DO SOMETHING
            // idk EXPLODE?
            // pixels to pixels
            // free is the memory
            // for your pixels are gone
        }
    }
}
