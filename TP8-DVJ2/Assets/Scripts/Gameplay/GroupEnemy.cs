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
    float SinOffset;
    bool InGame;

    private void Start()
    {
        Energy = MaxEnergy;
        ShootTimer = FireRate / 2;
        MoveForward = true;
        SinOffset = CameraUtils.OrthographicBounds().extents.x;
        InGame = false;
        Invoke("SetInGame", 0.5f);
    }

    private void Update()
    {
        Move();
        if(IsOutOfScreen(0) && InGame)
        {
            MoveForward = !MoveForward;
        }
    }

    void SetInGame()
    {
        InGame = true;
    }

    public override void Move()
    {
        YMovementTimer += Time.deltaTime * Speed;
        float y;
        if (MoveForward)
        {
            y = Mathf.Sin(YMovementTimer);
            XMovementTimer += Time.deltaTime * Speed;
        }
        else
        {
            y = Mathf.Sin(YMovementTimer);
            XMovementTimer -= Time.deltaTime * Speed;
        }

        transform.position = new Vector2(XMovementTimer - SinOffset, y);
    }

    public override void Shoot()
    {

    }
}
