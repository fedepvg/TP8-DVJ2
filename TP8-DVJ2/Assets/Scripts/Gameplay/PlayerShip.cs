using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    public float Speed;
    private int MaxEnergy = 100;
    KeyCode BasicShoot = KeyCode.J;
    public Transform LeftCannon;
    public Transform RightCannon;
    public Transform MiddleCannon;
    float FireRate = 0.05f;
    float ShootTimer;
    public GameObject BulletPrefab;
    public float EnergyLossMultiplier;

    void Start()
    {
        ShootTimer = FireRate;
        Energy = MaxEnergy;
    }

    void Update()
    {
        Energy -= Time.deltaTime * EnergyLossMultiplier;
        if(Energy <= 0)
        {
            //Destroy(gameObject);
        }
        Move();
        CheckWorldBounds();
        Shoot();
    }

    public override void GetHitted(int damage)
    {
        Energy -= damage;
        if(Energy <= 0)
        {
            //Destroy(gameObject);
        }
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
        Bounds bounds = CameraUtils.OrthographicBounds();
        pos.x = Mathf.Clamp(pos.x, bounds.min.x + scale.x/2, bounds.max.x - scale.x / 2);
        pos.y = Mathf.Clamp(pos.y, bounds.min.y + scale.y / 2, bounds.max.y - scale.y / 2);

        transform.position = pos;
    }

    public float GetEnergy()
    {
        return Energy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            GetHitted(collision.GetComponent<Bullet>().GetDamageAmount());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            GetHitted(MaxEnergy);
        }
    }
}
