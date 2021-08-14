using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceOptions : MonoBehaviour
{
    public GameObject objectManager;

    //Type variable and getter
    public ObjectInfo.TYPE type;
    public ObjectInfo.TYPE GetOptType() { return type; }
    
    //marketplace buy button function
    public void BuyAsset()
    {
        objectManager.GetComponent<GridScript>().CheckAvailablePositions(type);
    }
}
