using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;
    
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private SceneLoader sceneLoader;
    
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        playButton.onClick.AddListener(StartGame);
        backButton.onClick.AddListener(Back);
        helpButton.onClick.AddListener(Help);
        exitButton.onClick.AddListener(Exit);
        
        sceneLoader.gameObject.SetActive(true);
    }

    private void Back()
    {
        helpPanel.gameObject.SetActive(false);
    }

    private void StartGame()
    {
        sceneLoader.LoadGame();
    }

    private void Help()
    {
        helpPanel.gameObject.SetActive(true);
    }
    private void Exit()
    {
        Application.Quit();
    }
}
