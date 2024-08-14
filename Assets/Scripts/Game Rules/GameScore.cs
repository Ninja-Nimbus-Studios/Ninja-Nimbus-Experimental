using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static int[] maxScore = {40, 40, 40};
    public TextMeshProUGUI pointField;
    private string curScore;
    public static int finalScore;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curScore = score.ToString();
        pointField.text = $"{curScore}";
        finalScore = (int)CountDownTimer.currentTime - Mistake.mistake;
    }
}
