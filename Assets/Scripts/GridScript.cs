using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    //Declare variables for grid dimensions and layout
    public float xStart, yStart;
    public int columnLength, rowLength;
    public int xSpacing, zSpacing;

    //initial tile to be placed on grid
    public GameObject gridSquare;

    //list of tiles on grid
    List<GameObject> gridSquares = new List<GameObject>();

    //create grid
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
        gridSquares.Add((GameObject)Instantiate(gridSquare, pos, Quaternion.identity));
    }

    private void Start()
    {
        CreateGrid();
    }
}
