using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenuVisual;

    public void ResumeGame()
    {
        GetComponent<PauseScript>().UnPauseGame();
    }
}
