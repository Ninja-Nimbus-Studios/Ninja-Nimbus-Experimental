using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public Text textField;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        string curScore = score.ToString();
        if(PlayerPrefs.GetString("Status") == GameManager.STATUS_GAMECLEAR)
        {
            textField.text = "You are at the top!";
        }
        else
        {
            textField.text = $"{curScore}m";
        }
    }
}
