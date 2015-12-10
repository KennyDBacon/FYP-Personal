using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private int spawnerID;
    private float spawnTimer;
    WaveSpawn currentWaveSpawn;
    SpawnInfo tempSpawnInfo;

    void Update ()
    {
        spawnTimer += Time.deltaTime;
        CheckSpawn();
    }

    void CheckSpawn ()
    {
        while (currentWaveSpawn.basicTimes.Count > 0 && spawnTimer > currentWaveSpawn.basicTimes[0].time)
        {
            Spawn(0, currentWaveSpawn.basicTimes[0].amount);
            currentWaveSpawn.basicTimes.RemoveAt(0);
        }
        while (currentWaveSpawn.runnerTimes.Count > 0 && spawnTimer > currentWaveSpawn.runnerTimes[0].time)
        {
            Spawn(1, currentWaveSpawn.runnerTimes[0].amount);
            currentWaveSpawn.runnerTimes.RemoveAt(0);
        }
        while (currentWaveSpawn.hunterTimes.Count > 0 && spawnTimer > currentWaveSpawn.hunterTimes[0].time)
        {
            Spawn(2, currentWaveSpawn.hunterTimes[0].amount);
            currentWaveSpawn.hunterTimes.RemoveAt(0);
        }
        while (currentWaveSpawn.wallerTimes.Count > 0 && spawnTimer > currentWaveSpawn.wallerTimes[0].time)
        {
            Spawn(3, currentWaveSpawn.wallerTimes[0].amount);
            currentWaveSpawn.wallerTimes.RemoveAt(0);
        }
        while (currentWaveSpawn.tankTimes.Count > 0 && spawnTimer > currentWaveSpawn.tankTimes[0].time)
        {
            Spawn(4, currentWaveSpawn.tankTimes[0].amount);
            currentWaveSpawn.tankTimes.RemoveAt(0);
        }
    }

    void Spawn (int monsterID, int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            Instantiate(GameManager.manager.enemies[monsterID], transform.position + (Random.insideUnitSphere * 5.0f), Quaternion.identity);
        }
    }

    /****************
     * MonsterID
     * 0 = Basic
     * 1 = Runner
     * 2 = Hunter
     * 3 = Waller
     * 4 = Tank
     ****************/
    private void AddToSpawnList(int monsterID, float time, int amount)
    {
        tempSpawnInfo.time = time;
        tempSpawnInfo.amount = amount;
        switch (monsterID)
        {
            case 0:
                currentWaveSpawn.basicTimes.Add(tempSpawnInfo);
                break;
            case 1:
                currentWaveSpawn.runnerTimes.Add(tempSpawnInfo);
                break;
            case 2:
                currentWaveSpawn.hunterTimes.Add(tempSpawnInfo);
                break;
            case 3:
                currentWaveSpawn.wallerTimes.Add(tempSpawnInfo);
                break;
            case 4:
                currentWaveSpawn.tankTimes.Add(tempSpawnInfo);
                break;
        }
        GameManager.manager.enemyCount += amount;
    }

    public void LoadSpawnInfo ()
    {
        currentWaveSpawn = new WaveSpawn();
        spawnTimer = 0.0f;
        switch (GameManager.manager.levelNo)
        {
            case 1:
                switch (GameManager.manager.waveNo)
                {
                    case 1:
                        switch (spawnerID)
                        {
                            case 0:
                                //BASIC
                                AddToSpawnList(0, 1.0f, 1);
                                AddToSpawnList(0, 3.0f, 1);
                                AddToSpawnList(0, 5.0f, 1);


                                //RUNNER
                                AddToSpawnList(1, 2.0f, 1);
                                AddToSpawnList(1, 5.0f, 1);
                                AddToSpawnList(1, 8.0f, 1);
                                AddToSpawnList(1, 11.0f, 1);
                                AddToSpawnList(1, 14.0f, 1);
                                AddToSpawnList(1, 17.0f, 1);
                                AddToSpawnList(1, 20.0f, 5);

                                //WALLER
                                //AddToSpawnList(3, 20.0f, 2);

                                break;
                            case 1:
                                //BASIC
                                AddToSpawnList(1, 5.0f, 3);
                                AddToSpawnList(0, 7.0f, 2);
                                AddToSpawnList(0, 9.0f, 2);
                                AddToSpawnList(0, 11.0f, 4);
                                break;
                        }
                        break;
                    case 2:
                        //WAVE RESISTANCE
                        currentWaveSpawn.resistances[0] = 1.0f;
                        currentWaveSpawn.resistances[2] = 1.0f;
                        currentWaveSpawn.resistances[3] = 0.5f;
                        switch (spawnerID)
                        {
                            case 0:
                                //BASIC
                                AddToSpawnList(0, 1.0f, 4);
                                AddToSpawnList(0, 7.0f, 1);
                                AddToSpawnList(0, 9.0f, 1);
                                AddToSpawnList(0, 11.0f, 4);

                                //RUNNER
                                AddToSpawnList(1, 2.0f, 2);
                                AddToSpawnList(1, 8.0f, 2);
                                AddToSpawnList(1, 14.0f, 2);
                                AddToSpawnList(1, 20.0f, 4);

                                //TANK
                                AddToSpawnList(4, 21.0f, 2);
                                break;
                            case 1:
                                break;
                                //BASIC
                                AddToSpawnList(0, 2.0f, 4);
                                AddToSpawnList(0, 4.0f, 2);
                                AddToSpawnList(0, 6.0f, 2);
                                AddToSpawnList(0, 8.0f, 4);

                                //RUNNER
                                AddToSpawnList(0, 10.0f, 4);
                                AddToSpawnList(1, 14.0f, 2);
                                AddToSpawnList(1, 18.0f, 2);

                                //HUNTER
                                AddToSpawnList(2, 20.0f, 1);

                                //TANK
                                AddToSpawnList(4, 21.0f, 1);
                        }
                        break;
                    case 3:
                        //WAVE RESISTANCE
                        currentWaveSpawn.resistances[0] = 1.0f;
                        currentWaveSpawn.resistances[2] = 1.0f;
                        currentWaveSpawn.resistances[3] = 0.5f;
                        switch (spawnerID)
                        {
                            case 0:
                                //BASIC
                                AddToSpawnList(0, 1.0f, 4);
                                AddToSpawnList(0, 7.0f, 1);
                                AddToSpawnList(0, 9.0f, 1);
                                AddToSpawnList(0, 11.0f, 4);

                                //RUNNER
                                AddToSpawnList(1, 2.0f, 2);
                                AddToSpawnList(1, 8.0f, 2);
                                AddToSpawnList(1, 14.0f, 2);
                                AddToSpawnList(1, 20.0f, 4);
                                break;
                            case 1:
                                break;
                                //BASIC
                                AddToSpawnList(0, 1.0f, 4);
                                AddToSpawnList(0, 7.0f, 2);
                                AddToSpawnList(0, 9.0f, 2);
                                AddToSpawnList(0, 11.0f, 4);

                                //RUNNER
                                AddToSpawnList(0, 2.0f, 4);
                                AddToSpawnList(1, 5.0f, 2);
                                AddToSpawnList(1, 11.0f, 2);
                                AddToSpawnList(1, 17.0f, 4);

                                //HUNTER
                                AddToSpawnList(2, 20.0f, 2);
                        }
                        break;
                    case 4:
                        GameManager.manager.WinGame();
                        break;
                }
                break;
            case 2:
                break;
        }
    }
}

public class WaveSpawn
{
    //Fire, Ice, Earth, Lightning   min = 0, max = 1
    public float[] resistances = {0, 0, 0, 0};
    public List<SpawnInfo> basicTimes = new List<SpawnInfo>();
    public List<SpawnInfo> runnerTimes = new List<SpawnInfo>();
    public List<SpawnInfo> hunterTimes = new List<SpawnInfo>();
    public List<SpawnInfo> wallerTimes = new List<SpawnInfo>();
    public List<SpawnInfo> tankTimes = new List<SpawnInfo>();
}

public struct SpawnInfo
{
    public float time;
    public int amount;
}
