using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    void ShowFinalScore(){
        scoreField.text = $"Final Height: {Mathf.RoundToInt(score)} m";
    }
}
