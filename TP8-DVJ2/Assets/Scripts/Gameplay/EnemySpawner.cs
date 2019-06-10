using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float BasicEnemyCooldown;
    public float BasicEnemyMinRate;
    public float BasicEnemyMaxRate;
    float TimeToNextBasicEnemy;
    public GameObject BasicEnemyPrefab;

    private void Start()
    {
        TimeToNextBasicEnemy = 0.2f;
    }

    private void Update()
    {
        BasicEnemyCooldown += Time.deltaTime;
        if (BasicEnemyCooldown >= TimeToNextBasicEnemy)
        {
            BasicEnemyCooldown = 0;
            TimeToNextBasicEnemy = SetNextEnemySpawn(BasicEnemyMinRate, BasicEnemyMaxRate);
            GameObject go = Instantiate(BasicEnemyPrefab);
            Vector2 pos = GetSpawnRange();
            go.transform.position = pos;
        }
    }

    Vector2 GetSpawnRange()
    {
        float halfHeight = BasicEnemyPrefab.transform.localScale.y / 2;
        float halfWidth = BasicEnemyPrefab.transform.localScale.x / 2;
        Bounds bounds = CameraUtils.OrthographicBounds();
        Vector2 spawnPoint;
        spawnPoint.x = Random.Range(bounds.min.x + halfWidth, bounds.max.x - halfWidth);
        spawnPoint.y = bounds.max.y + halfHeight;
        return spawnPoint;
    }

    float SetNextEnemySpawn(float minRate, float maxRate)
    {
        float nextEnemy;
        nextEnemy = Random.Range(minRate, maxRate);
        return nextEnemy;
    }
}
