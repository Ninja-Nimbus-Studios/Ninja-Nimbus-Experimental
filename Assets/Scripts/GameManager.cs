using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button startButton;
    public Button hardButton;
    public Button playAgainButton;
    // public Button homeButton;
    // public Button nextButton;
    public GameObject gameStartCanvas;
    public GameObject gameOverCanvas;
    public GameObject nextCanvas;
    public GameObject scoreCanvas;
    public GroundManager groundManager;
    public static float speedOfPipe;
    private int roundScore;

    // Status constants
    public const String STATUS_JUMP = "Jumping";
    public const String STATUS_REST = "Resting";
    public const String STATUS_GAMECLEAR = "GameClear";
    public const String STATUS_GAMEOVER = "GameOver";

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Set Canvas
        gameStartCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        scoreCanvas.SetActive(true);

        // Assign the buttons to their respective functions
        startButton.onClick.AddListener(() => StartGame("Easy"));
        hardButton.onClick.AddListener(() => SecretAddScore());
        playAgainButton.onClick.AddListener(() => RestartGame());
        speedOfPipe = 1.7f;
        Debug.Log("GameManager: " + speedOfPipe);

        // Reset time scale
        Time.timeScale = 1;

        // Reset jump count
        NimbusJump.jumpCount = 0;

        // Set Game Status
        PlayerPrefs.SetString("Status", STATUS_REST);
    }

    private void SecretAddScore()
    {
        Score.score++;
        hardButton.gameObject.SetActive(false);
    }
    private void StartGame(string difficulty)
    {
        // Set the difficulty level
        PlayerPrefs.SetString("Difficulty", difficulty);
        PlayerPrefs.SetString("Status", "Start");
        Debug.Log($"Game will start with {difficulty}");
    
        // Set Canvas
        gameStartCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    private void RestartGame()
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
    public void CheckSuccessfulJump()
    {
        Debug.Log("Check Successful Jump!");
        NimbusJump.scoreAfterJump = Score.score;
        Debug.Log($"Before:{NimbusJump.scoreBeforeJump}, After:{NimbusJump.scoreAfterJump}");
        if(NimbusJump.scoreAfterJump == NimbusJump.scoreBeforeJump){
            GameOver();
        }
    }

    public void EndOfGame()
    {
        Debug.Log("End of Game is running.");
        groundManager.StopGround();
        roundScore = Score.score;
        if (roundScore == 14)
        {
            scoreCanvas.SetActive(false);
            nextCanvas.SetActive(true);
        }
        else 
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        PlayerPrefs.SetString("Status", "GameOver");
        Time.timeScale = 0f;
        gameStartCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
        // SceneManager.LoadScene("NimbusScene");
    }

    public void GameCleared()
    {
        Debug.Log("Game Cleared!");
        Time.timeScale = 0f;
        RestartGame();
    }

    public void NextStage()
    {
        Debug.Log("Move to next stage!");
        Time.timeScale = 0;
        SceneManager.LoadScene(1);
    }
}