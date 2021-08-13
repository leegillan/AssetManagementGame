using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    //Grid ID of object
    public int ID;

    //Object type
    public enum TYPE
    {
        NONE = 0,
        MELTER = 1,
        PRESSER = 2
    };

    //Declare object type
    public TYPE objectType;
    
    //costs of object
    public int maintenanceCost;
    public int operationalCost;

    //getters
    public TYPE GetObjectType() { return objectType; }
    public int GetObjectID() { return ID; }
    public int GetMaintenanceCost() { return maintenanceCost; }
    public int GetOperationalCost() { return operationalCost; }
    
    //setters
    public void SetObjectType(TYPE oT) { objectType = oT; }
    public void SetObjectID(int id) { ID = id; }
    public void SetMaintenanceCost(int mC) { maintenanceCost = mC; }
    public void SetOperationalCost(int oC) { operationalCost = oC; }
}
