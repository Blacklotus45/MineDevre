using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeHandler : MonoBehaviour {

	public bool LeftEdge;

	public void ConnectToParent (EdgeHandler otherElement)
	{
		CircuitElement parent = gameObject.GetComponentInParent<CircuitElement> ();
		if (parent != null)
		{
			if (LeftEdge)
			{
				parent.ConnectToLeft (otherElement.gameObject);
				otherElement.ConnectToParentNonRec(this);
			}
			else 
			{
			  	parent.ConnectToRight (otherElement.gameObject);
				otherElement.ConnectToParentNonRec(this);
			}
		}
		else
		{
			Debug.LogWarning("Found an edge circle without parent<CircuitElement>");
		}
	}

	public void ConnectToParentNonRec (EdgeHandler otherElement)
	{
		CircuitElement parent = gameObject.GetComponentInParent<CircuitElement> ();
		if (parent != null)
		{
			if (LeftEdge)
			{
				parent.ConnectToLeft (otherElement.gameObject);
			}
			else 
			{
			  parent.ConnectToRight (otherElement.gameObject);
			}
		}
		else
		{
			Debug.LogWarning("Found an edge circle without parent<CircuitElement>");
		}
	}

//	void Update ()
//	{
//		Debug.DrawRay(Camera.main.transform.position, transform.position + (Vector3.forward*20f), Color.red, 0.33f);
//		Debug.Log("Camera Location "+Camera.main.transform.position+ "\nObjLocation"+ transform.position);
//	}

	void OnMouseUpAsButton()
	{
		Debug.Log("OnmouseUpasButton from: " + name);

//		RaycastHit[] hit;
//		hit = Physics.RaycastAll(Camera.main.transform.position, transform.position + (Vector3.forward*20f), 100f);
//		for (int i = 0; i < hit.Length; i++)
//		{
//			Debug.Log("Number of hits " + hit.Length);
//			if (hit[i].transform.gameObject != gameObject)
//			{
//				Debug.Log("I hit to " + hit[i].transform.gameObject.ToString());
//
//				break;
//			}
//		}

		RaycastHit[] hit;
		Ray rei = Camera.main.ScreenPointToRay(Input.mousePosition);
		hit = Physics.RaycastAll(rei, 35f);
//		Debug.DrawRay(rei.origin, Vector3.forward*35f, Color.red, 3f);
		if (hit.Length != 0)
		{
			for (int i = 0; i < hit.Length; i++)
			{
				if (hit[i].transform.gameObject != gameObject)
				{
					hit[i].transform.GetComponent<EdgeHandler>().ConnectToParent(this);
					break;
				}
			}			
		}

//		GetComponent<CircleCollider2D>().enabled = true;
	}

}
