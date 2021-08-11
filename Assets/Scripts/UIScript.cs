using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    //Continue game at normal speed
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
