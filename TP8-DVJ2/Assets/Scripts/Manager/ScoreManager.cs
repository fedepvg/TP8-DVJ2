﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager> 
{
    int score;
    int killedEnemies;

    public override void Awake()
    {
        base.Awake();
    }

    public int Score
    {
        get { return score; }
    }

    public int KilledEnemies
    {
        get { return killedEnemies; }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    void AddKilledEnemy(int s)
    {
        killedEnemies ++;
    }
}
