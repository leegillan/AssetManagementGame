using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDecider : MonoBehaviour
{
    public GameObject objectManager;

    public enum ZONES
    {
        OVERVIEW = 0,
        PRODUCTION = 1,
        QA = 2,
        STORAGE = 3
    }

    //sets and gets activeZone
    private ZONES activeZone;
    public ZONES GetActiveZone() { return activeZone; }
    public void SetActiveZone(ZONES zone) { activeZone = zone; }

    // create a Dictionary with a key of type ZONE and a value of type gridscript
    public Dictionary<ZONES, GridScript> allGridScripts = new Dictionary<ZONES, GridScript>();

    // Start is called before the first frame update
    void Start()
    {
        activeZone = ZONES.PRODUCTION;  //start at production zone for order of GridScript components in objectManager

        // Get all the GridScript on this GameObject
        GridScript[] gridScripts = objectManager.GetComponents<GridScript>();

        // Loop through all GridScripts and add an entry into the Dictionary
        // which allows us to store each GridScript for each unique Zone
        foreach (GridScript gridScript in gridScripts)
        {
            switch(activeZone)
            {
                case ZONES.PRODUCTION:
                    SetGridForZone(gridScript, activeZone);
                    activeZone = ZONES.QA;
                    break;

                case ZONES.QA:
                    SetGridForZone(gridScript, activeZone);
                    break;

                default:
                    break;
            }
        }

        activeZone = ZONES.OVERVIEW;
    }

    //get a gridscript for a certain zone
    public GridScript GetGridForZone(ZONES gridZone)
    {
        return allGridScripts[gridZone];
    }

    //set a gridScript for a certain zone
    public void SetGridForZone(GridScript gridScript, ZONES zone)
    {
        allGridScripts.Add(zone, gridScript);
    }
}
