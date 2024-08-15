using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ClearScore : MonoBehaviour
{
    public TextMeshProUGUI finalScoreField;
    private int finalScore;
    private bool hasSetClearMessage = false;

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetString("Status") == GameManager.STATUS_GAMECLEAR && !hasSetClearMessage)
        {
            Debug.Log("GameClearScore.cs -> Update: Calculating final score...");
            CalculateFinalScore();
        }
    }

    public void CalculateFinalScore()
    {
        finalScore = (int)CountDownTimer.currentTime - Mistake.mistake;
        finalScoreField.text = $"Time:{(int)CountDownTimer.currentTime}; Mistakes:{Mistake.mistake}\nFinal Score:{finalScore.ToString()}";
        hasSetClearMessage = true;
    }
}
