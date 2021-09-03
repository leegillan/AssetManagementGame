using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateCameraScript : MonoBehaviour
{
	public Transform[] views;
	public float transitionSpeed;
	Transform currentView;
	Transform transitionPoint;
	bool halfwayThere;
	public bool isTransitioning;
	bool cameraIsJumping;

	public GameObject gameManager;

	float orthoSize = 9.5f;

	// Start is called before the first frame update
	void Start()
	{
		currentView = views[1];//set start view (default camera)
		halfwayThere = true;
		isTransitioning = false;
		cameraIsJumping = false;
		transitionPoint = views[0];//views[0] is far from the others to avoid issues
	}

	// Update is called once per frame
	void Update()
	{
		if (Mathf.Abs(transform.position.y - transitionPoint.position.y) < 1 && !halfwayThere)
		{
			if (currentView == views[4]) { orthoSize = 9.5f; }//Set ortho size for overview
			else { orthoSize = 9.5f; }//set ortho size for others

			Camera.main.
			transform.position = new Vector3(currentView.position.x, currentView.position.y + 40, currentView.position.z);
			transform.rotation = new Quaternion(currentView.rotation.x, currentView.rotation.y/* + 30 ADD THIS TO HAVE A NICE SPIN UPON APPEARING ABOVE THE TARGET VIEWPOINT*/, currentView.rotation.z, currentView.rotation.w);
			
			StartCoroutine("CameraSnapper");//Stops the LERP for 0.1 seconds to allow the camera to snap to new location
			
			halfwayThere = true;
		}

		if (Mathf.Abs(transform.position.y - currentView.position.y) < 5 && halfwayThere) { isTransitioning = false; } 

		Debug.Log(currentView);
	}

	void LateUpdate()
	{
		Transform targetTransform;

		if (!halfwayThere) { targetTransform = transitionPoint; }
		else { targetTransform = currentView; }

		transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * transitionSpeed);//set position

		Vector3 currentAngle = new Vector3(
			Mathf.LerpAngle(transform.rotation.eulerAngles.x, targetTransform.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
			Mathf.LerpAngle(transform.rotation.eulerAngles.y, targetTransform.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
			Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetTransform.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));//calculate rotation

		transform.eulerAngles = currentAngle;//set rotation

		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, orthoSize, Time.deltaTime * transitionSpeed);//smoothly change the camera's ortho size
	}

	public void SetView(int viewNum)
	{
		if (!cameraIsJumping) //Allows LERP to be stopped
		{
			if ((viewNum < views.Length) && views[viewNum] != currentView)//only work if the view is valid
			{
				isTransitioning = true;

				transitionPoint.position = new Vector3(currentView.position.x, currentView.position.y + 40, currentView.position.z);
				halfwayThere = false;

				currentView = views[viewNum];
			}
			else { Debug.Log("Failed"); }
		}
	}

	IEnumerator CameraSnapper()//disables LERP for 0.1 seconds
	{
		cameraIsJumping = true;
		yield return new WaitForSeconds(.1f);
		cameraIsJumping = false;
	}
}
