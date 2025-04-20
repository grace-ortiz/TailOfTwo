using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenuUI;

    public PlayerMovement playerMovement;
    public Slider SliderMusic;
    public Slider SliderSFX;
    private bool isVisible;

    void Start() 
    {
        Time.timeScale = 1f;
        isVisible = false;
        PauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    // public void ExitGame()
    // {
    //     GameManager.Instance.ExitGame();
    // }

    public void TogglePause()
    {
        if(!isVisible) // if not visible, pause game
        {
            PauseMenuUI.SetActive(true);
            isVisible = true;
            Time.timeScale = 0f;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.pauseSound, this.transform.position);
        }
        else // if visible, resume game
        {
            PauseMenuUI.SetActive(false);
            isVisible = false;
            Time.timeScale = 1f;
            
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0); // StartScene
    }

    // public void SetMusicVolume() { AudioManager.instance.ChangeMusicVolume(SliderMusic.value); }
    // public void SetSFXVolume() { AudioManager.instance.ChangeSFXVolume(SliderSFX.value); }
}