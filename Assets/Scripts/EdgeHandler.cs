using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeHandler : MonoBehaviour {

	public bool LeftEdge;

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
		}
		else
		{
			Debug.LogWarning("Found an edge circle without parent<CircuitElement>");
		}

	}
}
