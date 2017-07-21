using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour {

    public GameObject target;
	public GameObject background;

    public GameObject[] wall = new GameObject[2];
    
	public float speed;

    private Vector3 camStartingPos;
    private Vector3 targetPos;
    private Vector3 leftWallPos;
    private Vector3 rightWallPos;
	private Vector3 bgPos;

    private Camera cam;
    
	private float camHeight;
    private float camWidth;
	private float originalOffset;
	private float camLeftEndPoint;
	private float camRightEndPoint;
	private float endToEndDistance;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        
		camStartingPos = transform.position;
        leftWallPos = wall[0].GetComponent<Transform>().position;
        rightWallPos = wall[1].GetComponent<Transform>().position;
		bgPos = background.GetComponent<Transform> ().position;

		camHeight = 2f * cam.orthographicSize;
		camWidth = camHeight * cam.aspect;

		camLeftEndPoint = leftWallPos.x + camWidth / 2f;
		camRightEndPoint = rightWallPos.x - camWidth / 2f;
		endToEndDistance = camRightEndPoint - camLeftEndPoint;

		originalOffset = bgPos.x - transform.position.x;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		targetPos = target.GetComponent<Transform>().position;

		camHeight = 2f * cam.orthographicSize; // these 2 lines are here because the user might change height and width from unity GUI
		camWidth = camHeight * cam.aspect;

		// Method to make camera follow target
		cameraFollowTarget ();
		// Method to move the bg
		moveBg();
	}

	void moveBg() {
		float camPosPercentage = (transform.position.x / endToEndDistance) * 2f;

		float newOffset = (originalOffset - originalOffset * camPosPercentage);
		Vector3 newPos = new Vector3 (transform.position.x + newOffset, bgPos.y, bgPos.z);

		background.transform.position = newPos;
	}

	void cameraFollowTarget() {
		if (!(targetPos.x >= camLeftEndPoint))
		{
			translateCamHorizontallyTo(camLeftEndPoint);
		}
		else if (!(targetPos.x <= camRightEndPoint))
		{
			translateCamHorizontallyTo(camRightEndPoint);
		}
		else
		{
			translateCamHorizontallyTo(targetPos.x);
		}
	}

    void translateCamHorizontallyTo(float x)
    {
        Vector3 newPos = new Vector3(x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, speed);
    }
}
