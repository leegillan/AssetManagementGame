using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionProcess : MonoBehaviour
{
    public int totalDeliveredProduct = 10000;
    public int totalShippingProduct = 0;

    public int[] totalMachineInput = new int[10];
    public int[] totalMachineOutput = new int[10];

    public int[] totalMachineStockpile = new int[10];

    bool processStarted = false;

    public bool[] machineProcessOn = new bool[10];

    int deliveryAmount = 5000;
    int amountNeededForShipping = 1000;

    public void UpdateTotalMachineInput(int i, int value) { totalMachineInput[i] += value; }
    public void UpdateTotalMachineOutput(int i, int value) { totalMachineOutput[i] += value; }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ProgressProduction", 0.0f, 3.0f);
        processStarted = true;

        InvokeRepeating("DeliveryUpdate", 0.0f, 20.0f);
        InvokeRepeating("ShippingUpdate", 0.0f, 10.0f);
    }

    void ProgressProduction()
    {
        //Update Delivered Stock
        totalDeliveredProduct -= totalMachineInput[1];

        if (processStarted)
        {
            //Take from machines to be used throughout machines.
            totalMachineStockpile[1] += totalMachineOutput[1];      //add to stockpile the output of the machine
            totalMachineStockpile[1] -= totalMachineInput[2];       //take from machines stockpile

            if (machineProcessOn[0])
            {
                totalMachineStockpile[2] += totalMachineOutput[2];      //add to stockpile the output of the machine

                if (machineProcessOn[1])
                {
                    //Update Shipping stock
                    totalShippingProduct += totalMachineOutput[2];          //add last process output to shipping area stockpile

                    totalMachineStockpile[2] -= totalMachineStockpile[2];   //take away from the last stockpile
                }

                if (!machineProcessOn[1])
                {
                    machineProcessOn[1] = true;
                }
            }

            if (!machineProcessOn[0])
            {
                machineProcessOn[0] = true;
            }

            //figure out a way to make this better
            //i.e a way that determines how many sections there are in the current scene 
        }
    }

    void DeliveryUpdate()
    {
        if (totalDeliveredProduct <= deliveryAmount)                                //checks if the amount in storage can intake more delivery
        {
            totalDeliveredProduct += deliveryAmount;                                //adds delivery to storage

            GetComponent<Economy>().UpdateMoney((int)-(deliveryAmount * .75f));     //takes away cost of product per product
        }
    }

    void ShippingUpdate()
    {
        if(totalShippingProduct >= amountNeededForShipping)                                 //checks if the required amount for shipping is available in storage
        {
            totalShippingProduct -= amountNeededForShipping;                                //takes away from storage

            GetComponent<Economy>().UpdateMoney((int)(amountNeededForShipping * 1.50f));    //adds to money the shipped * amount per product
        }
    }
}
