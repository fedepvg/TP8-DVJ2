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

    public void EndLevel()
    {
        if(EnemiesOnScreen <= 0)
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case "Level1":
                    SceneManager.LoadScene("Level2");
                    break;
                case "Level2":
                    GameOver();
                    break;
                default:
                    break;
            }
        }
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
