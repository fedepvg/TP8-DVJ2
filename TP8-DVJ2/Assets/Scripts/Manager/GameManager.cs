using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    int EnemiesOnScreen;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        EnemiesOnScreen = 0;
        PlayerShip.OnPlayerKilled += GameOver;
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
