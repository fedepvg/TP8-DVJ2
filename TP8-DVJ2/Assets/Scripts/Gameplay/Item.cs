using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float Speed;
    public float LifeTime;

    private void Start()
    {
        Invoke("DestroyItem", LifeTime);
    }

    void Update()
    {
        transform.position -= Vector3.up * Speed * Time.deltaTime;
    }

    void DestroyItem()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            DestroyItem();
        }
    }
}
