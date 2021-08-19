using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//purchase cost and each level cost
public class Data
{
    public int purchaseCost;
    public int level2Cost;
    public int level3Cost;
}

public class ObjectData : MonoBehaviour
{
    Data data = new Data();

    public Data GetObjectData(ObjectInfo.TYPE type) //ret
    {
        //case statement to get data
        switch (type)
        {
            case ObjectInfo.TYPE.NONE:
                data.purchaseCost = 0;
                data.level2Cost = 0;
                data.level3Cost = 0;
                break;

            case ObjectInfo.TYPE.MELTER:
                data.purchaseCost = 1000;
                data.level2Cost = 2000;
                data.level3Cost = 5000;
                break;

            case ObjectInfo.TYPE.PRESSER:
                data.purchaseCost = 500;
                data.level2Cost = 1000;
                data.level3Cost = 3000;
                break;

            default:
                break;
        }

        return data;
    }
}