using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]TMP_Text scoreField;
    float score => GameStats.Instance.HeightScore;

    void OnEnable(){
        NimbusEvents.OnGameEnd += ShowFinalScore;
    }

    void OnDisable(){
        NimbusEvents.OnGameEnd -= ShowFinalScore;
    }

    void SetBestScore(){
        if(PlayerPrefs.HasKey("bestScore"))
        {
            var bestScore = PlayerPrefs.GetInt("bestScore");
            PlayerPrefs.SetInt("bestScore", Mathf.RoundToInt(Mathf.Max(bestScore, score)));
            PlayerPrefs.Save();
        }
        else
        {   
            PlayerPrefs.SetInt("bestScore", Mathf.RoundToInt(score));
            PlayerPrefs.Save();
        }
    }

    void ShowFinalScore(){
        SetBestScore();
        scoreField.text = $"Best Score: {PlayerPrefs.GetInt("bestScore")} m\nFinal Height: {Mathf.RoundToInt(score)} m";
    }
}
