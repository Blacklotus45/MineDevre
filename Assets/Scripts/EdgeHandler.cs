using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeHandler : MonoBehaviour {

	public bool LeftEdge;

	private Strecth strecthHandler;
	private SpriteRenderer edgeGFX;
	private SphereCollider edgeCollider;

	void Start ()
	{
		strecthHandler = GetComponent<Strecth>();
		edgeGFX = GetComponent<SpriteRenderer>();
		edgeCollider = GetComponent<SphereCollider>();
	}

	//Working
	public void ConnectToParent (GameObject otherElement)
	{
		CircuitElement parent = gameObject.GetComponentInParent<CircuitElement> ();
		if (parent != null)
		{
			if (LeftEdge)
			{
				parent.ConnectToLeft (otherElement);
			}
			else 
			{
			  	parent.ConnectToRight (otherElement);
			}
			otherElement.GetComponent<EdgeHandler>().ConnectToParentNonRec(gameObject);
			edgeGFX.color = Color.red;
			edgeCollider.enabled = false;
		}
		else
		{
			Debug.LogWarning("Found an edge circle without parent<CircuitElement>");
		}
	}

	//Working
	public void ConnectToParentNonRec (GameObject otherElement)
	{
		CircuitElement parent = gameObject.GetComponentInParent<CircuitElement> ();
		if (parent != null)
		{
			if (LeftEdge)
			{
				parent.ConnectToLeft (otherElement);
			}
			else 
			{
			  	parent.ConnectToRight (otherElement);
			}
			Snap(otherElement.transform.position);
			edgeCollider.enabled = false;
		}
		else
		{
			Debug.LogWarning("Found an edge circle without parent<CircuitElement>");
		}
	}

	//Needs testing
	public void DisconnectFromParent (GameObject otherElement)
	{
		CircuitElement parent = gameObject.GetComponentInParent<CircuitElement> ();
		if (parent != null)
		{
			if (LeftEdge)
			{
				parent.DisconnectFromLeft (otherElement);
			}
			else 
			{
			  	parent.DisconnectFromRight (otherElement);
			}
			Desnap(otherElement.transform.position);
		}
		else
		{
			Debug.LogWarning("Found an edge circle with disconnect problem");
		}
	}

	//Snaps the position to given position and updates GFX
	private void Snap (Vector3 snapPosition)
	{
		//transform the location of edge
		this.transform.position = snapPosition;

		//strecth the wire/middle zone
		strecthHandler.StrecthTo(snapPosition);

		//disable the yellow circle/sprite
		edgeGFX.enabled = false;
	}

	//WIP
	private void Desnap (Vector3 oldSnapPosition)
	{
		//new location away from oldSnapPosition


		//strect to new loaction
		strecthHandler.StrecthTo(transform.position);


		//enable yellow circle/sprite
		edgeGFX.enabled = true;

	}

	//Needs testing???
	void OnMouseUp ()
	{

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
						if (hit [i].transform.GetComponent<Connector> () == null)
						{
							if (hit [i].transform.GetComponentInParent<EarthlingNode>() == null)
							{
								continue;
							}
							else
							{
								hit [i].transform.GetComponentInParent<EarthlingNode>().ConnectTo(gameObject);
							}

						}
						else
						{
							hit [i].transform.GetComponent<Connector> ().ConnectElement (gameObject);
						}
					}
					else
					{
						hit [i].transform.GetComponent<EdgeHandler> ().ConnectToParent (gameObject);
					}
					break;
				}
			}			
		}

	}

}
