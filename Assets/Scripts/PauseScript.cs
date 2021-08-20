using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
	public bool isPaused;
	public void PauseGame()
	{
		Time.timeScale = 0;
		isPaused = true;
	}

	public void UnPauseGame()
	{
		Time.timeScale = 1;
		isPaused = false;
	}
}
