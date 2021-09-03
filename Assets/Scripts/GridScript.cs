using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefinedGridLocations
{
    public ObjectInfo.TYPE type;
    public List<int> gridLocationID;
    public List<bool> spawned;
}

public class GridScript : MonoBehaviour
{
    public GameObject gameManager;
    public ZoneDecider.ZONES zoneType;

    //Declare variables for grid dimensions and layout
    public float xStart, zStart;
    public int columnLength, rowLength;
    public float xSpacing, zSpacing;

    //initial tile to be placed on grid
    public GameObject gridSquare;

    //list of tiles on grid
    List<GameObject> gridSquares = new List<GameObject>();

    //getter
    public List<GameObject> GetGrid() { return gridSquares; }

    //removes tile from the grid list
    public void RemoveGridTile(GameObject ob) { gridSquares.Remove(ob); }

    //adds tile to the grid list
    public void AddGridTile(GameObject ob) { gridSquares.Add(ob); }

    //Defined grid location variables and getter
    [SerializeField] List<DefinedGridLocations> definedGridLocations;
    public List<DefinedGridLocations> GetDefinedGridLocations() { return definedGridLocations; }

    //the tile that the player has most recently clicked on
    [SerializeField]private GameObject selectedTile;

    //Selected Tile getter
    public GameObject GetSelectedTile() { return selectedTile; }

    //sets selected tile
    public void SetSelectedTile(GameObject s) { selectedTile = s; }

    public void SellSelectedTile()
    {
        if (selectedTile)
        {
            //minus the cost from the total costs that get shown in the quarterly menu
            GetComponent<ObjectInfoGatherer>().UpdateTotalOperationalCost(-selectedTile.GetComponent<ObjectInfo>().GetOperationalCost());
            GetComponent<ObjectInfoGatherer>().UpdateTotalMaintenanceCost(-selectedTile.GetComponent<ObjectInfo>().GetMaintenanceCost());
            //need to update list of objects aswell

            //gets rid of old asset
            UpdateAvailablePositions(selectedTile.GetComponent<ObjectInfo>().GetObjectType(), selectedTile.GetComponent<ObjectInfo>().GetObjectID());
        }
    }

    public void SellTile(GameObject Tile)
    {
        //minus the cost from the total costs that get shown in the quarterly menu
        GetComponent<ObjectInfoGatherer>().UpdateTotalOperationalCost(-Tile.GetComponent<ObjectInfo>().GetOperationalCost());
        GetComponent<ObjectInfoGatherer>().UpdateTotalMaintenanceCost(-Tile.GetComponent<ObjectInfo>().GetMaintenanceCost());
        //need to update list of objects aswell

        //gets rid of old asset
        UpdateAvailablePositions(Tile.GetComponent<ObjectInfo>().GetObjectType(), Tile.GetComponent<ObjectInfo>().GetObjectID());
    }

