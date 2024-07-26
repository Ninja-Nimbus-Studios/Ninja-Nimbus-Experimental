using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button startButton;
    public Button hardButton;
    public Button playAgainButton;
    public Button homeButton;
    public Button nextButton;
    public GameObject gameStartCanvas;
    public GameObject gameOverCanvas;
    public GameObject nextCanvas;
    public GameObject scoreCanvas;
    public GroundManager groundManager;
    public static float speedOfPipe;
    private int roundScore;

    private void Start()
    {
        // Set Canvas
        gameStartCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        scoreCanvas.SetActive(false);
        nextCanvas.SetActive(false);

        // Assign the buttons to their respective functions
        startButton.onClick.AddListener(() => StartGame("Easy"));
        hardButton.onClick.AddListener(() => SecretAddScore());
        playAgainButton.onClick.AddListener(() => RestartGame());
        homeButton.onClick.AddListener(() => GoHome());
        nextButton.onClick.AddListener(() => RestartGame());
        speedOfPipe = 1.7f;
        Debug.Log("GameManager: " + speedOfPipe);
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
        Debug.Log($"Scene Index is: {SceneManager.GetActiveScene().buildIndex}");
        Time.timeScale = 0;
        SceneManager.LoadScene("NimbusScene");
    }

    private void GoHome()
    {
        SceneManager.LoadScene("NimbusScene");
        Time.timeScale = 0;
    }

    public void EndOfGame()
    {
        Debug.Log("End of Game is running.");
        roundScore = Score.score;
        if (roundScore == 14)
        {
            NextStage();
        } else 
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetString("Status", "GameOver");
        gameStartCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
        Time.timeScale = 0;

        groundManager.StopGround();
        // SceneManager.LoadScene("NimbusScene");
    }

    public void NextStage()
    {
        Debug.Log("Move to next stage!");
        PlayerPrefs.SetString("Status", "NextStage");
        scoreCanvas.SetActive(false);
        nextCanvas.SetActive(true);
        Time.timeScale = 0;
    }

}