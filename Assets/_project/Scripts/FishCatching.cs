using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCatching : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager UIManager;
    private Player player;

    private bool fishCaught;
    private bool delayStarted;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }
    
    
    public void CommenceCatching()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            player.throwSuccesful = false;
            gameManager.ResetClamp();
        }

        if (!delayStarted)
        {
            StartCoroutine(FishDelay());
        }
        

        if (fishCaught)
        {
            Debug.Log("You got fish!");
            CaughtFish();
            fishCaught = false;
            player.throwSuccesful = false;
            gameManager.ResetClamp();
        }
    }

    public void CaughtFish()
    {
        gameManager.FishAmount++;
        UIManager.UpdateFishAmount();
        
    }

    IEnumerator FishDelay()
    {
        delayStarted = true;
        yield return new WaitForSeconds(Random.Range(5, 20));
        fishCaught = true;
        delayStarted = false;
    }
}
