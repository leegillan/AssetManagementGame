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
    public TextMeshProUGUI operationalCostText;
    public TextMeshProUGUI maintenanceCostText;
    public TextMeshProUGUI ZoneText;

    public GameObject objectManager;

    public float FPS;

    private void Update()
    {
        FPS = 1.0f / Time.deltaTime;

        //translates into strings to be displayed
        moneyText.text = GetComponent<Economy>().GetMoney().ToString();
        fpsText.text = ("FPS: " + FPS.ToString());
    }

    public void UpdateQuarterlyText()
    {
        operationalCostText.text = "£ " + objectManager.GetComponent<ObjectInfoGatherer>().GetTotalOperationCost().ToString();
        maintenanceCostText.text = "£ " + objectManager.GetComponent<ObjectInfoGatherer>().GetTotalMaintenanceCost().ToString();
    }

    public void ChangeZoneText(string zone)
    {
        ZoneText.text = "Zone: " + zone;
    }
}
