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

    private void InitializeAudioAccordingToToggle(){
        if(PlayerPrefs.HasKey("Sound")){
            AudioManager.Instance.SetSound(PlayerPrefs.GetInt("Sound") == 1);
            SoundToggle.isOn = PlayerPrefs.GetInt("Sound") == 1;
        }else{
            AudioManager.Instance.SetSound(true);
            SoundToggle.isOn = true;
        }
        if(PlayerPrefs.HasKey("Music")){
            AudioManager.Instance.SetMusic(PlayerPrefs.GetInt("Music") == 1);
            MusicToggle.isOn = PlayerPrefs.GetInt("Music") == 1;
        }else{
            AudioManager.Instance.SetMusic(true);
            MusicToggle.isOn = true;
        }
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
        SavesSettingToPlayerPrefs();
        Application.Quit();
    }

    public void Restart(){
        SavesSettingToPlayerPrefs();
        Time.timeScale = 1;
        Unpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuClosed(){
        SavesSettingToPlayerPrefs();
        Resume();
        this.gameObject.SetActive(false);
    }

    public void OnMenuOpened(){
        Pause();
        this.gameObject.SetActive(true);
    }

    private void SavesSettingToPlayerPrefs(){
        PlayerPrefs.SetInt("Sound", SoundToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Music", MusicToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleSound(){
        if(SoundToggle.isOn){
            AudioManager.Instance.TurnOnSound();
        } else {
            AudioManager.Instance.TurnOffSound();
        }
        PlayerPrefs.SetInt("Sound", SoundToggle.isOn ? 1 : 0);
    }
    public void ToggleMusic(){
        if(MusicToggle.isOn){
            AudioManager.Instance.TurnOnMusic();

        } else {
            AudioManager.Instance.TurnOffMusic();
        }
        PlayerPrefs.SetInt("Music", MusicToggle.isOn ? 1 : 0);
    }
}
