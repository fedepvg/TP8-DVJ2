using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Bounds SpriteBounds;
    public float ParallaxSpeed;
    private Vector3 StartPosition;

    void Start()
    {
        SpriteBounds = GetComponent<SpriteRenderer>().bounds;
        StartPosition = transform.position;
    }
    
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * ParallaxSpeed, SpriteBounds.size.y);
        transform.position = StartPosition - Vector3.up * newPosition;
    }
}