    private void Start()
    {
        CreateGrid();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateAvailablePositions(ObjectInfo.TYPE.PRESSER);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            UpdateAvailablePositions(ObjectInfo.TYPE.MELTER);
        }
    }

    //create grid with spacing
    public void CreateGrid()
    {
        //Declares variable
        int id = 0;

        //Creates minimum spacing if the default is below 1
        if (xSpacing < 1)
        {
            xSpacing = 1;
        }

        if (zSpacing < 1)
        {
            zSpacing = 1;
        }

        //creates and instantiates each tile, giving them a unique ID
        for (int i = 0; i < columnLength; i++)
        {
            for (int j = 0; j < rowLength; j++)
            {
                //Calls Create Square function to place a unique tile
                CreateSquare(new Vector3(xStart + (xSpacing * (i % columnLength)), 0.0f, zStart + (zSpacing * (j % rowLength))), id);

                id++;
            }
        }
    }

    //creates individual tiles, setting ID and types
    void CreateSquare(Vector3 pos, int ID)
    {
        if (zoneType == ZoneDecider.ZONES.PRODUCTION)
        {
            if (ID == 43 || ID == 33 || ID == 23)
            {
                gridSquares.Add((GameObject)Instantiate(Resources.Load("Prefabs/Presser"), pos, Quaternion.identity));
            }
            else if (ID == 20 || ID == 30)
            {
                gridSquares.Add((GameObject)Instantiate(Resources.Load("Prefabs/Melter"), pos, Quaternion.identity));
            }
            else if (ID == 21 || ID == 31 || ID == 41 || ID == 51 || ID == 54 || ID == 44 || ID == 34 || ID == 24)
            {
                gridSquares.Add((GameObject)Instantiate(Resources.Load("Prefabs/ConveyorBelt"), pos, Quaternion.Euler(0.0f, 90.0f, 0.0f)));
            }
            else if (ID == 52 || ID == 53)
            {
                gridSquares.Add((GameObject)Instantiate(Resources.Load("Prefabs/ConveyorBelt"), pos, Quaternion.identity));
            }
            else
            {
                gridSquares.Add((GameObject)Instantiate(gridSquare, pos, Quaternion.identity));
            }
        }
        else if (zoneType == ZoneDecider.ZONES.QA)
        {
            if (ID == 0 || ID == 1 || ID == 2)
            {
                gridSquares.Add((GameObject)Instantiate(Resources.Load("Prefabs/StorageBoxes"), pos, Quaternion.identity));
            }
            else if (ID == 5 || ID == 6 || ID == 7 || ID == 10 || ID == 11 || ID == 12)
            {
                gridSquares.Add((GameObject)Instantiate(Resources.Load("Prefabs/QADesk"), pos, Quaternion.identity));
            }
            else
            {
                gridSquares.Add((GameObject)Instantiate(gridSquare, pos, Quaternion.identity));
            }
        }
        else
        {
            gridSquares.Add((GameObject)Instantiate(gridSquare, pos, Quaternion.identity));
        }

        gridSquares[ID].GetComponent<ObjectInfo>().SetObjectID(ID);

        GetComponent<ObjectInfoGatherer>().AddToObjectList(gridSquares[ID].GetComponent<ObjectInfo>().GetObjectType());
        GetComponent<ObjectInfoGatherer>().UpdateTotalOperationalCost(gridSquares[ID].GetComponent<ObjectInfo>().GetOperationalCost());
        GetComponent<ObjectInfoGatherer>().UpdateTotalMaintenanceCost(gridSquares[ID].GetComponent<ObjectInfo>().GetMaintenanceCost());
    }

    //Gets grid tile from ID of tile
    public GameObject GetGridTile(int id)
    {
        //Decalre variables
        bool found = false;
        int count = 0;

        //looks through the list to find the one being used
        while (found == false)
        {
            if (gridSquares[count].GetComponent<ObjectInfo>().GetObjectID() == id)
            {
                found = true;
            }

            count += 1;
        }

        //returns the grid when found otherwise it returns nothing
        if (found)
        {
            return gridSquares[count - 1];
        }
        else
        {
            return null;
        }
    }

    //code to unload old and load new asset
    public void ChangeAsset(int id, ObjectInfo.TYPE type)
    {
        GameObject oldAsset = GetGridTile(id);  //get asset to be changed
        GameObject newAsset = LoadAsset(type, oldAsset.transform);  //load correct asset based on type

        //set objectID and level and fill
        newAsset.GetComponent<ObjectInfo>().SetObjectID(id);

        //Remove from list
        RemoveGridTile(oldAsset);

        //Destroy shape to be replaced
        GameObject.Destroy(oldAsset);

        //Add to list
        AddGridTile(newAsset);

        GetComponent<ObjectInfoGatherer>().AddToObjectList(newAsset.GetComponent<ObjectInfo>().GetObjectType());
        GetComponent<ObjectInfoGatherer>().UpdateTotalOperationalCost(newAsset.GetComponent<ObjectInfo>().GetOperationalCost());
        GetComponent<ObjectInfoGatherer>().UpdateTotalMaintenanceCost(newAsset.GetComponent<ObjectInfo>().GetMaintenanceCost());
    }

    public GameObject LoadAsset(ObjectInfo.TYPE type, Transform transform)//load asset based on type
    {
        //load prefab using name
        switch (type)
        {
            case ObjectInfo.TYPE.NONE:
                return (GameObject)Instantiate(Resources.Load("Prefabs/ProdGridObject"), transform.position, Quaternion.identity);

            case ObjectInfo.TYPE.MELTER:
                return (GameObject)Instantiate(Resources.Load("Prefabs/Melter"), transform.position, Quaternion.identity);

            case ObjectInfo.TYPE.PRESSER:
                return (GameObject)Instantiate(Resources.Load("Prefabs/Presser"), transform.position, Quaternion.identity);

            case ObjectInfo.TYPE.QADESK:
                return (GameObject)Instantiate(Resources.Load("Prefabs/QADesk"), transform.position, Quaternion.identity);

            case ObjectInfo.TYPE.STORAGEBOXES:
                return (GameObject)Instantiate(Resources.Load("Prefabs/StorageBoxes"), transform.position, Quaternion.identity);

            default:
                return null;
        }
    }

    //Checks availability by looping through the list of defined grid locations for each type of object
    public void UpdateAvailablePositions(ObjectInfo.TYPE type)
    {
        Data obData = GetComponent<ObjectData>().GetObjectData(type);

        for (int i = 0; i < definedGridLocations.Count; i++)
        {
            if(definedGridLocations[i].type == type)
            {
                for (int j = 0; j < definedGridLocations[i].spawned.Count; j++)
                {
                    if (definedGridLocations[i].spawned[j] == false)
                    {
                        //take money away for buying equipment
                        gameManager.GetComponent<Economy>().UpdateMoney(-obData.purchaseCost);

                        //changes old to new assets
                        StartCoroutine(WaitForConstructionTime(i, j, type, obData));

                        //sets boolean to true for further checks
                        definedGridLocations[i].spawned[j] = true;

                        return;
                    }
                }
            }
        }
    }
    
    //Checks for the object selected to be in the list to get changed back to the default grid object
    public void UpdateAvailablePositions(ObjectInfo.TYPE type, int gridID)
    {
        Data obData = GetComponent<ObjectData>().GetObjectData(type);

        for (int i = 0; i < definedGridLocations.Count; i++)
        {
            if(definedGridLocations[i].type == type)
            {
                for (int j = 0; j < definedGridLocations[i].spawned.Count; j++)
                {
                    if (definedGridLocations[i].spawned[j] == true && definedGridLocations[i].gridLocationID[j] == gridID)
                    {
                        //add money for selling equipment
                        gameManager.GetComponent<Economy>().UpdateMoney(obData.purchaseCost - 500);

                        //changes old to new assets
                        ChangeAsset(definedGridLocations[i].gridLocationID[j], ObjectInfo.TYPE.NONE);

                        //sets boolean to false for further checks
                        definedGridLocations[i].spawned[j] = false;

                        return;
                    }
                }
            }
        }
    }

    IEnumerator WaitForConstructionTime(int i, int j, ObjectInfo.TYPE type, Data obData)
    {
        //Wait for construction time in seconds
        yield return new WaitForSeconds(obData.constructionTime);

        //changes old to new assets
        ChangeAsset(definedGridLocations[i].gridLocationID[j], type);
    }
}
