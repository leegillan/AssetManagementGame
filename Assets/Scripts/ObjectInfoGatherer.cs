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
    
    public void AddToList(ObjectInfo.TYPE objectType)
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
