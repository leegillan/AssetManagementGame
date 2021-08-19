using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectCountInfo
{
    public int count;
    public ObjectInfo.TYPE type;
}

public class ObjectInfoGatherer : MonoBehaviour
{
    [SerializeField] private ObjectCountInfo[] countInfo;

    [SerializeField] private int totalOperationCost;
    [SerializeField] private int totalMaintenanceCost;

    //getter
    public int GetTotalOperationCost() { return totalOperationCost; }
    public int GetTotalMaintenanceCost() { return totalMaintenanceCost; }

    //Update totals
    public void UpdateTotalOperationalCost(int oC) { totalOperationCost += oC; }
    public void UpdateTotalMaintenanceCost(int mC) { totalMaintenanceCost += mC; }

    public void AddToObjectList(ObjectInfo.TYPE objectType)
    {
        if (objectType == ObjectInfo.TYPE.NONE)
        {
            countInfo[0].count += 1;
        }
        else if (objectType == ObjectInfo.TYPE.MELTER)
        {
            countInfo[1].count += 1;
        }
        else if (objectType == ObjectInfo.TYPE.PRESSER)
        {
            countInfo[2].count += 1;
        }
    }
}
