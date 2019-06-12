using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameManager : MonoBehaviour
{
    public Slider EnergySlider;
    public PlayerShip Player;

    private void Update()
    {
        EnergySlider.value = Player.GetEnergy();
        if(Player.GetEnergy()==0)
        {
            EnergySlider.fillRect.gameObject.SetActive(false);
        }
    }
}
