using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class DeathReason : MonoBehaviour
{
    public TextMeshProUGUI deathReasonField;
    private bool hasSetGameOverMessage = false;

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetString("Status") == GameManager.STATUS_GAMEOVER && !hasSetGameOverMessage)
        {
            Debug.Log($"GameOverReason.cs:\nCountDownTimer.currentTime={CountDownTimer.currentTime}\nScore.score={Score.score}\nScore.finalScore={Score.finalScore}");
            
            if(CountDownTimer.currentTime == 0 && Score.score < Score.maxScore[SceneManager.GetActiveScene().buildIndex - 1])
            {
                TimeUpMessage();
            }
            else if(CountDownTimer.currentTime == 0 && Score.score == Score.maxScore[SceneManager.GetActiveScene().buildIndex - 1] && Score.finalScore < 5)
            {
                NegativeScoreMessage();
            }
        }
    }

    private void TimeUpMessage()
    {
        deathReasonField.text = "Time is up!";
        hasSetGameOverMessage = true;
    }

    private void NegativeScoreMessage()
    {
        deathReasonField.text = $"Your final score was: {Score.finalScore}.\nYou made too many mistakes or didn't go fast enough!";
        hasSetGameOverMessage = true;
    }
}
