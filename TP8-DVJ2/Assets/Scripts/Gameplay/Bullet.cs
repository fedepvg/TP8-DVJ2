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
        pos += transform.up * Speed * Time.deltaTime;
        transform.position = pos;
        if(IsOutOfScreen())
        {
            Destroy(gameObject);
        }
    }

    public int GetDamageAmount()
    {
        return Damage;
    }

    bool IsOutOfScreen()
    {
        Bounds bounds = CameraUtils.OrthographicBounds();
        float halfScale = transform.GetComponent<SpriteRenderer>().bounds.size.x;
        Vector3 pos = transform.position;
        float leftBound = bounds.min.x;
        float rightBound = bounds.max.x;
        float upBound = bounds.max.y;
        float downBound = bounds.min.y;
        if(pos.x < leftBound - halfScale || pos.x > rightBound + halfScale ||
            pos.y < downBound - halfScale || pos.y > upBound + halfScale)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemy")
        {

        }
    }
}
