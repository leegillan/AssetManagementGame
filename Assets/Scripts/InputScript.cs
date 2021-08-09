using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputScript : MonoBehaviour
{
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //checks if mouse button has been pressed
        if (Input.GetMouseButtonDown(0))
        {
            //calls select function to create a ray from camera to pointer location
            Select(Input.mousePosition);
        }
    }

    //Finds what object is being selected
    void Select(Vector2 pos)
    {
        //Declare variables
        RaycastHit hit;

        //casts a ray from camera to mouse position/tap
        Ray ray = Camera.main.ScreenPointToRay(pos);

        //checks to see if position of click/touch is over any UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Checks if the ray connects with an object/asset
        if (Physics.Raycast(ray, out hit))
        {
            ////checks what type of tile is selected
            //if (hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.EMPTY)
            //{
            //    gameManager.GetComponent<MarketplaceSpawner>().SpawnMenu();
            //    selecting = false;
            //}

            gameManager.GetComponent<Economy>().MinusMoney(10);

            Debug.Log("Raycast complete: " + hit.collider.name);
        }
    }
}
