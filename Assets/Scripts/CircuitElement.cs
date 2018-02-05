using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitElement : MonoBehaviour {

	public enum ElementType
    {
    	Wire,
    	Resistance,
    	Battery,
    	Switch,
    	Lamp,
		Earthing,
		Connector
    }

	public ElementType typeOfItem;
	public bool isLocked = false;

	public CircuitElement rightSide = null;
	public CircuitElement leftSide = null;

	public void ConnectToLeft(GameObject other)
	{
		leftSide = other.GetComponentInParent<CircuitElement>();
		isLocked = true;
	}

	public void ConnectToRight(GameObject other)
	{
		rightSide = other.GetComponentInParent<CircuitElement>();
		isLocked = true;
	}


	public void DisconnectFromLeft(GameObject other)
	{
		leftSide = null;
		if (rightSide == null || leftSide == null)
		{
			isLocked = false;
		}
	}


	public void DisconnectFromRight(GameObject other)
	{
		rightSide = null;
		if (rightSide == null || leftSide == null)
		{
			isLocked = false;
		}
	}
}
