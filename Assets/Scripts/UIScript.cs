using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject marketplaceMenu;

    //Continue game at normal speed
    public void ResumeGame()
    {
        GetComponent<PauseScript>().UnPauseGame();
    }
}
