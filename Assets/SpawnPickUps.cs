using UnityEngine;
using System.Collections;

public class SpawnPickUps : MonoBehaviour {

    const int HP = 0;
    const int SP = 1;
    const int HP_SP = 2;

    Vector3 spawnPos;
    public GameObject pickUpPrefab;

	// Use this for initialization
	void Start () {
        spawnPos = Vector3.up * 5.5f;
        StartCoroutine(SpawnThings());
	}

    private void Spawn(int item)
    {
        spawnPos.x = Random.Range(-2.65f, 2.65f);
        GameObject spawned = (GameObject)Instantiate(pickUpPrefab, spawnPos, Quaternion.identity);
        switch(item)
        {
            case HP:
                spawned.GetComponent<Item>().hp = 1;
                break;
            case SP:
                spawned.GetComponent<Item>().sp = 1;
                break;
            case HP_SP:
                spawned.GetComponent<Item>().hp = 1;
                spawned.GetComponent<Item>().sp = 1;
                break;
            default: break;
        }
    }

    IEnumerator SpawnThings()
    {
        while (true)
        {// why did I even make that method, I wonder
            yield return WaitBeforeNextSpawn(Random.Range(10f, 45f));
            Spawn(Random.Range(0, 2));
        }
    }

    IEnumerator WaitBeforeNextSpawn(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
