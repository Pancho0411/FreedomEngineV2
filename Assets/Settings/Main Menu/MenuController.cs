using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MenuController : MonoBehaviour
{

    public bool quitting = false;
    public GameObject confirmUI;
    public GameObject optionsUI;
    public GameObject menuUI;
    public string Level;
    public GameObject loadingUI;

    //start game when start button is pressed
    public void StartGame()
    {
        loadingUI.SetActive(true);
        SceneManager.LoadSceneAsync(Level);
    }

    //open options menu
    public void OptionsUI(bool isOn)
    {
        if (isOn)
        {
            menuUI.SetActive(false);
            optionsUI.SetActive(true);
        }
        else
        {
            menuUI.SetActive(true);
            optionsUI.SetActive(false);
        }
    }

    //open quit confirmation menu
    public void ConfirmUI(bool isOn)
    {
        if (isOn)
        {
            menuUI.SetActive(false);
            confirmUI.SetActive(true);
        }
        else
        {
            menuUI.SetActive(true);
            confirmUI.SetActive(false);
        }
    }

    //quit the game
    //does not work on android
    public void EndGame()
    {
        //check if quitting variable is true
        if (quitting)
        {
            //load main menu, by default is first scene
            Application.Quit();
        }
    }

    //do not quit game
    public void RejectQuit()
    {
        //check if quitting variable is false
        quitting = false;

        //if false, disable confirmation UI
        confirmUI.SetActive(false);
    }
}
