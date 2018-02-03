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
			}
			else 
			{
			  	parent.ConnectToRight (otherElement.gameObject);
			}
			otherElement.ConnectToParentNonRec(this);
			gameObject.GetComponent<SpriteRenderer>().color = Color.red;
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
			Snap(otherElement.gameObject.transform.position);
			gameObject.GetComponent<Strecth>().StrecthTo(otherElement.gameObject.transform.position);
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
		else
		{
			Debug.LogWarning("Found an edge circle without parent<CircuitElement>");
		}
	}

	private void Snap (Vector3 snapPosition)
	{
		Debug.Log("I'm " + gameObject.name + " of the " + transform.parent.name);
		this.transform.position = snapPosition;
	}

	void OnMouseUpAsButton ()
	{
		Debug.Log ("OnmouseUpasButton from: " + name + " of " + transform.parent.name);

		RaycastHit[] hit;
		Ray rei = Camera.main.ScreenPointToRay (Input.mousePosition);
		hit = Physics.RaycastAll (rei, 35f);
		if (hit.Length != 0)
		{
			for (int i = 0; i < hit.Length; i++)
			{
				if (hit [i].transform.gameObject != gameObject)
				{
					if (hit [i].transform.GetComponent<EdgeHandler> () == null)
					{
						hit [i].transform.GetComponent<Connector> ().ConnectElement (gameObject.GetComponentInParent<CircuitElement>());
					}
					else
					{
						hit [i].transform.GetComponent<EdgeHandler> ().ConnectToParent (this);
					}
					break;
				}
			}			
		}

	}

}
