using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]TMP_Text timerField;

    void Update(){
        //format the time to minutes and seconds
        float t = Time.timeSinceLevelLoad;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("00");
        timerField.text = $"{minutes}:{seconds}";
    }
}
