using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private FishCatching fishCatching;
    
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private TMP_Text titleField;
    [SerializeField] private float textRunSpeed = 0.1f;
    [SerializeField] private GameObject bottleMessage;
    [SerializeField] private Image characterImage;
    [SerializeField] private AudioClip sealSound;
    [SerializeField] private AudioClip hornSound;
    [SerializeField] private AudioClip engineSound;
    [SerializeField] private GameObject endScreen;
    
    [Header("Fisher sprites")]
    [SerializeField] private Sprite regularExpression;
    [SerializeField] private Sprite wonderingExpression;
    [SerializeField] private Sprite surprisedExpression;
    [SerializeField] private Sprite seal;
    
    [Header("Models")] 
    [SerializeField] private GameObject modelPos;
    [SerializeField] private TMP_Text modelTitle;
    [SerializeField] private GameObject fish;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject bottle;
    
    [Header("Credits stuff")]
    public Animator transition;
    public float transitionTime = 1f;
    private static readonly int StartAnim = Animator.StringToHash("Start");

    private int dialogIncrement;
    private bool dialogFinished = false;
    private bool dialogStopped = false;
    private Dialog currentType;

    private bool gameEnded = false;

    private void Start()
    {
        StartDialogue(Dialog.START);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogFinished && !dialogStopped)
        {
            dialogIncrement++;
            StartDialogue(currentType);
        }
    }

    private void PrintDialog(string message, string title)
    {
        dialogFinished = false;
        titleField.text = title;
        textField.text = message;
        StartCoroutine(TextPrint(message));
    }
    
    public void StartDialogue(Dialog type)
    {
        gameManager.enableFishing = false;
        gameManager.mouseLookEnabled = false;
        dialogStopped = false;
        currentType = type;
        
        if (type == Dialog.START)
        {
            dialogBox.SetActive(true);
            switch (dialogIncrement)
            {
                case 0:
                    PrintDialog("Okay buddy, time to get fishing. Let’s see what kind of fish we are going to get today. ", "Fisherman");
                    characterImage.sprite = regularExpression;
                    break;
                default:
                    StopDialogue();
                    break;
            }
        }
        else if (type == Dialog.FISH)
        {
            switch (dialogIncrement)
            {
                case 0:
                    DisplayModel("You caught a fish!", "fish", true);
                    break;
                default:
                    StopDialogue();
                    break;
            }
        }
        else if (type == Dialog.BOTTLE)
        {
            dialogBox.SetActive(true);
            switch (dialogIncrement)
            {
                case 0:
                    PrintDialog("Today is the day my friend! I can feel something huge!", "Fisherman");
                    characterImage.sprite = regularExpression;
                    break;
                case 1:
                    dialogBox.SetActive(false);
                    DisplayModel("You caught a bottle!", "bottle", true);
                    characterImage.sprite = regularExpression;
                    break;
                case 2:
                    dialogBox.SetActive(true);
                    DisplayModel("", "", false);
                    PrintDialog("Well this is weird, what’s this? Some kind of paper in the bottle? Why would someone throw something like this to the ocean?", "Fisherman");
                    characterImage.sprite = wonderingExpression;
                    break;
                case 3:
                    ShowMessage(true);
                    uiManager.EnableBottleFull(false);
                    uiManager.EnableBottleEmpty(true);
                    break;
                case 4:
                    ShowMessage(false);
                    break;
                case 5:
                    PrintDialog("What a weird map, but it talks about huge treasure. I think it’s time to try fish it up and maybe we hit something big.", "Fisherman");
                    characterImage.sprite = regularExpression;
                    break;
                default:
                    StopDialogue();
                    break;
            }
        }
        else if (type == Dialog.CHEST)
        {
            dialogBox.SetActive(true);
            switch (dialogIncrement)
            {
                case 0:
                    dialogBox.SetActive(false);
                    DisplayModel("You caught a chest!", "chest", true);
                    characterImage.sprite = wonderingExpression;
                    break;
                case 1:
                    dialogBox.SetActive(true);
                    DisplayModel("", "", false);
                    PrintDialog("This must be the chest the message was talking about. It’s so heavy, there must be something inside.", "Fisherman");
                    characterImage.sprite = wonderingExpression;
                    break;
                default:
                    StopDialogue();
                    break;
            }
        }
        else if (type == Dialog.KEY)
        {
            dialogBox.SetActive(true);
            switch (dialogIncrement)
            {
                case 0:
                    dialogBox.SetActive(false);
                    DisplayModel("You caught a key!", "key", true);
                    characterImage.sprite = wonderingExpression;
                    break;
                case 1:
                    dialogBox.SetActive(true);
                    DisplayModel("","", false);
                    PrintDialog("Good thing this is not as small as a regular key, otherwise a fish might’ve eaten it. I think this goes to the chest.", "Fisherman");
                    characterImage.sprite = wonderingExpression;
                    break;
                case 2:
                    PrintDialog("I got the chest and the key, time to put them together. .", "Fisherman");
                    characterImage.sprite = regularExpression;
                    break;
                case 3:
                    PrintDialog("It’s… it’s a golden seal… it looks like you buddy. There’s text at the bottom of the statue.", "Fisherman");
                    characterImage.sprite = wonderingExpression;
                    fishCatching.AddModelDeck("goldenseal");
                    break;
                case 4:
                    PrintDialog("This is the Longest John, the greatest extra member of the Wellerman’s crew and eater of small fishes. May his grandchildren rule over this small island someday. ", "Golden seal");
                    characterImage.sprite = wonderingExpression;
                    break;
                case 5:
                    PrintDialog("Old relative of yours?", "Fisherman");
                    characterImage.sprite = wonderingExpression;
                    break;
                case 6:
                    PrintDialog("Ow ow ow!!", "Seal");
                    SoundManager.Instance.Play(sealSound);
                    characterImage.sprite = seal;
                    break;
                case 7:
                    PrintDialog("Thought so, I can see the resemblance. I think we found ourselves a new friend to the bookshelf. Well this was a long day, what say you if we head back home and have a nice dinner? ", "Fisherman");
                    characterImage.sprite = regularExpression;
                    break;
                case 8:
                    PrintDialog("Ow ow!!", "Seal");
                    SoundManager.Instance.Play(sealSound);
                    characterImage.sprite = seal;
                    break;
                case 9:
                    gameManager.EndGame();
                    SoundManager.Instance.Play(hornSound);
                    StartCoroutine(PlayDelayedSound(engineSound, 6));
                    ShowEndScreen();
                    break;
                default:
                    StopDialogue();
                    break;
            }
        }
    }

    private void ShowMessage(bool state)
    {
        bottleMessage.gameObject.SetActive(state);
    }
    private void DisplayModel(string title, string type, bool state)
    {
        if (state)
        {
            modelTitle.text = title;
            modelPos.gameObject.SetActive(true);
            modelTitle.gameObject.SetActive(true);
            if (type == "fish")
            {
                GameObject newModel = Instantiate(fish, modelPos.transform, false);
                newModel.transform.position = modelPos.transform.position;
                newModel.AddComponent<ObjectSpin>();
                modelTitle.text += " Weight: " + gameManager.LastWeight+"kg";
            }
            if (type == "chest")
            {
                GameObject newModel = Instantiate(chest, modelPos.transform, false);
                newModel.transform.position = modelPos.transform.position;
                newModel.AddComponent<ObjectSpin>().axis = Vector3.up;
            }
            if (type == "key")
            {
                GameObject newModel = Instantiate(key, modelPos.transform, false);
                newModel.transform.position = modelPos.transform.position;
                newModel.AddComponent<ObjectSpin>().axis = Vector3.up;;
            }
            if (type == "bottle")
            {
                GameObject newModel = Instantiate(bottle, modelPos.transform, false);
                newModel.transform.position = modelPos.transform.position;
                newModel.AddComponent<ObjectSpin>().axis = Vector3.up;;
            }
        }
        else
        {
            modelTitle.gameObject.SetActive(false);
            modelPos.gameObject.SetActive(false);
        }
    }
    public void StopDialogue()
    {
        if (!gameManager.GameEnd)
        {
            dialogBox.SetActive(false);
            dialogStopped = true;
            gameManager.enableFishing = true;
            gameManager.mouseLookEnabled = true;
            dialogIncrement = 0;
            modelPos.gameObject.SetActive(false);
            modelTitle.gameObject.SetActive(false);
            foreach (Transform child in modelPos.transform)
                Destroy(child.gameObject);
        }
    }
    private void ShowEndScreen()
    {
        StartCoroutine(EndTransition());
        endScreen.gameObject.SetActive(true);
        transition.SetTrigger(StartAnim);
    }
    IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(transitionTime);
        uiManager.RollCredits();
    }
    IEnumerator TextPrint(string message)
    {
        for (int i = 0; i < message.Length + 1; i++)
        {
            textField.maxVisibleCharacters = i;
            yield return new WaitForSeconds(textRunSpeed);
        }
        dialogFinished = true;
    }

    IEnumerator PlayDelayedSound(AudioClip sound, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SoundManager.Instance.Play(sound);
    }
}
