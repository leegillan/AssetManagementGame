using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    //Declare object type
    public TYPE objectType;

    public int maintenanceCost;
    public int operationalCost;

    //Object type
    public enum TYPE
    {
        NONE = 0,
        MELTER = 1,
        PRESSER = 2
    };

    //getters
    public TYPE GetObjectType() { return objectType; }
    public int GetMaintenanceCost() { return maintenanceCost; }
    public int GetOperationalCost() { return operationalCost; }
    
    //setters
    public void SetObjectType(TYPE oT) { objectType = oT; }
    public void SetMaintenanceCost(int mC) { maintenanceCost = mC; }
    public void SetOperationalCost(int oC) { operationalCost = oC; }
}
