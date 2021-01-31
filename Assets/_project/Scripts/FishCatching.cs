using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using Random = UnityEngine.Random;

public class FishCatching : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Boat boat;
    
    [SerializeField] private int chestChance = 20;
    [SerializeField] private int keyChance = 20;
    [SerializeField] private int numFishBeforeSpecial = 2;
    
    [Header("Models")]
    [SerializeField] private GameObject fish;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject chest;
    
    private Player player;

    private bool fishBit;
    private bool fishCatch;
    private bool delayStarted;
    private bool timedClick = false;
    private bool timerStarted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && timerStarted)
        {
            timedClick = true;
            uiManager.EnableAlert(false);
        }
    }

    public void CommenceCatching()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            player.throwSuccesful = false;
            gameManager.ResetClamp();
        }

        if (!delayStarted && fishBit == false)
            StartCoroutine(FishCatchDelay());
        
        if (!fishBit) return;
        
        if (!delayStarted)
        {
            Debug.Log("You got bit!");
            uiManager.EnableAlert(true);
            StartCoroutine(FishBit());
        }

        if (!fishCatch) return;
        int randomNum = Random.Range(0, 100);
        if (gameManager.FishAmount > numFishBeforeSpecial)
        {
            if ((randomNum > 0 && randomNum < 80) && !gameManager.BottleGet)
                CaughtBottle();
            else if ((randomNum > 0 && randomNum < chestChance) && !gameManager.ChestGet)
                CaughtChest();
            else if ((randomNum > 0 && randomNum < keyChance) && !gameManager.KeyGet)
                CaughtKey();
            else
                CaughtFish();
        }
        else
            CaughtFish();
        StopCatching();
    }

    private void CaughtFish()
    {
        uiManager.EnableAlert(false);
        int fishWeight = Random.Range(3, 20);
        boat.ChangeWeight(fishWeight);
        gameManager.FishAmount++;
        gameManager.LastWeight = fishWeight;
        uiManager.UpdateFishAmount();
        AddModelDeck("fish");
        dialogueManager.StartDialogue(Dialog.FISH);
    }

    private void CaughtBottle()
    {
        gameManager.BottleGet = true;
        uiManager.EnableBottleFull(true);
        dialogueManager.StartDialogue(Dialog.BOTTLE);
    }

    private void CaughtKey()
    {
        gameManager.KeyGet = true;
        uiManager.EnableKey(true);
        AddModelDeck("key");
        dialogueManager.StartDialogue(Dialog.KEY);
    }

    private void CaughtChest()
    {
        gameManager.ChestGet = true;
        uiManager.EnableChest(true);
        AddModelDeck("chest");
        dialogueManager.StartDialogue(Dialog.CHEST);
    }
    private void CaughtJunk()
    {
        //gameManager.FishAmount++;
        //UIManager.UpdateFishAmount();
    }

    private void StopCatching()
    {
        fishBit = false;
        player.throwSuccesful = false;
        gameManager.ResetClamp();
        player.bait.gameObject.SetActive(false);
        player.baitLineActive = false;
        timedClick = false;
        fishCatch = false;
        timerStarted = false;
        delayStarted = false;
        uiManager.EnableAlert(false);
    }

    private void AddModelDeck(string model)
    {
        if (model == "fish")
        {
             fish.gameObject.SetActive(true);
        }
        else if(model == "chest")
        {
            chest.gameObject.SetActive(true);
        }
        else if (model == "key")
        {
            key.gameObject.SetActive(true);
        }
        else
            Debug.Log("No model given!");

    }
    IEnumerator FishCatchDelay()
    {
        delayStarted = true;
        yield return new WaitForSeconds(Random.Range(5, 20));
        fishBit = true;
        delayStarted = false;
    }
    IEnumerator FishBit()
    {
        delayStarted = true;
        timerStarted = true;
        yield return new WaitForSeconds(Random.Range(1, 4));
        if (timedClick){
            timerStarted = false;
            fishCatch = true;
            delayStarted = false;
        }
        else
            StopCatching();
    }
}