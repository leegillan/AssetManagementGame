using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    //public variable to set in inspector
    public GameObject gameManager;
    public GameObject objectManager;

    public GameObject quarterlyMenu;

    //Declare money variable
    public int money;

    //getter and setter
    public int GetMoney() { return money; }
    public void SetMoney(int mon) { money = mon; }

    float spendTime;
    bool quarterDone;

    // Start is called before the first frame update
    void Start()
    {
        money = 5000;
    }

    private void Update()
    {
        //Expenditure and Operation/Maintenance costs getting taken away from budget
        spendTime += Time.deltaTime;

        if (spendTime > 5)
        {
            spendTime = 0;

            UpdateMoney(-100);
        }

        if (!quarterDone)
        {
            int i = (int)GetComponent<CalendarScript>().getMonth();

            //Yearly/Quarterly earnings added/taken to/from budget
            if ((GetComponent<CalendarScript>().getDate() == 1) && ((i - 1) % 3 == 0))
            {
                quarterDone = true;

                //Edit budget amount
                int opCost = objectManager.GetComponent<ObjectInfoGatherer>().GetTotalOperationCost();

                UpdateMoney(-opCost);

                Debug.Log("Operational Cost for the quarter: " + opCost);

                UpdateMoney(1000);

                Debug.Log("Profit for the quarter: " + 1000);

                //pauses any time.deltaTime related issues in game
                quarterlyMenu.SetActive(true);
                GetComponent<TextScript>().UpdateQuarterlyText();
                GetComponent<SecondhandMarketScript>().RefreshSecondhandShop();

                GetComponent<PauseScript>().PauseGame();
            }
        }

        if (GetComponent<CalendarScript>().getDate() == 2) { quarterDone = false; }
    }

    //Add money
    public void UpdateMoney(int m)
    {
        //Update money value with passed value +/-
        money +=  m;
    }
}