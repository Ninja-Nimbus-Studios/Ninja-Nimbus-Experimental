using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Toggle SoundToggle;
    [SerializeField] private Toggle MusicToggle;

    void Start(){
        Time.timeScale = 1;
    }
    private void Pause(){
        Time.timeScale = 0;
        GameManager.isGamePaused = true;
    }

    private void Resume(){
        Time.timeScale = 1;

        //add a 0.1 second delay to prevent the player from moving immediately after unpausing
        Invoke("Unpause", 0.1f);
    }

    private void Unpause(){
        GameManager.isGamePaused = false;
    }

    public void Quit(){
        Application.Quit();
    }

    public void Restart(){
        Time.timeScale = 1;
        Unpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuClosed(){
        Resume();
        this.gameObject.SetActive(false);
    }

    public void OnMenuOpened(){
        Pause();
        this.gameObject.SetActive(true);
    }

    public void ToggleSound(){
        if(SoundToggle.isOn){
            AudioManager.Instance.TurnOnSound();
        } else {
            AudioManager.Instance.TurnOffSound();
        }
    }
    public void ToggleMusic(){
        if(MusicToggle.isOn){
            AudioManager.Instance.TurnOnMusic();
        } else {
            AudioManager.Instance.TurnOffMusic();
        }
    }
}
