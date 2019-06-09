﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    public float Speed;
    KeyCode BasicShoot = KeyCode.J;
    KeyCode MissileShoot = KeyCode.K;
    public Collider2D LevelBounds;
    public Transform LeftCannon;
    public Transform RightCannon;
    public Transform MiddleCannon;
    float FireRate = 0.1f;
    float ShootTimer;
    public GameObject BulletPrefab;

    void Start()
    {
        ShootTimer = FireRate;
    }

    void Update()
    {
        Move();
        CheckWorldBounds();
        Shoot();
    }

    public override void GetDamage()
    {
        
    }

    public override void Move()
    {
        Vector3 Pos = transform.position;
        float HorizontalMovement = Input.GetAxis("Horizontal");
        float VerticalMovement = Input.GetAxis("Vertical");
        Pos += new Vector3(HorizontalMovement * Speed * Time.deltaTime, VerticalMovement * Speed * Time.deltaTime, 0);
        transform.position = Pos;
    }

    public override void Shoot()
    {
        ShootTimer += Time.deltaTime;
        if (Input.GetKey(BasicShoot))
        {
            if(ShootTimer >= FireRate)
            {
                ShootTimer = 0;
                GameObject leftBullet = Instantiate(BulletPrefab, LeftCannon.position,Quaternion.identity);
                GameObject RightBullet = Instantiate(BulletPrefab, RightCannon.position,Quaternion.identity);
            }
        }
    }

    void CheckWorldBounds()
    {
        Vector3 pos = transform.position;
        Vector3 scale = transform.localScale;
        Bounds bounds = LevelBounds.bounds;
        pos.x = Mathf.Clamp(pos.x, bounds.min.x + scale.x/2, bounds.max.x - scale.x / 2);
        pos.y = Mathf.Clamp(pos.y, bounds.min.y + scale.y / 2, bounds.max.y - scale.y / 2);

        transform.position = pos;
    }
}