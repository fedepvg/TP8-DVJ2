using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Ship
{
    const int MaxEnergy = 100;
    Transform Player;
    public Transform Cannon;
    public GameObject BulletPrefab;
    public enum States { Attack, Kamikaze };
    const float MinDistanceToPlayer = 3.5f;
    States CurrentState;
    public float Speed;
    Quaternion RotationToPlayer;

    private void Start()
    {
        Energy = MaxEnergy;
        Player = GameObject.Find("PlayerShip").transform;
        CurrentState = States.Attack;
    }

    private void Update()
    {
        CheckState();
        switch (CurrentState)
        {
            case States.Attack:

                break;
            case States.Kamikaze:
                LookToPlayer();
                transform.position= Vector2.MoveTowards(transform.position, Player.position, Speed*Time.deltaTime);
                break;
        }
        //transform.RotateAround(Player.transform.position, Vector3.forward, 0.1f);
        //transform.rotation = Quaternion.identity;
    }

    void CheckState()
    {
        Vector3 pos = transform.position;
        Vector3 playerPos = Player.transform.position;

        if(Energy <= 30 || Vector3.Distance(pos, playerPos) < MinDistanceToPlayer)
        {
            CurrentState = States.Kamikaze;
        }
        else
        {
            CurrentState = States.Attack;
        }
    }

    void LookRotationToPlayer(out Quaternion rot)
    {
        Vector3 relativePos = Player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.forward);
        rotation.x = 0;
        rotation.y = 0;
        rot = rotation;
    }

    void LookToPlayer()
    {
        Quaternion q = new Quaternion();
        LookRotationToPlayer(out q);
        transform.rotation = Quaternion.Lerp(transform.rotation, q, Mathf.PingPong(Time.time, 2) / 2);
    }

    public override void GetHitted(int damage)
    {
        
    }

    public override void Move()
    {
        Vector3 pos = transform.position;
        pos += new Vector3(0, Speed * Time.deltaTime, 0);
        transform.position = pos;
    }

    public override void Shoot()
    {
        Instantiate(BulletPrefab, Cannon.transform.position, Quaternion.identity);
    }
}
