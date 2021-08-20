using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputScript : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject objectManager;

    //Camera variables
    public float cameraSpeed;
    public int Boundary; // distance from edge scrolling starts

    //screen borders
    private int theScreenWidth;
    private int theScreenHeight;

    void Start()
    {
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
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

        //escape key for pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GetComponent<PauseScript>().isPaused)
            {
                GetComponent<PauseScript>().PauseGame();
                GetComponent<PauseMenuScript>().PauseMenuVisual.SetActive(true);
            }
			else
			{
                GetComponent<PauseScript>().UnPauseGame();
                GetComponent<PauseMenuScript>().PauseMenuVisual.SetActive(false);
            }
        }

        //e key for marketplace toggle
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(GetComponent<UIScript>().marketplaceMenu.activeSelf == false)
            {
                GetComponent<UIScript>().marketplaceMenu.SetActive(true);
            }
            else
            {
                GetComponent<UIScript>().marketplaceMenu.SetActive(false);
            }
        }

        //checks if player can move camera
        if (GetComponent<CameraScript>().GetCanMove())
        {
            //if player is poressing W, A, S, or D then the camera moves depending on function call
            if (Input.GetKey("s"))
            {
                GetComponent<CameraScript>().MoveUp(Time.deltaTime * cameraSpeed);
            }
            if (Input.GetKey("w"))
            {
                GetComponent<CameraScript>().MoveDown(Time.deltaTime * cameraSpeed);
            }
            if (Input.GetKey("a"))
            {
                GetComponent<CameraScript>().MoveLeft(Time.deltaTime * cameraSpeed);
            }
            if (Input.GetKey("d"))
            {
                GetComponent<CameraScript>().MoveRight(Time.deltaTime * cameraSpeed);
            }

            //If mouse is near the screen edge then move.
            if (Input.mousePosition.x > theScreenWidth - Boundary)
            {
                GetComponent<CameraScript>().MoveRight(Time.deltaTime * cameraSpeed); // move on +X axis
            }
            if (Input.mousePosition.x < 0 + Boundary)
            {
                GetComponent<CameraScript>().MoveLeft(Time.deltaTime * cameraSpeed); // move on -X axis
            }
            if (Input.mousePosition.y > theScreenHeight - Boundary)
            {
                GetComponent<CameraScript>().MoveDown(Time.deltaTime * cameraSpeed); // move on +Z axis
            }
            if (Input.mousePosition.y < 0 + Boundary)
            {
                GetComponent<CameraScript>().MoveUp(Time.deltaTime * cameraSpeed); // move on -Z axis
            }

            ///Continously unpausing the game - will have to find a more efficient way for doing this
            ///
            if (Input.mousePosition.x > theScreenWidth || Input.mousePosition.x < 0 || Input.mousePosition.y > theScreenHeight || Input.mousePosition.y < 0) { Time.timeScale = 0; }
            else if (!GetComponent<PauseScript>().isPaused) { GetComponent<PauseScript>().UnPauseGame(); }
            ///
        }
    }

    //Finds what object is being selected
    void Select(Vector2 pos)
    {
        //casts a ray from camera to mouse position/tap
        Ray ray = Camera.main.ScreenPointToRay(pos);

        //checks to see if position of click/touch is over any UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Checks if the ray connects with an object/asset
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.TryGetComponent(out ClickableObject cB))
            {
                objectManager.GetComponent<GridScript>().SetSelectedTile(hit.collider.gameObject);

                gameManager.GetComponent<UIScript>().statMenu.SetActive(true);

                //gathger stats of object using grid id?
                //or hit.getcomponent?
            }
        }
    }
}
