using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private TMP_Text titleField;
    [SerializeField] private float textRunSpeed = 0.1f;
    [SerializeField] private GameObject bottleMessage;
    
    [Header("Fisher sprites")]
    [SerializeField] private Image regularExpression;
    [SerializeField] private Image wonderingExpression;
    [SerializeField] private Image surprisedExpression;

    private int dialogIncrement;
    private bool dialogFinished = false;
    private bool dialogStopped = false;
    private Dialog currentType;

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
                    break;
                default:
                    StopDialogue();
                    break;
            }
        }
        else if (type == Dialog.FISH)
        {
            dialogBox.SetActive(true);
        }
        else if (type == Dialog.BOTTLE)
        {
            dialogBox.SetActive(true);
            switch (dialogIncrement)
            {
                case 0:
                    PrintDialog("Today is the day my friend! I can feel something huge!", "Fisherman");
                    break;
                case 1:
                    PrintDialog("Well this is weird, what’s this? Some kind of paper in the bottle? Why would someone throw something like this to the ocean?", "Fisherman");
                    break;
                case 2:
                    ShowMessage(true);
                    break;
                case 3:
                    ShowMessage(false);
                    break;
                case 4:
                    PrintDialog("What a weird map, but it talks about huge treasure. I think it’s time to try fish it up and maybe we hit something big.", "Fisherman");
                    break;
                default:
                    StopDialogue();
                    break;
            }
        }
        if (type == Dialog.CHEST)
        {
            dialogBox.SetActive(true);
            switch (dialogIncrement)
            {
                case 0:
                    PrintDialog("This must be the chest message was talking about. It’s so heavy, there must be something inside.", "Fisherman");
                    break;
                default:
                    StopDialogue();
                    break;
            }
        }
        if (type == Dialog.KEY)
        {
            dialogBox.SetActive(true);
            switch (dialogIncrement)
            {
                case 0:
                    PrintDialog("Good thing this is not as small as a regular key, otherwise a fish might’ve eaten it. I think this goes to the chest.", "Fisherman");
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
    private void ContinueGame()
    {
        
    }
    public void StopDialogue()
    {
        dialogBox.SetActive(false);
        dialogStopped = true;
        gameManager.enableFishing = true;
        gameManager.mouseLookEnabled = true;
        dialogIncrement = 0;
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
}
