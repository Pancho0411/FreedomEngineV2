using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    //set variables
    public bool gameIsPaused = false;
    public bool quitting = false;
    public GameObject pauseMenuUI;
    public GameObject scoreUI;
    public GameObject confirmUI;
    public GameObject optionsUI;
    public int index = 0;

    void Start()
    {
        gameIsPaused = false;
    }

    void Update()
    {
        //checks if start button is pressed, toggles gameIsPaused, pauses game if gameIsPaused is true
        if (Input.GetButtonDown("Start"))
        {
            gameIsPaused = !gameIsPaused;
            if (!gameIsPaused)
            {
                PauseGame();
            }
            else
            {
                Resume();
            }
        }
    }

    public void PauseGame()
    {
        //if paused, sets time to 0, pauses sound, enables pause UI, disables score UI, disables options UI if enabled
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        scoreUI.SetActive(false);
    }

    public void Resume()
    {
        //if not paused, sets time to 1, unpauses sound, disables pause UI, enables score UI, disables confirm UI if enabled, disables options UI if enabled
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        scoreUI.SetActive(true);
        confirmUI.SetActive(false);
        optionsUI.SetActive(false);
    }

    public void ConfirmQuit()
    {
        //check if quitting variable is true
        quitting = true;

        //if true, enable confirmation UI, disable pause UI
        pauseMenuUI.SetActive(false);
        confirmUI.SetActive(true);
    }

    public void RejectQuit()
    {
        //check if quitting variable is false
        quitting = false;
        //if false, disable confirmation UI, enable pause UI
        pauseMenuUI.SetActive(true);
        confirmUI.SetActive(false);
    }

    public void EndGame()
    {
        //check if quitting variable is true
        if (quitting)
        {
            //disable UIs
            confirmUI.SetActive(false);
            pauseMenuUI.SetActive(false);

            //resume time
            Time.timeScale = 1f;
            //add save feature later

            //load main menu, by default is first scene
            SceneManager.LoadSceneAsync(index);
        }
    }

    public void OptionsUI(bool isOn)
    {
        if (isOn)
        {
            pauseMenuUI.SetActive(false);
            optionsUI.SetActive(true);
        }
        else
        {
            pauseMenuUI.SetActive(true);
            optionsUI.SetActive(false);
        }
    }
}
