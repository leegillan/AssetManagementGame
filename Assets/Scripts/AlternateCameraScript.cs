using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateCameraScript : MonoBehaviour
{
    public Transform[] views;
    public float transitionSpeed;
	bool halfwayThere;
    Transform currentView;
	Transform transitionPoint;

    public GameObject gameManager;

    float orthoSize = 18;

    // Start is called before the first frame update
    void Start()
    {
        currentView = views[3];//set start view (default camera)
		halfwayThere = true;
		transitionPoint = views[0];
	}

    // Update is called once per frame
    void Update()
    {
		//if (Input.GetKeyDown(KeyCode.Alpha1) || gameManager.GetComponent<ZoneDecider>().GetActiveZone() == ZoneDecider.ZONES.PRODUCTION)
		//{
		//    currentView = views[0];

		//    if (Camera.main.orthographicSize != 6)
		//        orthoSize = 6;
		//}

		//if (Input.GetKeyDown(KeyCode.Alpha2) || gameManager.GetComponent<ZoneDecider>().GetActiveZone() == ZoneDecider.ZONES.QA)
		//{
		//    currentView = views[1];

		//    if (Camera.main.orthographicSize != 6)
		//        orthoSize = 6;
		//}

		//if (Input.GetKeyDown(KeyCode.Alpha3))
		//{
		//    currentView = views[2];

		//    if(Camera.main.orthographicSize != 6)
		//        orthoSize = 6;
		//}

		//if (Input.GetKeyDown(KeyCode.Alpha4))
		//{
		//    currentView = views[3];

		//    //OVERVIEW CAMERA SIZE 18....
		//    if (Camera.main.orthographicSize != 18)
		//        orthoSize = 18;
		//}

		if ((Input.GetKeyDown(KeyCode.Alpha1) || gameManager.GetComponent<ZoneDecider>().GetActiveZone() == ZoneDecider.ZONES.PRODUCTION) && currentView != views[0])
		{
			//if (Camera.main.orthographicSize != 6)
			//	orthoSize = 6;

			transitionPoint.position = new Vector3(currentView.position.x, currentView.position.y + 15, currentView.position.z);
			halfwayThere = false;

			currentView = views[0];
		}

		if ((Input.GetKeyDown(KeyCode.Alpha2) || gameManager.GetComponent<ZoneDecider>().GetActiveZone() == ZoneDecider.ZONES.QA) && currentView != views[1])
		{
			transitionPoint.position = new Vector3(currentView.position.x, currentView.position.y + 15, currentView.position.z);
			halfwayThere = false;
			
			currentView = views[1];

			//if (Camera.main.orthographicSize != 6)
			//	orthoSize = 6;
		}

		if (Input.GetKeyDown(KeyCode.Alpha3) && currentView != views[2])
		{
			//if (Camera.main.orthographicSize != 6)
			//	orthoSize = 6;

			transitionPoint.position = new Vector3(currentView.position.x, currentView.position.y + 15, currentView.position.z);
			halfwayThere = false;

			currentView = views[2];
		}

		if (Input.GetKeyDown(KeyCode.Alpha4) && currentView != views[3])
		{
			transitionPoint.position = new Vector3(currentView.position.x, currentView.position.y - 15, currentView.position.z);
			halfwayThere = false;
			
			currentView = views[3];	
			
			////OVERVIEW CAMERA SIZE 18....
			//if (Camera.main.orthographicSize != 18)
			//	orthoSize = 18;
		}


		if (Mathf.Abs(transform.position.y - transitionPoint.position.y) < 1 && !halfwayThere)
		{
			if(currentView == views[3]) 
			{ 
				transform.position = new Vector3(transform.position.x, transform.position.y + 15, transform.position.z);
				orthoSize = 18;
			}
			else 
			{
				transform.position = new Vector3(transform.position.x, transform.position.y - 15, transform.position.z);
				orthoSize = 6;
			}

			halfwayThere = true;
		}

		Debug.Log(currentView);
	}

    void LateUpdate()
    {
		Transform targetTransform;

       if (!halfwayThere) { targetTransform = transitionPoint; }
		else { targetTransform = currentView; }

		transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * transitionSpeed);//set position
       // transform.position = Vector3.Lerp(transform.position, currentView.position, 0.5f * transitionSpeed);//set position

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(transform.rotation.eulerAngles.x, targetTransform.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, targetTransform.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetTransform.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));//calculate rotation

        transform.eulerAngles = currentAngle;//set rotation

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, orthoSize, Time.deltaTime * transitionSpeed);
    }
}
