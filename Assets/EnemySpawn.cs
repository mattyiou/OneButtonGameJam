using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{

    const int RNDS_FOR_BOSS_ONE = 10;
    const int RNDS_FOR_BOSS_TWO = 20;

    public GameObject laserShootingShip;
    public GameObject suiciderShip;
    public GameObject formationShooterShip;
    public GameObject shooterBoss;
    public GameObject laserBoss;
    public float currentTime;
    public float timePerEnemy = 5f;
    private int roundCount = 0;
    private bool isBoss = false;
    private bool doubleDifficulty = false;
    private GameObject curBoss;
    private float bossMaxHP;
    private bool bossMobsSpawned = false;
    // Use this for initialization
    void Start()
    {
        spawnShooters();
    }

    public void restartGame()
    {
        Application.LoadLevel(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (roundCount == RNDS_FOR_BOSS_ONE && !isBoss)
        {
            isBoss = true;
            spawnShooterBoss();
        }
        else if (roundCount == RNDS_FOR_BOSS_TWO && !isBoss)
        {
            isBoss = true;
            spawnLaserBoss();
        }
        currentTime = currentTime + Time.fixedDeltaTime;
        if (timePerEnemy < currentTime && !isBoss)
        {
            roundCount += 1;
            currentTime = 0;
            int whatToSpawn = Mathf.RoundToInt(Random.Range(0, 2.98f) - .49f);
            switch (whatToSpawn)
            {
                case 0: spawnShooters();
                    break;
                case 1: spawnLaserEnemies();
                    break;
                case 2: spawnSuiciders();
                    break;
            }
            if (doubleDifficulty)
            {
                int toSpawnTwo = -1;
                while (toSpawnTwo != whatToSpawn)
                    toSpawnTwo = Mathf.RoundToInt(Random.Range(0, 2.98f) - .49f);
                switch (whatToSpawn)
                {
                    case 0: spawnShooters();
                        break;
                    case 1: spawnLaserEnemies();
                        break;
                    case 2: spawnSuiciders();
                        break;
                }
            }
            //spawnLaserEnemies();
        }
    }

    void Update()
    {
        if (isBoss)
        {
            if (curBoss == null)
            {
                roundCount++;
                isBoss = false;
                if (roundCount > RNDS_FOR_BOSS_TWO)
                {
                    // do something here
                    roundCount = 0;
                    timePerEnemy *= 0.9f;
                }
            }
            else if (curBoss.GetComponent<Enemy>().hp < 0.6f * bossMaxHP && !bossMobsSpawned)
            {
                StartCoroutine(StartSpawnsInBossFight());
                bossMobsSpawned = true;
            }
        }
    }

    void spawnSuiciders()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(suiciderShip, suiciderShip.transform.position, suiciderShip.transform.rotation);
        }
    }

    void spawnShooters()
    {
        Instantiate(formationShooterShip, formationShooterShip.transform.position, formationShooterShip.transform.rotation);
    }

    void spawnLaserEnemies()
    {

        int freeSpace = Random.Range(1, 5);
        for (int i = 0; i < 6; i++)
        {
            if (i != freeSpace)
            {
                GameObject goTemporalLaserShip = (GameObject)Instantiate(laserShootingShip, laserShootingShip.transform.position, laserShootingShip.transform.rotation);
                goTemporalLaserShip.GetComponent<ApperShootAndLeave>().place = i + 1;
            }
        }
    }

    void spawnShooterBoss()
    {
        curBoss = (GameObject)Instantiate(shooterBoss, new Vector3(0, 6, 0), Quaternion.identity);
        curBoss.GetComponent<Enemy>().hp = 1200f;
        bossMaxHP = curBoss.GetComponent<Enemy>().hp;
        Debug.Log(bossMaxHP);
    }

    void spawnLaserBoss()
    {
        curBoss = (GameObject)Instantiate(laserBoss, new Vector3(0, 6, 0), Quaternion.identity);
        curBoss.GetComponent<Enemy>().hp = 700f;
        bossMaxHP = curBoss.GetComponent<Enemy>().hp;
        Debug.Log(bossMaxHP);
    }

    IEnumerator StartSpawnsInBossFight()
    {
        float cTime = Time.deltaTime;
        while (isBoss)
        {
            if (timePerEnemy*2 < cTime)
            {
                cTime = 0;
                int whatToSpawn = Mathf.RoundToInt(Random.Range(0, 2.98f) - .49f);
                switch (whatToSpawn)
                {
                    case 0: spawnShooters();
                        break;
                    case 1:
                        break;
                    case 2: spawnSuiciders();
                        break;
                }
            }
            cTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        bossMobsSpawned = false;
    }
}
