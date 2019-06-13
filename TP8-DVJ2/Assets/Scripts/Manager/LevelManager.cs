using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float LevelDistance;
    float DistanceLeft;

    private void Start()
    {
        DistanceLeft = LevelDistance;
    }

    void Update()
    {
        DistanceLeft -= Time.deltaTime;
        if (DistanceLeft <= 0)
        {
            DistanceLeft = 0;
            GameManager.Instance.EndLevel();
        }
    }

    public int GetDistanceLeft()
    {
        return (int)DistanceLeft;
    }
}
