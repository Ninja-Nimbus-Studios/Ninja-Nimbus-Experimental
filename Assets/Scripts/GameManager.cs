using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button easyButton;
    public Button hardButton;
    public Button playAgainButton;
    public Button homeButton;
    public GameObject gameStartCanvas;
    public GameObject gameOverCanvas;
    public GameObject scoreCanvas;
    public GroundManager groundManager;
    public static float speedOfPipe;

    private void Start()
    {
        // Set Canvas
        gameStartCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        scoreCanvas.SetActive(false);

        // Assign the buttons to their respective functions
        easyButton.onClick.AddListener(() => StartGame("Easy"));
        hardButton.onClick.AddListener(() => StartGame("Hard"));
        playAgainButton.onClick.AddListener(() => RestartGame());
        homeButton.onClick.AddListener(() => GoHome());
        speedOfPipe = 1.7f;
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

    public void GameOver()
    {
        PlayerPrefs.SetString("Status", "End");
        gameStartCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
        Time.timeScale = 0;

        groundManager.StopGround();
        // SceneManager.LoadScene("NimbusScene");
    }

}