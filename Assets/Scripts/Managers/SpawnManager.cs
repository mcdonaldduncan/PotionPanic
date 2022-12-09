using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnTimer;
    [SerializeField] int firstWaveNumber;
    [SerializeField] int maxDifficulty;

    float lastSpawnTime;
    int difficultyMeasure;
    int speedUps;

    bool shouldSpawn => lastSpawnTime + spawnTimer < Time.time;
    float stateThreshold => maxDifficulty / Enum.GetValues(typeof(Difficulty)).Length;
    Difficulty difficulty => (Difficulty)Mathf.Floor(difficultyMeasure/stateThreshold);

    void Start()
    {
        for (int i = 0; i < firstWaveNumber; i++)
        {
            GameObject temp = Instantiate(enemyPrefabs[0]);
            temp.transform.position = spawnPoints[(i + 1) % spawnPoints.Length].transform.position;
        }
    }

    void Update()
    {
        if (shouldSpawn)
        {
            SpawnWave();
            difficultyMeasure++;
            lastSpawnTime = Time.time;
        }

        if (difficultyMeasure / maxDifficulty > 2 && speedUps < 1)
        {
            spawnTimer *= .5f;
            speedUps = 1;
        }

        if (difficultyMeasure / maxDifficulty > 3 && speedUps < 2)
        {
            spawnTimer *= .5f;
            speedUps = 2;
        }

    }

    void SpawnWave()
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                CreateWave(0, 2);
                break;
            case Difficulty.MEDIUM:
                CreateWave(0, 3);
                break;
            case Difficulty.HARD:
                CreateWave(1, 4);
                break;
            case Difficulty.IMPOSSIBLE:
                CreateWave(2, 4);
                break;
            default:
                break;
        }
    }

    void CreateWave(int minEnemy, int maxEnemy)
    {
        for (int i = 0; i < firstWaveNumber + difficultyMeasure; i++)
        {
            GameObject temp = Instantiate(enemyPrefabs[UnityEngine.Random.Range(minEnemy, maxEnemy)]);
            temp.transform.position = spawnPoints[(i + 1) % spawnPoints.Length].transform.position;
        }
    }

}

enum Difficulty
{
    EASY,
    MEDIUM,
    HARD,
    IMPOSSIBLE
}
