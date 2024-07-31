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
        // nextCanvas.SetActive(false);

        // Assign the buttons to their respective functions
        startButton.onClick.AddListener(() => StartGame("Easy"));
        hardButton.onClick.AddListener(() => SecretAddScore());
        playAgainButton.onClick.AddListener(() => RestartGame());
        // homeButton.onClick.AddListener(() => GoHome());
        // nextButton.onClick.AddListener(() => NextStage());
        speedOfPipe = 1.7f;
        Debug.Log("GameManager: " + speedOfPipe);

        // Reset time scale
        Time.timeScale = 1;
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
        Debug.Log($"{Time.timeScale}");
        Debug.Log($"Scene Index is: {SceneManager.GetActiveScene().buildIndex}");
        SceneManager.LoadScene("Stage 2-1");
        InitializeGame();
    }

    private void GoHome()
    {
        SceneManager.LoadScene("NimbusScene");
        Time.timeScale = 0f;
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
        PlayerPrefs.SetString("Status", "GameOver");
        Time.timeScale = 0f;
        gameStartCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
        // SceneManager.LoadScene("NimbusScene");
    }

    public void NextStage()
    {
        Debug.Log("Move to next stage!");
        PlayerPrefs.SetString("Status", "NextStage");
        Time.timeScale = 0;
        SceneManager.LoadScene(1);
    }

}