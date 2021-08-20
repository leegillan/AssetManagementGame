using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Camera Movement Variables
    bool canMove = true; //TEMPORARY, WILL UPDATE DURING GAMEPLAY

    //Inputs for the main camera of the game
    //movement for camera.

    public void MoveUp(float v)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + v, Camera.main.transform.position.y, Camera.main.transform.position.z + v);
    }

    public void MoveDown(float v)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - v, Camera.main.transform.position.y, Camera.main.transform.position.z - v);
    }

    public void MoveRight(float v)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - v, Camera.main.transform.position.y, Camera.main.transform.position.z + v);
    }

    public void MoveLeft(float v)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + v, Camera.main.transform.position.y, Camera.main.transform.position.z - v);
    }

    //Getter/Setter for canMove
    public void SetCanMove(bool c) { canMove = c; }
    public bool GetCanMove() { return canMove; }
}
