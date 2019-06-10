using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public override void Awake()
    {
        base.Awake();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("EndGameScene");
    }
}
