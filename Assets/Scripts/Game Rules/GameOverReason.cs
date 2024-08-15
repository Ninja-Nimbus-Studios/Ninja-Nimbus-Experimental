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
    private const int PASSING_FINAL_SCORE = 5;

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetString("Status") == GameManager.STATUS_GAMEOVER && !hasSetGameOverMessage)
        {
            Debug.Log($"GameOverReason.cs:\nCountDownTimer.currentTime={CountDownTimer.currentTime}\nScore.score={Score.score}\nScore.finalScore={Score.finalScore}");
            
            // When time is 0
            if(CountDownTimer.currentTime == 0)
            {
                TimeUpMessage();
            }
            // Otherwise, time is not 
            else if(Score.finalScore < PASSING_FINAL_SCORE)
            {
                NegativeScoreMessage();
            }
            else
            {
                GenericGameOverMessage();
                Debug.LogError("Game Over: Game Over detection was not handled properly..");
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
        deathReasonField.text = $"Final Score: {Score.finalScore}\nYou made too many mistakes or didn't go fast enough!";
        hasSetGameOverMessage = true;
    }

    private void GenericGameOverMessage()
    {
        deathReasonField.text = $"GAME OVER!\nCountDownTimer.currentTime={CountDownTimer.currentTime}\nScore.score={Score.score}\nScore.finalScore={Score.finalScore}";
        hasSetGameOverMessage = true;
    }

}
