using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]TMP_Text timerField;
    float time;

    public void ResetTimer(){
        time = 0;
    }
    void Update(){
        //format the time to minutes and seconds
        if(GameManager.isGamePaused) return;
        else{
            UpdateTimer();
        }
        FormatTime();
    }

    void UpdateTimer(){
        time += Time.deltaTime;
    }

    void FormatTime(){
        string minutes = ((int)time / 60).ToString();
        string seconds = (time % 60).ToString("00");
        timerField.text = $"{minutes}:{seconds}";
    }
}
