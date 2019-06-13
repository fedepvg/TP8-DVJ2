using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    int EnemiesOnScreen;
    Dictionary<int,int> LevelDistance;
    public int Level1Distance;
    public int Level2Distance;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        EnemiesOnScreen = 0;
        PlayerShip.OnPlayerKilled += GameOver;
        LevelDistance.Add(1,Level1Distance);
        LevelDistance.Add(2,Level2Distance);
    }

    public void AddEnemyOnScreen()
    {
        EnemiesOnScreen++;
    }

    public void SubstractEnemyOnScreen()
    {
        EnemiesOnScreen--;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("EndGameScene");
    }
}
