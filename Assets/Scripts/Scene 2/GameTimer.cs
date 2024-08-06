using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour 
{
    public float countDownTime = 1 * 60f;
    private float currentTime;
    private bool isTiming = false;

    public TextMeshProUGUI timerText;
    public GameManager gameManager;

    void Start()
    {
        countDownTime = 0.75f * 60f;
    }

    void Update()
    {
        // Countdown should start in the beginning when timing hasn't started yet
        if(PlayerPrefs.GetString("Status") == GameManager.STATUS_REST && !isTiming)
        {
            StartCountDown(); // Start the countdown
        }
        // Countdown should stop if GameClear has been called
        else if(PlayerPrefs.GetString("Status") == GameManager.STATUS_GAMECLEAR && isTiming)
        {
            StopCountDown();
            gameManager.GameCleared();
        }

        // While game is timing, update display with current time
        if (isTiming)
        {
            currentTime -= Time.deltaTime;

            // Countdown should stop if GameClear has not been called and time is 0
            if (currentTime <= 0)
            {
                currentTime = 0;
                StopCountDown();
                gameManager.GameOver();
            }

            UpdateTimerDisplay();
        }
    }

    private void StartCountDown()
    {
        currentTime = countDownTime; // Reset elapsed time
        isTiming = true;
        Debug.Log("Timer started");
    }

    private void StopCountDown()
    {
        isTiming = false;
        Debug.Log("Timing stopped");
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}