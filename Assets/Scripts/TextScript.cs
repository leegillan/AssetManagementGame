using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextScript : MonoBehaviour
{
    //Declares UI variables
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI fpsText;

    public float FPS;

    private void Update()
    {
        FPS = 1.0f / Time.deltaTime;

        //translates into strings to be displayed
        moneyText.text = GetComponent<Economy>().GetMoney().ToString();
        fpsText.text = ("FPS: " + FPS.ToString());
    }
}
