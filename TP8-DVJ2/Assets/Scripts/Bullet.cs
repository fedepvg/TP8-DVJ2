using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    Transform Bounds;

    private void Start()
    {
        Bounds = GameObject.Find("Bounds").transform;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos += new Vector3(0, Speed*Time.deltaTime, 0);
        transform.position = pos;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Bounds")
            Destroy(gameObject);
    }
}
