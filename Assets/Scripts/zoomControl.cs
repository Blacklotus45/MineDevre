using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zoomControl : MonoBehaviour {

	public Button btnIn;
	public Button btnOut;


	bool zoomIn=false;
	bool zoomOut=false;

	public float zoomSize=20;

	public float panSpeed = -1;

	Vector3 bottomLeft;
	Vector3 topRight;

	float cameraMaxY;
	float cameraMinY;
	float cameraMaxX;
	float cameraMinX;


	// Use this for initialization
	void Start () {



		//set max camera bounds (assumes camera is max zoom and centered on Start)
		topRight = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight, -transform.position.z));
		bottomLeft = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0,0,-transform.position.z));
		cameraMaxX = topRight.x;
		cameraMaxY = topRight.y;
		cameraMinX = bottomLeft.x;
		cameraMinY = bottomLeft.y;


	}



	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButton(0))
		{
			float x = Input.GetAxis("Mouse X") * panSpeed;
			float y = Input.GetAxis("Mouse Y") * panSpeed;
			transform.Translate(x,y,0);
		}

		if(zoomIn){

			if(zoomSize > 8){

				zoomSize -= 1;

			}
			zoomIn = false;
		}

		if(zoomOut){

			if(zoomSize < 20){

				zoomSize += 1;

			}
			zoomOut = false;
		}

		GetComponent<Camera> ().orthographicSize = zoomSize;

		//check if camera is out-of-bounds, if so, move back in-bounds
		topRight = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(GetComponent<Camera>().pixelWidth,GetComponent<Camera>().pixelHeight, -transform.position.z));
		bottomLeft = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0,0,-transform.position.z));

		if(topRight.x > cameraMaxX)
		{
			transform.position = new Vector3(transform.position.x - (topRight.x - cameraMaxX), transform.position.y, transform.position.z);
		}

		if(topRight.y > cameraMaxY)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - (topRight.y - cameraMaxY), transform.position.z);
		}

		if(bottomLeft.x < cameraMinX)
		{
			transform.position = new Vector3(transform.position.x + (cameraMinX - bottomLeft.x), transform.position.y, transform.position.z);
		}

		if(bottomLeft.y < cameraMinY)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + (cameraMinY - bottomLeft.y), transform.position.z);
		}

	}

	public void zoomInF(){
		zoomIn = true;
	}

	public void zoomOutF(){
		zoomOut = true;
	}
}
