using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject marketplaceMenu;
    public GameObject statMenu;

    public void SellAsset()
    {
        //objectManager.GetComponent<GridScript>().UpdateAvailablePositions(type);  //below line does this code
        GetComponent<ZoneDecider>().GetGridForZone(GetComponent<ZoneDecider>().GetActiveZone()).SellSelectedTile();
    }
}
