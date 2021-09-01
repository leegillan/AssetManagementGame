using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecondhandMarketScript : MonoBehaviour
{
    bool secondhandSaleActive = false;
    //Type variable and getter
    public GameObject machinePage, secondhandPage, objectManager, secondHandPressy, secondHandQADesky, secondHandMelty;

    public TextMeshProUGUI secondhandToggleButton, tab1Cost, tab1Condition, tab2Cost, tab2Condition, tab3Cost, tab3Condition;//tab1 - Pressers, tab2 - QA desks, tab3 - Melters

    Data cost;

    

    GameObject[] secondHandPressers = new GameObject[3];
    GameObject[] secondHandQADesks = new GameObject[3];
    GameObject[] secondHandMelters = new GameObject[3];

    int pressNum, qaNum, meltNum;

    // Start is called before the first frame update
    void Start()
    {
        pressNum = 0;
        qaNum = 0;
        meltNum = 0;

        for (int i = 0; i < 3; i++)
		{
            secondHandPressers[i] = Instantiate(secondHandPressy);
            secondHandQADesks[i] = Instantiate(secondHandQADesky);
            secondHandMelters[i] = Instantiate(secondHandMelty);
        }

        RefreshSecondhandShop();
    }

    public void SecondhandToggle() //Toggles between showing the new machines and the second hand machines
    {
		if (secondhandSaleActive) 
        {
            secondhandSaleActive = false;
            secondhandPage.SetActive(false);
            machinePage.SetActive(true);
            secondhandToggleButton.SetText("Second hand");
        }
		else
		{
            secondhandSaleActive = true;
            secondhandPage.SetActive(true);
            machinePage.SetActive(false);
            secondhandToggleButton.SetText("New");
        }
    }

    public void SetToFalse() { secondhandSaleActive = false; } //ensures that new machines are prioritised

    public void cycleTab1()
	{
        if (pressNum < 2) { pressNum++; }
        else { pressNum = 0; }
        UpdateOutputs();
    }
    public void cycleTab2()
    {
        if (qaNum < 2) { qaNum++; }
        else { qaNum = 0; }
        UpdateOutputs();
    }
    public void cycleTab3()
    {
        if (meltNum < 2) { meltNum++; }
        else { meltNum = 0; }
        UpdateOutputs();
    }

    public void RefreshSecondhandShop()
	{
        for (int i = 0; i < secondHandPressers.Length; i++)
        {
            //Pressers
            secondHandPressers[i].GetComponent<ObjectInfo>().SetMachineHealth(Random.Range(50, 90));

            Debug.Log(secondHandPressers[i].GetComponent<ObjectInfo>().GetMachineHealth());
        }

        Debug.Log(secondHandPressers[0].GetComponent<ObjectInfo>().GetMachineHealth());
        Debug.Log(secondHandPressers[1].GetComponent<ObjectInfo>().GetMachineHealth());
        Debug.Log(secondHandPressers[2].GetComponent<ObjectInfo>().GetMachineHealth());

        for (int i = 0; i < secondHandQADesks.Length; i++)
        {
            //QA Desks
            secondHandQADesks[i].GetComponent<ObjectInfo>().SetMachineHealth(90);
        }

        for (int i = 0; i < secondHandMelters.Length; i++)
        {
            //Melters
            secondHandMelters[i].GetComponent<ObjectInfo>().SetMachineHealth(Random.Range(50, 90));
        }
    
       UpdateOutputs();//Call last for guaranteed newest info
	}

    void UpdateOutputs()
	{

        cost = objectManager.GetComponent<ObjectData>().GetObjectData(ObjectInfo.TYPE.PRESSER);//Get the base cost

        //Pressers
        tab1Cost.SetText("Cost: £" + (cost.purchaseCost * secondHandPressers[pressNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100);//Set appropriate discounted cost
        tab1Condition.SetText("Status: " + secondHandPressers[pressNum].GetComponent<ObjectInfo>().GetMachineHealth().ToString() + "%");//Output the health percentage

        cost = objectManager.GetComponent<ObjectData>().GetObjectData(ObjectInfo.TYPE.QADESK);

        //QA Desks
        tab2Cost.SetText("Cost: £" + (cost.purchaseCost * secondHandQADesks[qaNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100);
        tab2Condition.SetText("Status: " + secondHandQADesks[qaNum].GetComponent<ObjectInfo>().GetMachineHealth().ToString() + "%");

        cost = objectManager.GetComponent<ObjectData>().GetObjectData(ObjectInfo.TYPE.MELTER);

        //Melters
        tab3Cost.SetText("Cost: £" + (cost.purchaseCost * secondHandMelters[meltNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100);
        tab3Condition.SetText("Status: " + secondHandMelters[meltNum].GetComponent<ObjectInfo>().GetMachineHealth().ToString() + "%");
	}
}
