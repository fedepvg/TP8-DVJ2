using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float BasicEnemyCooldown;
    public float BasicEnemyMinRate;
    public float BasicEnemyMaxRate;
    float TimeToNextBasicEnemy;
    float GroupCooldown;
    public float GroupMinSpawnRate;
    public float GroupMaxSpawnRate;
    public float GroupDistance;
    float TimeToNextGroup;
    public GameObject BasicEnemyPrefab;
    public GameObject GroupEnemyPrefab;

    private void Start()
    {
        TimeToNextBasicEnemy = 0.2f;
        PlayerShip.OnPlayerKilled += DestroySpawner;
    }

    private void Update()
    {
        BasicEnemyCooldown += Time.deltaTime;
        GroupCooldown += Time.deltaTime;
        if (BasicEnemyCooldown >= TimeToNextBasicEnemy)
        {
            BasicEnemyCooldown = 0;
            TimeToNextBasicEnemy = SetNextEnemySpawn(BasicEnemyMinRate, BasicEnemyMaxRate);
            GameObject go = Instantiate(BasicEnemyPrefab);
            Vector2 pos = GetSpawnRange();
            go.transform.position = pos;
            GameManager.Instance.AddEnemyOnScreen();
        }
        if (GroupCooldown >= TimeToNextGroup)
        {
            GroupCooldown = 0;
            TimeToNextGroup = SetNextEnemySpawn(GroupMinSpawnRate, GroupMaxSpawnRate);
            GameObject go = Instantiate(GroupEnemyPrefab,Vector3.zero,Quaternion.identity);
            //recursividad con invoke para varios enemigos
            GameManager.Instance.AddEnemyOnScreen();
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

    void DestroySpawner()
    {
        Destroy(gameObject);
    }

    float SetNextEnemySpawn(float minRate, float maxRate)
    {
        float nextEnemy;
        nextEnemy = Random.Range(minRate, maxRate);
        return nextEnemy;
    }
}
