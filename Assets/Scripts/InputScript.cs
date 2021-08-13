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
            GetComponent<PauseScript>().PauseGame();
            GetComponent<PauseMenuScript>().PauseMenuVisual.SetActive(true);
        }

        //checks if player can move camera
        if (canMove == true)
        {
            //if player is poressing W, A, S, or D then the camera moves depending on function call
            if (Input.GetKey("s") && Camera.main.transform.position.z <= 41 && Camera.main.transform.position.x <= 58)
            {
                MoveUp(Time.deltaTime * cameraSpeed);
            }
            if (Input.GetKey("w") && Camera.main.transform.position.z >= -15 && Camera.main.transform.position.x >= -16)
            {
                MoveDown(Time.deltaTime * cameraSpeed);
            }
            if (Input.GetKey("a") && Camera.main.transform.position.z >= -15 && Camera.main.transform.position.x <= 58)
            {
                MoveLeft(Time.deltaTime * cameraSpeed);
            }
            if (Input.GetKey("d") && Camera.main.transform.position.z <= 41 && Camera.main.transform.position.x >= -16)
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
            gameManager.GetComponent<Economy>().MinusMoney(10);

            Debug.Log("Raycast complete: " + hit.collider.name);
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
