using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
	public void PauseGame()
	{
		Time.timeScale = 0;
		GetComponent<InputScript>().isPaused = true;
	}

	public void UnPauseGame()
	{
		Time.timeScale = 1;
		GetComponent<InputScript>().isPaused = false;
	}
}
