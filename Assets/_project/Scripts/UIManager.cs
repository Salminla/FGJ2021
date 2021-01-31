using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private Text fishAmount;
    [SerializeField] private Text boatWeight;
    [SerializeField] private Image bottleFull;
    [SerializeField] private Image bottleEmpty;
    [SerializeField] private Image key;
    [SerializeField] private Image chest;
    [SerializeField] private Text junk;
    [SerializeField] private TMP_Text boatSank;
    [SerializeField] private Image alert;
    [SerializeField] private TMP_Text powerState;
    [SerializeField] private TMP_Text creditsText;

    private void Start()
    {
        UpdateFishAmount();
        UpdateBoatWeight(0);
    }

    public void UpdateFishAmount()
    {
        fishAmount.text = gameManager.FishAmount.ToString();
    }
    public void UpdateBoatWeight(int weight)
    {
        boatWeight.text = weight.ToString();
    }
    public void EnableBottleFull(bool state)
    {
        bottleFull.gameObject.SetActive(state);
    }
    public void EnableBottleEmpty(bool state)
    {
        bottleEmpty.gameObject.SetActive(state);
    }
    public void EnableKey(bool state)
    {
        key.gameObject.SetActive(state);
    }
    public void EnableChest(bool state)
    {
        chest.gameObject.SetActive(state);
    }

    public void EnableBoatSank(bool state)
    {
        boatSank.gameObject.SetActive(state); 
    }
    public void EnableAlert(bool state)
    {
        alert.gameObject.SetActive(state); 
    }

    public void SetPowerText(string text, Color color, bool state)
    {
        powerState.gameObject.SetActive(state);
        powerState.text = text;
        powerState.color = color;
    }

    public void RollCredits()
    {
        
    }
}
