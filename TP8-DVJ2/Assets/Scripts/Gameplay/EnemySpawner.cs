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
    public float EnemiesPerGroup;
    float EnemiesOnCurrentGroup;
    Vector3 GroupSpawnPosition;

    private void Start()
    {
        //TimeToNextBasicEnemy = 0.2f;
        PlayerShip.OnPlayerKilled += DestroySpawner;
        TimeToNextGroup = SetNextEnemySpawn(GroupMinSpawnRate, GroupMaxSpawnRate);
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
            CreateEnemyOfGroup();
        }
    }

    void CreateEnemyOfGroup()
    {
        if (EnemiesOnCurrentGroup < EnemiesPerGroup)
        {
            Instantiate(GroupEnemyPrefab,SpawnPositionForGroup(),Quaternion.identity);
            Invoke("CreateEnemyOfGroup", GroupDistance);
            GameManager.Instance.AddEnemyOnScreen();
            EnemiesOnCurrentGroup++;
        }
        else
        {
            EnemiesOnCurrentGroup = 0;
        }
    }

    Vector3 SpawnPositionForGroup()
    {
        if (EnemiesOnCurrentGroup == 0)
        {
            Bounds cameraBounds = CameraUtils.OrthographicBounds();
            float groupSpriteOffset = GroupEnemyPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
            float xPosition = cameraBounds.min.x - groupSpriteOffset;
            float yPosition = Random.Range(cameraBounds.center.y, cameraBounds.center.y + cameraBounds.extents.y / 2);
            GroupSpawnPosition = new Vector3(xPosition, yPosition, 0);
        }

        return GroupSpawnPosition;
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
