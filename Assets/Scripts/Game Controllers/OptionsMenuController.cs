﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuController : MonoBehaviour {

    [SerializeField]
    private GameObject easySign;
    [SerializeField]
    private GameObject mediumSign;
    [SerializeField]
    private GameObject hardSign;


    // Use this for initialization
    void Start () {
        SetInitialDifficultyInOptionsMenu();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void InitialDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "easy":
                easySign.SetActive(true);
                mediumSign.SetActive(false);
                hardSign.SetActive(false);
                break;

            case "medium":
                easySign.SetActive(false);
                mediumSign.SetActive(true);
                hardSign.SetActive(false);
                break;

            case "hard":
                easySign.SetActive(false);
                mediumSign.SetActive(false);
                hardSign.SetActive(true);
                break;
        }
    }

    void SetInitialDifficultyInOptionsMenu()
    {
        if (GamePreferences.GetEasyDifficultyState() == 1)
        {
            InitialDifficulty("easy");
        }

        if (GamePreferences.GetMediumDifficultyState() == 1)
        {
            InitialDifficulty("medium");
        }

        if (GamePreferences.GetHardDifficultyState() == 1)
        {
            InitialDifficulty("hard");
        }
    }

    public void EasyDifficulty()
    {

        GamePreferences.SetEasyDifficultyState(1);
        GamePreferences.SetMediumDifficultyState(0);
        GamePreferences.SetHardDifficultyState(0);

        easySign.SetActive(true);
        mediumSign.SetActive(false);
        hardSign.SetActive(false);
    }

    public void MediumDifficulty()
    {

        GamePreferences.SetEasyDifficultyState(0);
        GamePreferences.SetMediumDifficultyState(1);
        GamePreferences.SetHardDifficultyState(0);

        easySign.SetActive(false);
        mediumSign.SetActive(true);
        hardSign.SetActive(false);
    }

    public void HardDifficulty()
    {

        GamePreferences.SetEasyDifficultyState(0);
        GamePreferences.SetMediumDifficultyState(0);
        GamePreferences.SetHardDifficultyState(1);

        easySign.SetActive(false);
        mediumSign.SetActive(false);
        hardSign.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}