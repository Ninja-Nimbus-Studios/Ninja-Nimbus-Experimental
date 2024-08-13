using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static int stageGoalPoint;

    public static int[] maxScore = {40, 60};
    public TextMeshProUGUI pointField;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        string curScore = score.ToString();
        pointField.text = $"{curScore}";
    }

    public void CalculateFinalScore()
    {
        int time = (int)CountDownTimer.currentTime;
        int finalScore = score + time - Mistake.mistake;
        pointField.text = $"{finalScore.ToString()}";
    }
}
