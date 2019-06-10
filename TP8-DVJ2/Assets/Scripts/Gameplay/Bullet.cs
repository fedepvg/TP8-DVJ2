using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public int Damage;

    void Update()
    {
        Vector3 pos = transform.position;
        //pos += new Vector3(0, Speed*Time.deltaTime, 0);
        pos += transform.up * Speed * Time.deltaTime;
        transform.position = pos;
    }

    public int GetDamageAmount()
    {
        return Damage;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Bounds")
            Destroy(gameObject);
    }
}
