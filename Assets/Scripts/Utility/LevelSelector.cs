using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public string level;
    public Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = level;
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("Stage " + level);
    }
}
