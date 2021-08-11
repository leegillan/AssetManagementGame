using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    //Declare variables for grid dimensions and layout
    public float xStart, yStart;
    public int columnLength, rowLength;
    public float xSpacing, zSpacing;

    //initial tile to be placed on grid
    public GameObject gridSquare;
    public GameObject ObjectManager;

    //list of tiles on grid
    List<GameObject> gridSquares = new List<GameObject>();

    //getter
    public List<GameObject> GetGrid() { return gridSquares; }

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
        else
        {
            gridSquares.Add((GameObject)Instantiate(gridSquare, pos, Quaternion.identity));
        }

        ObjectManager.GetComponent<ObjectInfoGatherer>().AddToList(gridSquares[ID].GetComponent<ObjectInfo>().GetObjectType());
        ObjectManager.GetComponent<ObjectInfoGatherer>().AddToTotalOperationalCost(gridSquares[ID].GetComponent<ObjectInfo>().GetOperationalCost());
        ObjectManager.GetComponent<ObjectInfoGatherer>().AddToTotalMaintenanceCost(gridSquares[ID].GetComponent<ObjectInfo>().GetMaintenanceCost());
    }

    private void Start()
    {
        CreateGrid();
    }
}
