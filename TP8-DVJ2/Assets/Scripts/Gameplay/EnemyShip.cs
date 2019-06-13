﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EnemyShip : Ship
{
    public int MaxEnergy;
    int ChanceToSpawnItem = 20;
    int TopValueToSpawnEnergy = 3;
    int TopValueToSpawnBullet = 6;
    public GameObject EnergyItem;
    public GameObject BulletItem;
    public int ScoreOnDead;
    public float Speed;
    public Transform Cannon;
    public GameObject BulletPrefab;
    public float FireRate;

    protected bool IsOutOfScreen(float offset)
    {
        Bounds bounds = CameraUtils.OrthographicBounds();
        Vector3 pos = transform.position;
        float leftBound = bounds.min.x;
        float rightBound = bounds.max.x;
        float downBound = bounds.min.y;
        if (pos.x < leftBound - offset || pos.x > rightBound + offset ||
            pos.y < downBound - offset)
        {
            return true;
        }
        return false;
    }

    public override void GetHitted(int damage)
    {
        Energy -= damage;
        if (Energy <= 0)
        {
            Die();
            ScoreManager.Instance.AddScore(ScoreOnDead);
        }
        else
        {
            ChangeColor();
        }
    }

    public override void Die()
    {
        ReturnToNormalColor();
        base.Die();
        TryToSpawnItem();
        Destroy(this);
    }

    void TryToSpawnItem()
    {
        int rand = Random.Range(0, ChanceToSpawnItem);
        if (rand < TopValueToSpawnEnergy)
        {
            Instantiate(EnergyItem, transform.position, Quaternion.identity);
        }
        else if (rand < TopValueToSpawnBullet)
        {
            Instantiate(BulletItem, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            GetHitted(bullet.GetDamageAmount());
            Destroy(collision.gameObject);
        }
        if (collision.transform.tag == "Player")
        {
            GetHitted(MaxEnergy);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GetHitted(MaxEnergy);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.SubstractEnemyOnScreen();
    }
}