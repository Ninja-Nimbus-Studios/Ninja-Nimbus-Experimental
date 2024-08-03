using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button playAgainButton;
    // public Button homeButton;
    // public Button nextButton;
    public GameObject gameStartCanvas;
    public GameObject gameOverCanvas;
    public GameObject gameInterface;
    public GameObject gameClearCanvas;
    private int roundScore;

    // Status constants
    public const String STATUS_JUMP = "Jumping";
    public const String STATUS_REST = "Resting";
    public const String STATUS_GAMECLEAR = "GameClear";
    public const String STATUS_GAMEOVER = "GameOver";

    private void Start()
    {
        InitializeGame();

        // Assign the buttons to their respective functions
        playAgainButton.onClick.AddListener(() => RestartGame());
    }

    /*
        Game initializes 
    */
    public void InitializeGame()
    {
        // Set Canvas
        gameInterface.SetActive(true);
        gameStartCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        gameClearCanvas.SetActive(false);

        // Reset time scale
        Time.timeScale = 1;

        // Reset jump count
        NimbusJump.jumpCount = 0;

        // Set Game Status
        PlayerPrefs.SetString("Status", STATUS_REST);
    }

    /*
        Game Restarts from game over state which calls initialize game.
        This function is often times part of OnClick on PlayAgain Button in different canvas.
    */
    public void RestartGame()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene("Stage 2-1");
        var status = PlayerPrefs.GetString("Status");
        if(status == STATUS_GAMEOVER)
        {
            InitializeGame();
        }
        else
        {
            Debug.LogError($"The current game status doesn't allow RestartGame(). Current status is {status}");
        }
    }

    /*
        Successfully finishes the stage before countdown hits 0
    */
    public void GameCleared()
    {
        Debug.Log("Game Cleared!");
        Time.timeScale = 0f;
        gameInterface.SetActive(false);
        gameClearCanvas.SetActive(true);
    }

    /*
        Unsuccessfully finishing the stage either because the countdown hit 0 before getting to top,
        or picked wrong direction to jump.
    */
    public void GameOver()
    {
        Debug.Log("Game Over!");
        PlayerPrefs.SetString("Status", "GameOver");
        Time.timeScale = 0f;
        gameInterface.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}