﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioClip waterAmbiance;
    

    public bool enableFishing = true;
    public bool mouseLookEnabled = true;
    public int FishAmount { get; set; } = 0;
    public int LastWeight { get; set; } = 0;
    public bool BottleGet { get; set; }
    public bool ChestGet { get; set; }
    public bool KeyGet { get; set; }
    public float CamClampAngleYMax { get; set; } = 220;
    public float CamClampAngleYMin { get; set; } = 0;

    public void ResetClamp()
    {
        CamClampAngleYMax = 220;
        CamClampAngleYMin = 0;
    }

    private void Start()
    {
        sceneLoader.gameObject.SetActive(true);
        SoundManager.Instance.PlayMusicSecond(waterAmbiance);
    }

    private void Update()
    {
        // DEBUG REMOVE BEFORE BUILDING
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
