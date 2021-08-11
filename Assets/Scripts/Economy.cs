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
    float gainTime;

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

            MinusMoney(100);
        }

        //Yearly/Quarterly earnings added/taken to/from budget
        gainTime += Time.deltaTime;

        if (gainTime > 10)
        {
            gainTime = 0;

                //Edit budget amount
            int opCost = objectManager.GetComponent<ObjectInfoGatherer>().GetTotalOperationCost();

            MinusMoney(opCost);

            Debug.Log("Operational Cost for the quarter: " + opCost);

            AddMoney(1000);

            Debug.Log("Profit for the quarter: " + 1000);

            //pauses any time.deltaTime related issues in game
            quarterlyMenu.SetActive(true);
            GetComponent<TextScript>().UpdateQuarterlyText();

            Time.timeScale = 0;
        }
    }

    //Add money
    public void AddMoney(int m)
    {
        //add money value
        money = money + m;
    }

    //takeaway money
    public void MinusMoney(int m)
    {
        //add money value
        money = money - m;
    }
}