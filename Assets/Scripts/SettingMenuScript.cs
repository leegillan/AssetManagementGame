using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingMenuScript : MonoBehaviour
{
	public TextMeshProUGUI volumeLevelDisplay;

	public void VolumeOff()
	{

		AudioListener.volume = 0.0f;
		volumeLevelDisplay.text = "0%";

	}
	public void VolumeLow()
	{

		AudioListener.volume = 0.33f;
		volumeLevelDisplay.text = "33%";

	}

	public void VolumeMed()
	{
		AudioListener.volume = 0.66f;
		volumeLevelDisplay.text = "66%";
	}

	public void VolumeHigh()
	{
		AudioListener.volume = 1.0f;
		volumeLevelDisplay.text = "100%";
	}
}

