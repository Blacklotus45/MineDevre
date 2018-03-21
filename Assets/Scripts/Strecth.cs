using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strecth : MonoBehaviour {

	public Transform middle;
	private Transform anchor;
	Vector3 firstPosition;

	// Use this for initialization
	void Start () {
		//Anchor initialization
		foreach (Transform edgeTransform in transform.parent)
		{
			if (edgeTransform != this.transform)
			{
				anchor = edgeTransform;
				break;
			}
		}
	}
		

	void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition) ;

        transform.position = objPosition;
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
