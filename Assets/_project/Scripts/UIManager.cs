using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private Text fishAmount;
    [SerializeField] private Text boatWeight;
    [SerializeField] private Image bottle;
    [SerializeField] private Image key;
    [SerializeField] private Image chest;
    [SerializeField] private Text junk;
    [SerializeField] private TMP_Text boatSank;
    
    public void UpdateFishAmount()
    {
        fishAmount.text = "X" + gameManager.FishAmount;
    }
    public void UpdateBoatWeight(int weight)
    {
        boatWeight.text = "Weight " + weight;
    }
    public void EnableBottle(bool state)
    {
        bottle.gameObject.SetActive(state);
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
}
