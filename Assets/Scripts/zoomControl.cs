using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomControl : MonoBehaviour {

	public float zoomSize=20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetAxis("Mouse ScrollWheel") > 0){

			if(zoomSize > 8){

				zoomSize -= 1;

			}

		}

		if(Input.GetAxis("Mouse ScrollWheel") < 0){

			if(zoomSize < 20){

				zoomSize += 1;

			}

		}

		GetComponent<Camera> ().orthographicSize = zoomSize;

	}
}
