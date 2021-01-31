using System.Collections;
using UnityEngine;

public class FishCatching : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Boat boat;
    
    [SerializeField] private int chestChance = 20;
    [SerializeField] private int keyChance = 20;
    [SerializeField] private int numFishBeforeSpecial = 2;
    
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
            StartCoroutine(FishDelay());
        
        if (!fishCaught) return;
        Debug.Log("You got something!");
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
        int fishWeight = Random.Range(3, 20);
        boat.ChangeWeight(fishWeight);
        gameManager.FishAmount++;
        gameManager.LastWeight = fishWeight;
        UIManager.UpdateFishAmount();
        dialogueManager.StartDialogue(Dialog.FISH);
    }

    private void CaughtBottle()
    {
        gameManager.BottleGet = true;
        UIManager.EnableBottle(true);
        dialogueManager.StartDialogue(Dialog.BOTTLE);
    }

    private void CaughtKey()
    {
        gameManager.KeyGet = true;
        UIManager.EnableKey(true);
        dialogueManager.StartDialogue(Dialog.KEY);
    }

    private void CaughtChest()
    {
        gameManager.ChestGet = true;
        UIManager.EnableChest(true);
        dialogueManager.StartDialogue(Dialog.CHEST);
    }
    private void CaughtJunk()
    {
        //gameManager.FishAmount++;
        //UIManager.UpdateFishAmount();
    }

    private void StopCatching()
    {
        fishCaught = false;
        player.throwSuccesful = false;
        gameManager.ResetClamp();
        player.bait.gameObject.SetActive(false);
        player.baitLineActive = false;
    }
    IEnumerator FishDelay()
    {
        delayStarted = true;
        yield return new WaitForSeconds(Random.Range(5, 20));
        fishCaught = true;
        delayStarted = false;
    }
}