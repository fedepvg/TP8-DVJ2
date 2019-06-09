using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Ship
{
    const int MaxEnergy = 100;
    const int DamageAmount = 10;
    Transform Bounds;
    Transform Player;
    public Transform Cannon;
    public GameObject BulletPrefab;

    private void Start()
    {
        Energy = MaxEnergy;
        Bounds = GameObject.Find("Bounds").transform;
        Player = GameObject.Find("PlayerShip").transform;
    }

    private void Update()
    {
        SetRotation();
    }

    void SetRotation()
    {
        Vector3 relativePos = Player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.forward);
        rotation.x = 0;
        rotation.y = 0;
        transform.rotation = rotation;
    }

    public override void GetHitted(int damage)
    {
        
    }

    public override void Move()
    { 

    }

    public override void Shoot()
    {
        Instantiate(BulletPrefab, Cannon.transform.position, Quaternion.identity);
    }
}
