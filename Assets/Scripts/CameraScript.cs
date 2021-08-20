using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Camera Movement Variables
    bool canMove = true; //TEMPORARY, WILL UPDATE DURING GAMEPLAY
    public float cameraSpeed;

    //Getter/Setter for canMove
    public void SetCanMove(bool c) { canMove = c; }
    public bool GetCanMove() { return canMove; }

    //Inputs for the main camera of the game
    //movement for camera.
    public void MoveUp()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + cameraSpeed, Camera.main.transform.position.y, Camera.main.transform.position.z + cameraSpeed);
    }

    public void MoveDown()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - cameraSpeed, Camera.main.transform.position.y, Camera.main.transform.position.z - cameraSpeed);
    }

    public void MoveRight()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - cameraSpeed, Camera.main.transform.position.y, Camera.main.transform.position.z + cameraSpeed);
    }

    public void MoveLeft()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + cameraSpeed, Camera.main.transform.position.y, Camera.main.transform.position.z - cameraSpeed);
    }
}
