using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private static readonly int Start = Animator.StringToHash("Start");

    public void LoadGame()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void BackToMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger(Start);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
}