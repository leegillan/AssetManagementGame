using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    private GameObject gameManager;

    //Grid ID of object
    public int ID;

    //Object type
    public enum TYPE
    {
        NONE = 0,
        MELTER = 1,
        PRESSER = 2,
        QADESK = 3,
        STORAGEBOXES = 4
    };

    //Declare object type
    public TYPE objectType;

    //Objects dynamic variables that can be changed throughout gameplay
    public bool isMachine = false;              //determine if object is a machine or not

    public float machineHealth = 100.0f;        //modifiable value that determines the life of the machine
    public float depreciationSpeed = 10.0f;      //speed at which the health decreases
    private float depreciationCounter;

    public int productionOutput;                //output from machine which is modified by speed of machine. (e.g 5,000 /day?)
    public float productionSpeed;               //speed of the output - affects the health depreciation speed

    public int maintenanceCost;                 //the cost to increase the machines health either back to 100%? or per 10%?
    public int operationalCost;                 //the cost to run the machine (e.g 500 /day?)

    public float constructionTime = 5.0f;       //the time it takes for the machine to start producing output - gets called to start after player buys the machine

    //getters
    public TYPE GetObjectType() { return objectType; }
    public int GetObjectID() { return ID; }
    public bool GetIsMachine() { return isMachine; }
    public float GetMachineHealth() { return machineHealth; }
    public float GetDepreciationSpeed() { return depreciationSpeed; }
    public int GetProductionOutput() { return productionOutput; }
    public float GetProductionSpeed() { return productionSpeed; }
    public int GetMaintenanceCost() { return maintenanceCost; }
    public int GetOperationalCost() { return operationalCost; }
    
    //setters
    public void SetObjectType(TYPE oT) { objectType = oT; }
    public void SetObjectID(int id) { ID = id; }
    public void SetIsMachine(bool iM) { isMachine = iM; }
    public void SetMachineHealth(float mH) { machineHealth = mH; }
    public void SetDepreciationSpeed(float dS) { depreciationSpeed = dS; }
    public void SetProductionOutput(int pO) { productionOutput = pO; }
    public void SetProductionSpeed(float pS) { productionSpeed = pS; }
    public void SetMaintenanceCost(int mC) { maintenanceCost = mC; }
    public void SetOperationalCost(int oC) { operationalCost = oC; }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    private void Update()
    {
        if(isMachine)
        {
            depreciationCounter += Time.deltaTime;

            if(depreciationCounter >= depreciationSpeed)
            {
                machineHealth -= 1.0f;

                depreciationCounter = 0;
            }
        }

        if(machineHealth <= 0)
        {
            gameManager.GetComponent<ZoneDecider>().GetGridForZone(gameManager.GetComponent<ZoneDecider>().GetActiveZone()).SellTile(gameObject);
        }
    }
}
