using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameManager : MonoBehaviour
{
    public Slider EnergySlider;
    public PlayerShip Player;
    public Text Score;
    public Text Distance;
    public LevelManager levelManager;
    int ActualScore;
    int DistanceLeft;

    private void Update()
    {
        EnergySlider.value = Player.GetEnergy();
        if(Player.GetEnergy()==0)
        {
            EnergySlider.fillRect.gameObject.SetActive(false);
        }
        if(ScoreManager.Instance.Score != ActualScore)
        {
            ActualScore = ScoreManager.Instance.Score;
            Score.text = "Score: " + ActualScore;
        }
        if(levelManager.GetDistanceLeft() != DistanceLeft)
        {
            DistanceLeft = levelManager.GetDistanceLeft();
            Distance.text = "Distance: " + DistanceLeft;
        }
    }
}
