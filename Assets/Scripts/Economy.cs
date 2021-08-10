using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    //public variable to set in inspector
    public GameObject gameManager;

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

        //Yearly/Quarterly earnings added to budget
        gainTime += Time.deltaTime;

        if (gainTime > 20)
        {
            gainTime = 0;

            AddMoney(1000);
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
