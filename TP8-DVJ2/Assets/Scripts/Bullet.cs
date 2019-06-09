using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        Vector3 pos = transform.position;
        pos += new Vector3(0, Speed*Time.deltaTime, 0);
        transform.position = pos;
    }
}
