using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public const float TRANSITION_TIME = 2f;
    void Update()
    {
        if(Score.score == Score.maxScore[SceneManager.GetActiveScene().buildIndex - 1])
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        // Give time to play Nimbus animation

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play Animation
        transition.SetTrigger("Goal");

        yield return new WaitForSeconds(TRANSITION_TIME);

        SceneManager.LoadScene(levelIndex);
    }

}