using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupEnemy : EnemyShip
{
    float ShootTimer;
    Vector3 playerPos;
    float XMovementTimer;
    float YMovementTimer;
    bool MoveForward;
    float YInicial = 0;
    float ForwardDistance = 2;
    float BackDistance = 1.5f;
    float XMultiplier;

    private void Start()
    {
        XMultiplier = ForwardDistance;
        Energy = MaxEnergy;
        ShootTimer = FireRate / 2;
        MoveForward = true;
    }

    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        YMovementTimer += Time.deltaTime;
        float y;
        if (MoveForward)
        {
            y = Mathf.Sin(YMovementTimer); //* 0.8f + 2f;
            XMovementTimer += Time.deltaTime;
        }
        else
        {
            y = Mathf.Sin(YMovementTimer);
            XMovementTimer -= Time.deltaTime;
        }

        transform.position = new Vector2(XMovementTimer, y);

        if (y < YInicial - 0.5f && MoveForward == true)
        {
            MoveForward = false;
            XMultiplier = ForwardDistance;
            XMovementTimer = YMovementTimer;
        }
        else if(y > YInicial + 0.5f && MoveForward == false)
        {
            MoveForward = true;
            XMultiplier = ForwardDistance;
            YMovementTimer = XMovementTimer;
        }
    }

    public override void Shoot()
    {

    }
}
