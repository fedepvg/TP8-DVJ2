using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    public float Speed;
    KeyCode BasicShoot = KeyCode.J;
    KeyCode MissileShoot = KeyCode.K;
    public Collider2D LevelBounds;

    void Start()
    {

    }

    void Update()
    {
        Move();
        CheckWorldBounds();
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
