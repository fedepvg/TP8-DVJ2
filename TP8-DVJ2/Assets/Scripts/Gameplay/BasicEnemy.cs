using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Ship
{
    const int MaxEnergy = 100;
    const int EnergyToKamikazeAttack = 30;
    Transform Player;
    public Transform Cannon;
    public GameObject BulletPrefab;
    public enum States { Attack, Kamikaze };
    const float MinDistanceToPlayer = 3.5f;
    States CurrentState;
    public float Speed;
    public float KamikazeSpeed;
    Quaternion RotationToPlayer;
    float ShootTimer;
    const float FireRate = 1f;

    private void Start()
    {
        Energy = MaxEnergy;
        Player = GameObject.Find("PlayerShip").transform;
        CurrentState = States.Attack;
        ShootTimer = 0f;
    }

    private void Update()
    {
        CheckState();
        Vector3 pos = transform.position;
        switch (CurrentState)
        {
            case States.Attack:
                ShootTimer += Time.deltaTime;
                pos += Vector3.down * Speed * Time.deltaTime;
                if (ShootTimer >= FireRate)
                {
                    Shoot();
                    ShootTimer = 0f;
                }
                break;
            case States.Kamikaze:
                LookToPlayer();
                //pos = Vector2.MoveTowards(pos, Player.position, Speed * Time.deltaTime);
                pos += -transform.up * KamikazeSpeed * Time.deltaTime;
                break;
        }
        transform.position = pos;
        //transform.RotateAround(Player.transform.position, Vector3.forward, 0.1f);
        //transform.rotation = Quaternion.identity;
    }

    void CheckState()
    {
        Vector3 pos = transform.position;
        Vector3 playerPos = Player.transform.position;

        if(Energy <= EnergyToKamikazeAttack || Vector3.Distance(pos, playerPos) < MinDistanceToPlayer)
        {
            CurrentState = States.Kamikaze;
        }
        else
        {
            CurrentState = States.Attack;
        }
    }

    void LookRotationToPlayer(out Quaternion rot, Vector3 upwards)
    {
        Vector3 relativePos = Player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, upwards);
        rotation.x = 0;
        rotation.y = 0;
        rot = rotation;
    }

    void LookToPlayer()
    {
        Quaternion q = new Quaternion();
        LookRotationToPlayer(out q, Vector3.forward);
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
        Quaternion shootRot;
        LookRotationToPlayer(out shootRot, Vector3.back);
        Instantiate(BulletPrefab, Cannon.transform.position, shootRot);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="PlayerBullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            GetHitted(bullet.GetDamageAmount());
        }
    }
}
