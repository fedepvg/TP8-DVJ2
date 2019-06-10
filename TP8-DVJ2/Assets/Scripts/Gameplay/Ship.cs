using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    protected float Energy;

    public abstract void Shoot();
    public abstract void Move();
    public abstract void GetHitted(int damage);
}
