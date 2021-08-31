using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecondhandMarketScript : MonoBehaviour
{
    bool secondhandSaleActive = false;
    //Type variable and getter
    public ObjectInfo objecty;

    public GameObject machinePage, secondhandPage;

    public TextMeshProUGUI tab1Cost, tab1Condition, tab2Cost, tab2Condition, tab3Cost, tab3Condition;

    ObjectInfo[] secondHandPressers;
    ObjectInfo[] secondHandQADesks;
    ObjectInfo[] secondHandMelters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SecondhandToggle() //Toggles between showing the new machines and the second hand machines
    {
		if (secondhandSaleActive) 
        {
            secondhandSaleActive = false;
            secondhandPage.SetActive(false);
            machinePage.SetActive(true);
        }
		else
		{
            secondhandSaleActive = true;
            secondhandPage.SetActive(true);
            machinePage.SetActive(false);
        }
    }

    public void SetToFalse() { secondhandSaleActive = false; } //ensures that new machines are prioritised

    public void RefreshSecondhandShop()
	{
        for (int i = 0; i < 3; i++)
		{
            secondHandMelters[i].SetIsMachine(true);
            secondHandMelters[i].SetMachineHealth(Random.Range(50, 90));
            secondHandMelters[i].constructionTime = 2.5f;
        }

        UpdateOutputs();//Call last for guaranteed newest info
	}

    void UpdateOutputs()
	{

	}
}
