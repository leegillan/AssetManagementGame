using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketplaceOptions : MonoBehaviour
{
    public GameObject objectManager;
    public GameObject gameManager;

    //Type variable and getter
    public ObjectInfo.TYPE type;
    public ObjectInfo.TYPE GetOptType() { return type; }
    
    //marketplace buy button function
    public void BuyAsset()
    {
        Data obData = objectManager.GetComponent<ObjectData>().GetObjectData(type);

        if (gameManager.GetComponent<Economy>().GetMoney() >= obData.purchaseCost)
        {
            gameManager.GetComponent<Economy>().UpdateMoney(-obData.purchaseCost);
            objectManager.GetComponent<GridScript>().UpdateAvailablePositions(type);
        }
    }
}
