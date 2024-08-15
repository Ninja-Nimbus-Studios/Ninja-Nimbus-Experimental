using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mistake : MonoBehaviour
{
    public static int mistake = 0;
    public TextMeshProUGUI mistakeField;
    // Start is called before the first frame update
    void Start()
    {
        mistake = 0;
    }

    // Update is called once per frame
    void Update()
    {
        string curMistake = mistake.ToString();
        mistakeField.text = $"{curMistake}";
    }
}
