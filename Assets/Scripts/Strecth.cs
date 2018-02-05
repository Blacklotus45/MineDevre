﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strecth : MonoBehaviour {

	public Transform middle;
	private Transform anchor;

	// Use this for initialization
	void Start () {
		foreach (Transform edgeTransform in transform.parent)
		{
			//Debug.Log("I'm " + gameObject.name +  " I find the " + transform.gameObject.name);
			if (edgeTransform != this.transform)
			{
				anchor = edgeTransform;
				break;
			}
		}
	}

	void OnMouseDrag()
    {
//		GetComponent<CircleCollider2D>().enabled = false;
    	
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition) ;

        transform.position = objPosition;

//        Debug.Log(objPosition + " Anchor " + anchor.position);

        //Quaternion.LookRotation(Vector3.forward,Vector3.up);
		float lengthOfWire = Vector3.Magnitude(objPosition - anchor.position);
        float rotation = Mathf.Atan((objPosition.y - anchor.position.y) / (objPosition.x - anchor.position.x)) / 2;

        middle.position = (transform.position + anchor.position) / 2;

		Vector3 scale = middle.localScale;
		scale.x = lengthOfWire - (1.836f * 0.3f);
        middle.localScale = scale;

        Quaternion newRot = middle.rotation;
        newRot.z = rotation;
        middle.rotation = newRot;

    }

    public void StrecthTo(Vector3 lastPos)
    {
		float lengthOfWire = Vector3.Magnitude(lastPos - anchor.position);
        float rotation = Mathf.Atan((lastPos.y - anchor.position.y) / (lastPos.x - anchor.position.x)) / 2;

		middle.position = (transform.position + anchor.position) / 2;

		Vector3 scale = middle.localScale;
		scale.x = lengthOfWire - (1.836f * 0.3f);
        middle.localScale = scale;

        Quaternion newRot = middle.rotation;
        newRot.z = rotation;
        middle.rotation = newRot;
    }
}
