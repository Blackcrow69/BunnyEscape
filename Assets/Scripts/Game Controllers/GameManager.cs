using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [HideInInspector]
    public bool isGameStartedFromMainMenu;
    [HideInInspector]
    public bool isGameRestartedAfterPlayerDied;
    [HideInInspector]
    public int score;
    [HideInInspector]
    public int coinScore;
    [HideInInspector]
    public int lifeScore;


    // Use this for initialization
    void Awake () {
        MakeSingleton();
	}

    private void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        if (!PlayerPrefs.HasKey("Game Initialized"))
        {
            GamePreferences.SetMusicState(1);

            GamePreferences.SetEasyDifficultyState(0);
            GamePreferences.SetEasyDifficultyHighscore(0);
            GamePreferences.SetEasyDifficultyCoinScore(0);

            GamePreferences.SetMediumDifficultyState(1);
            GamePreferences.SetMediumDifficultyHighscore(0);
            GamePreferences.SetMediumDifficultyCoinScore(0);

            GamePreferences.SetHardDifficultyState(0);
            GamePreferences.SetHardDifficultyHighscore(0);
            GamePreferences.SetHardDifficultyCoinScore(0);

            PlayerPrefs.SetInt("Game Initialized", 1);
        }
    }

    public void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay")
        {
            if (GameManager.instance.isGameRestartedAfterPlayerDied)
            {
                GameplayController.instance.SetScore(score);
                GameplayController.instance.SetCoinScore(coinScore);
                GameplayController.instance.SetLifeScore(lifeScore);

                PlayerScore.scoreCount = score;
                PlayerScore.coinCount = coinScore;
                PlayerScore.lifeCount = lifeScore;
            }
            else if (GameManager.instance.isGameStartedFromMainMenu)
            {
                score = 0;
                coinScore = 0;
                lifeScore = 2;

                PlayerScore.scoreCount = score;
                PlayerScore.coinCount = coinScore;
                PlayerScore.lifeCount = lifeScore;

                GameplayController.instance.SetScore(score);
                GameplayController.instance.SetCoinScore(coinScore);
                GameplayController.instance.SetLifeScore(lifeScore);
            }
        }
    }

    public void CheckGameStatus(int score, int coinScore, int lifeScore)
    {
        if (lifeScore < 0 )
        {

            if (GamePreferences.GetEasyDifficultyState() == 1)
            {

                int highscore = GamePreferences.GetEasyDifficultyHighscore();
                int highCoinScore = GamePreferences.GetEasyDifficultyCoinScore();

                if (highscore < score)
                    GamePreferences.SetEasyDifficultyHighscore(score);

                if (highCoinScore < coinScore)
                    GamePreferences.SetEasyDifficultyCoinScore(coinScore);

            }

            if (GamePreferences.GetMediumDifficultyState() == 1)
            {

                int highscore = GamePreferences.GetMediumDifficultyHighscore();
                int highCoinScore = GamePreferences.GetMediumDifficultyCoinScore();

                if (highscore < score)
                    GamePreferences.SetMediumDifficultyHighscore(score);

                if (highCoinScore < coinScore)
                    GamePreferences.SetMediumDifficultyCoinScore(coinScore);

            }

            if (GamePreferences.GetHardDifficultyState() == 1)
            {

                int highscore = GamePreferences.GetHardDifficultyHighscore();
                int highCoinScore = GamePreferences.GetHardDifficultyCoinScore();

                if (highscore < score)
                    GamePreferences.SetHardDifficultyHighscore(score);

                if (highCoinScore < coinScore)
                    GamePreferences.SetHardDifficultyCoinScore(coinScore);

            }


            isGameStartedFromMainMenu = false;
            isGameRestartedAfterPlayerDied = false;

            GameplayController.instance.GameOverShowPanel(score, coinScore);
        }
        else
        {
            this.score = score;
            this.lifeScore = lifeScore;
            this.coinScore = coinScore;

            isGameStartedFromMainMenu = false;
            isGameRestartedAfterPlayerDied = true;

            GameplayController.instance.PlayerDiedRestartTheGame();


        }
    }

}
