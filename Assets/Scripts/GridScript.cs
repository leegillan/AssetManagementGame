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
    //Declare variables for grid dimensions and layout
    public float xStart, yStart;
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
    [SerializeField] List<DefinedGridLocations> definedLocations;
    public List<DefinedGridLocations> GetDefinedGridLocations() { return definedLocations; }

    private void Start()
    {
        CreateGrid();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CheckAvailablePositions(ObjectInfo.TYPE.PRESSER, GetDefinedGridLocations());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            CheckAvailablePositions(ObjectInfo.TYPE.MELTER, GetDefinedGridLocations());
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
                CreateSquare(new Vector3((xSpacing * (i % columnLength)), 0.0f, (zSpacing * (j % rowLength))), id);

                id++;
            }
        }
    }

    //creates individual tiles, setting ID and types
    void CreateSquare(Vector3 pos, int ID)
    {
        if(ID == 2)
        {
            gridSquares.Add((GameObject)Instantiate(Resources.Load("Prefabs/Melter"), pos, Quaternion.identity));
        }
        else if(ID == 20)
        {
            gridSquares.Add((GameObject)Instantiate(Resources.Load("Prefabs/Presser"), new Vector3(pos.x, pos.y + 0.35f, pos.z - 0.5f), Quaternion.Euler(0.0f, 180.0f, 0.0f)));
        }
        else if (ID == 30)
        {
            gridSquares.Add((GameObject)Instantiate(Resources.Load("Prefabs/Presser"), new Vector3(pos.x, pos.y + 0.35f, pos.z - 0.5f), Quaternion.Euler(0.0f, 180.0f, 0.0f)));
        }
        else
        {
            gridSquares.Add((GameObject)Instantiate(gridSquare, pos, Quaternion.identity));
        }

        gridSquares[ID].GetComponent<ObjectInfo>().SetObjectID(ID);

        GetComponent<ObjectInfoGatherer>().AddToObjectList(gridSquares[ID].GetComponent<ObjectInfo>().GetObjectType());
        GetComponent<ObjectInfoGatherer>().AddToTotalOperationalCost(gridSquares[ID].GetComponent<ObjectInfo>().GetOperationalCost());
        GetComponent<ObjectInfoGatherer>().AddToTotalMaintenanceCost(gridSquares[ID].GetComponent<ObjectInfo>().GetMaintenanceCost());
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
        RemoveGridTile(newAsset);

        //Add from list
        AddGridTile(newAsset);
    }

    public GameObject LoadAsset(ObjectInfo.TYPE type, Transform transform)//load asset based on type
    {
        //load prefab using name
        switch (type)
        {
            case ObjectInfo.TYPE.NONE:
                return (GameObject)Instantiate(Resources.Load("Prefabs/GridObjectTest"), transform.position, Quaternion.identity);

            case ObjectInfo.TYPE.MELTER:
                return (GameObject)Instantiate(Resources.Load("Prefabs/Melter"), transform.position, Quaternion.identity);

            case ObjectInfo.TYPE.PRESSER:
                return (GameObject)Instantiate(Resources.Load("Prefabs/Presser"), new Vector3(transform.position.x, transform.position.y + 0.35f, transform.position.z - 0.5f), Quaternion.Euler(0.0f, 180.0f, 0.0f));

            default:
                return null;
        }
    }

    //Checks availability by looping through the list of defined grid locations for each type of object
    public void CheckAvailablePositions(ObjectInfo.TYPE type, List<DefinedGridLocations> definedGridLocations)
    {
        for (int i = 0; i < definedGridLocations.Count; i++)
        {
            if(definedGridLocations[i].type == type)
            {
                for (int j = 0; j < definedGridLocations[i].spawned.Count; j++)
                {
                    if(definedGridLocations[i].spawned[j] == false)
                    {
                        ChangeAsset(definedGridLocations[i].gridLocationID[j], type);
                        definedGridLocations[i].spawned[j] = true;

                        return;
                    }
                }
            }
        }
    }
}
