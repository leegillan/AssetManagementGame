using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateCameraScript : MonoBehaviour
{
    public Transform[] views;
    public float transitionSpeed;
    Transform currentView;

    public GameObject gameManager;

    float orthoSize = 18;

    // Start is called before the first frame update
    void Start()
    {
        currentView = views[3];//set start view (default camera)
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || gameManager.GetComponent<ZoneDecider>().GetActiveZone() == ZoneDecider.ZONES.PRODUCTION)
        {
            currentView = views[0];

            if (Camera.main.orthographicSize != 6)
                orthoSize = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || gameManager.GetComponent<ZoneDecider>().GetActiveZone() == ZoneDecider.ZONES.QA)
        {
            currentView = views[1];

            if (Camera.main.orthographicSize != 6)
                orthoSize = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentView = views[2];

            if(Camera.main.orthographicSize != 6)
                orthoSize = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentView = views[3];

            //OVERVIEW CAMERA SIZE 18....
            if (Camera.main.orthographicSize != 18)
                orthoSize = 18;
        }

    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);//set position
       // transform.position = Vector3.Lerp(transform.position, currentView.position, 0.5f * transitionSpeed);//set position

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));//calculate rotation

        transform.eulerAngles = currentAngle;//set rotation

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, orthoSize, Time.deltaTime * transitionSpeed);
    }
}
