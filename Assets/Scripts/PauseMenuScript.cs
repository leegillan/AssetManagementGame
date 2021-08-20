using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenuVisual;
    public GameObject SettingsMenuVisual;

    public void ExitToMenu()
    {
        Debug.Log("Exit to Menu");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        GetComponent<PauseScript>().UnPauseGame();
    }
}
