﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strecth : MonoBehaviour {

	public Transform middle;
	private Transform anchor;
	Vector3 firstPosition;
	int count = 1;

	// Use this for initialization
	void Start () {
		count = 1;
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
		if (count == 1) {
			firstPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f);
			count = 0;
			Debug.Log ("Mouse FİRST : _>>>>>>>>>>>>>>>>>>>>>>>>>>>>:"+firstPosition);
		}
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition) ;

        transform.position = objPosition;
		Debug.Log ("Sınır x---->: "+Input.mousePosition.x);
		Debug.Log ("Sınır y---->: "+Input.mousePosition.y);
		StrecthTo (objPosition);
    }

    public void StrecthTo(Vector3 lastPos)
    {
		float lengthOfWire = Vector3.Magnitude(lastPos - anchor.position);
        float rotation = Mathf.Atan((lastPos.y - anchor.position.y) / (lastPos.x - anchor.position.x)) / 2;

		middle.position = (transform.position + anchor.position) / 2;

		Vector3 scale = middle.localScale;
		scale.x = lengthOfWire - (1.836f * 0.3f);
        middle.localScale = scale;

		Vector3 newRot = middle.eulerAngles;
		newRot.z = rotation * Mathf.Rad2Deg * 2;
		middle.eulerAngles = newRot;
    }
}
