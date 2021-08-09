using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
