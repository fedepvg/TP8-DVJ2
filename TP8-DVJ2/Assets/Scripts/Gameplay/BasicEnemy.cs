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
    States CurrentState;
    public float Speed;
    public float KamikazeSpeed;
    Quaternion RotationToPlayer;
    float ShootTimer;
    public float FireRate;
    Vector3 DownRotation = new Vector3(0, 0, 180);

    private void Start()
    {
        Energy = MaxEnergy;
        Player = GameObject.Find("PlayerShip").transform;
        CurrentState = States.Attack;
        ShootTimer = FireRate/2;
        transform.Rotate(DownRotation);
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
                if (ShootTimer >= FireRate && IsOnFireZone())
                {
                    Shoot();
                    ShootTimer = 0f;
                }
                break;
            case States.Kamikaze:
                LookToPlayer();
                pos += transform.up * KamikazeSpeed * Time.deltaTime;
                break;
        }
        transform.position = pos;
        if(IsOutOfScreen())
        {
            Destroy(gameObject);
        }
    }

    void CheckState()
    {
        Vector3 pos = transform.position;
        Vector3 playerPos = Player.transform.position;

        if(Energy <= EnergyToKamikazeAttack)
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
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.back);
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
        Energy -= damage;
        if(Energy <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void Move()
    {
        Vector3 pos = transform.position;
        pos += new Vector3(0, Speed * Time.deltaTime, 0);
        transform.position = pos;
    }

    public override void Shoot()
    {
        Quaternion bulletRotation;
        LookRotationToPlayer(out bulletRotation);
        Instantiate(BulletPrefab, Cannon.transform.position, bulletRotation);
    }

    bool IsOutOfScreen()
    {
        Bounds bounds = CameraUtils.OrthographicBounds();
        float halfScale = transform.GetComponent<SpriteRenderer>().bounds.size.x;
        Vector3 pos = transform.position;
        float leftBound = bounds.min.x;
        float rightBound = bounds.max.x;
        float downBound = bounds.min.y;
        if (pos.x < leftBound - halfScale || pos.x > rightBound + halfScale ||
            pos.y < downBound - halfScale)
        {
            return true;
        }
        return false;
    }

    bool IsOnFireZone()
    {
        Vector3 pos = transform.position;
        float fireZone = CameraUtils.OrthographicBounds().center.y;
        return pos.y > fireZone;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="PlayerBullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            GetHitted(bullet.GetDamageAmount());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Player")
        {
            GetHitted(MaxEnergy);
        }
    }
}
