using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private Text fishAmount;

    public void UpdateFishAmount()
    {
        fishAmount.text = "Fish caught: " + gameManager.FishAmount;
    }
}
