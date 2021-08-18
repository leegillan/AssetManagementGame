using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputScript : MonoBehaviour
{
    public GameObject gameManager;

    //Camera variables
    public float cameraSpeed;
    public int Boundary; // distance from edge scrolling starts

    //Camera Movement Variables
    bool canMove = true; //TEMPORARY, WILL UPDATE DURING GAMEPLAY
    public bool isPaused = false;


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
            if (!isPaused)
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

        //escape key for pause menu
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
        if (canMove == true)
        {
            //if player is poressing W, A, S, or D then the camera moves depending on function call
            if (Input.GetKey("s"))
            {
                MoveUp(Time.deltaTime * cameraSpeed);
            }
            if (Input.GetKey("w"))
            {
                MoveDown(Time.deltaTime * cameraSpeed);
            }
            if (Input.GetKey("a"))
            {
                MoveLeft(Time.deltaTime * cameraSpeed);
            }
            if (Input.GetKey("d"))
            {
                MoveRight(Time.deltaTime * cameraSpeed);
            }

            //If mouse is near the screen edge then move.
            if (Input.mousePosition.x > theScreenWidth - Boundary)
            {
                MoveRight(Time.deltaTime * cameraSpeed); // move on +X axis
            }
            if (Input.mousePosition.x < 0 + Boundary)
            {
                MoveLeft(Time.deltaTime * cameraSpeed); // move on -X axis
            }
            if (Input.mousePosition.y > theScreenHeight - Boundary)
            {
                MoveDown(Time.deltaTime * cameraSpeed); // move on +Z axis
            }
            if (Input.mousePosition.y < 0 + Boundary)
            {
                MoveUp(Time.deltaTime * cameraSpeed); // move on -Z axis
            }

            ///Continously unpausing the game - will have to find a more efficient way for doing this
            ///
            if (Input.mousePosition.x > theScreenWidth || Input.mousePosition.x < 0 || Input.mousePosition.y > theScreenHeight || Input.mousePosition.y < 0) { Time.timeScale = 0; }
            else if(!isPaused){ GetComponent<PauseScript>().UnPauseGame(); }
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
                gameManager.GetComponent<UIScript>().statMenu.SetActive(true);


                //gathger stats of object using grid id?
                //or hit.getcomponent?
            }
        }
    }

    //Inputs for the main camera of the game
    //movement for camera.

    void MoveUp(float v)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + v, Camera.main.transform.position.y, Camera.main.transform.position.z + v);
    }

    void MoveDown(float v)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - v, Camera.main.transform.position.y, Camera.main.transform.position.z - v);
    }

    void MoveRight(float v)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - v, Camera.main.transform.position.y, Camera.main.transform.position.z + v);
    }

    void MoveLeft(float v)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + v, Camera.main.transform.position.y, Camera.main.transform.position.z - v);
    }

    //Getter/Setter for canMove
    public void SetCanMove(bool c) { canMove = c; }
    public bool GetCanMove() { return canMove; }
}
