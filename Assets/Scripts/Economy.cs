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

    // Start is called before the first frame update
    void Start()
    {
        money = 10000;
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
