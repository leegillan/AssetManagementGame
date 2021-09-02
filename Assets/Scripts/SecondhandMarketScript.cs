using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecondhandMarketScript : MonoBehaviour
{
    bool secondhandSaleActive = false;
    //Type variable and getter
    public GameObject machinePage, secondhandPage, objectManager, gameManager,secondHandPressy, secondHandQADesky, secondHandMelty;

    public TextMeshProUGUI secondhandToggleButton, tab1Cost, tab1Condition, tab2Cost, tab2Condition, tab3Cost, tab3Condition;//tab1 - Pressers, tab2 - QA desks, tab3 - Melters

    Data cost;

    bool[] isPresserSold = new bool[3];
    bool[] isQASold = new bool[3];
    bool[] isMelterSold = new bool[3];

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
            //isPresserSold[i] = false;
            //isQASold[i] = false;
            //isMelterSold[i] = false;
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

    public void cycleTab1()//Switch between the available options
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
            isPresserSold[i] = false;
        }

        for (int i = 0; i < secondHandQADesks.Length; i++)
        {
            //QA Desks
            secondHandQADesks[i].GetComponent<ObjectInfo>().SetMachineHealth(90);
            isQASold[i] = false;
        }

        for (int i = 0; i < secondHandMelters.Length; i++)
        {
            //Melters
            secondHandMelters[i].GetComponent<ObjectInfo>().SetMachineHealth(Random.Range(50, 90));
            isMelterSold[i] = false;
        }
    
       UpdateOutputs();//Call last for guaranteed newest info
	}

    void UpdateOutputs()
	{

        cost = objectManager.GetComponent<ObjectData>().GetObjectData(ObjectInfo.TYPE.PRESSER);//Get the base cost

        //Pressers
        if (!isPresserSold[pressNum])
        {
            tab1Cost.SetText("Cost: £" + (cost.purchaseCost * secondHandPressers[pressNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100);//Set appropriate discounted cost
            tab1Condition.SetText("Status: " + secondHandPressers[pressNum].GetComponent<ObjectInfo>().GetMachineHealth().ToString() + "%");//Output the health percentage
        }
		else
		{
            tab1Cost.SetText("Cost: SOLD");
            tab1Condition.SetText("Status: SOLD");
        }

        cost = objectManager.GetComponent<ObjectData>().GetObjectData(ObjectInfo.TYPE.QADESK);

        //QA Desks
        if (!isQASold[qaNum])
        {
            tab2Cost.SetText("Cost: £" + (cost.purchaseCost * secondHandQADesks[qaNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100);
            tab2Condition.SetText("Status: " + secondHandQADesks[qaNum].GetComponent<ObjectInfo>().GetMachineHealth().ToString() + "%");
        }
		else
		{
            tab2Cost.SetText("Cost: SOLD");
            tab2Condition.SetText("Status: SOLD");
        }

        cost = objectManager.GetComponent<ObjectData>().GetObjectData(ObjectInfo.TYPE.MELTER);

        //Melters
        if (!isMelterSold[meltNum])
        {
            tab3Cost.SetText("Cost: £" + (cost.purchaseCost * secondHandMelters[meltNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100);
            tab3Condition.SetText("Status: " + secondHandMelters[meltNum].GetComponent<ObjectInfo>().GetMachineHealth().ToString() + "%");
        }
		else
		{
            tab3Cost.SetText("Cost: SOLD");
            tab3Condition.SetText("Status: SOLD");
        }
	}

    public void BuyPresser()
	{
        cost = objectManager.GetComponent<ObjectData>().GetObjectData(ObjectInfo.TYPE.PRESSER);//Get the base cost
        float floatRefund = cost.purchaseCost - (cost.purchaseCost * secondHandPressers[pressNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100;
        int moneyToRefund = (int)floatRefund;

        if ((gameManager.GetComponent<Economy>().GetMoney() >= (cost.purchaseCost * secondHandPressers[pressNum].GetComponent<ObjectInfo>().GetMachineHealth()) /100) && !isPresserSold[pressNum])
		{
            gameManager.GetComponent<ZoneDecider>().GetGridForZone(gameManager.GetComponent<ZoneDecider>().GetActiveZone()).UpdateAvailablePositions(ObjectInfo.TYPE.PRESSER);
            gameManager.GetComponent<Economy>().UpdateMoney(moneyToRefund);
            isPresserSold[pressNum] = true;
            UpdateOutputs();
        }
		else
		{
            //Not enough Money
		}
    }
    public void BuyQA()
    {
        cost = objectManager.GetComponent<ObjectData>().GetObjectData(ObjectInfo.TYPE.QADESK);
        float floatRefund = cost.purchaseCost - (cost.purchaseCost * secondHandQADesks[qaNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100;
        int moneyToRefund = (int)floatRefund;

        if ((gameManager.GetComponent<Economy>().GetMoney() >= (cost.purchaseCost * secondHandQADesks[qaNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100) && !isQASold[qaNum])
        {
            gameManager.GetComponent<ZoneDecider>().GetGridForZone(gameManager.GetComponent<ZoneDecider>().GetActiveZone()).UpdateAvailablePositions(ObjectInfo.TYPE.QADESK);
            gameManager.GetComponent<Economy>().UpdateMoney(moneyToRefund);
            isQASold[qaNum] = true;
            UpdateOutputs();
        }
        else
        {
            //Not enough Money
        }
    }
    public void BuyMelter()
    {
        cost = objectManager.GetComponent<ObjectData>().GetObjectData(ObjectInfo.TYPE.MELTER);
        float floatRefund = cost.purchaseCost - (cost.purchaseCost * secondHandMelters[meltNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100;
        int moneyToRefund = (int)floatRefund;

        if ((gameManager.GetComponent<Economy>().GetMoney() >= (cost.purchaseCost * secondHandMelters[meltNum].GetComponent<ObjectInfo>().GetMachineHealth()) / 100) && !isMelterSold[meltNum])
        {
            gameManager.GetComponent<ZoneDecider>().GetGridForZone(gameManager.GetComponent<ZoneDecider>().GetActiveZone()).UpdateAvailablePositions(ObjectInfo.TYPE.MELTER);
            gameManager.GetComponent<Economy>().UpdateMoney(moneyToRefund);
            isMelterSold[meltNum] = true;
            UpdateOutputs();
        }
        else
        {
            //Not enough Money
        }
    }
}