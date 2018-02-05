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

	public void ConnectToLeft(EdgeHandler other)
	{
		leftSide = other.GetComponentInParent<CircuitElement>();
		isLocked = true;
	}

	public void ConnectToRight(EdgeHandler other)
	{
		rightSide = other.GetComponentInParent<CircuitElement>();
		isLocked = true;
	}

	//No need for paremeter?
	public void DisconnectFromLeft(EdgeHandler other)
	{
		leftSide = null;
		if (rightSide == null || leftSide == null)
		{
			isLocked = false;
		}
	}

	//No need for paremeter?
	public void DisconnectFromRight(EdgeHandler other)
	{
		rightSide = null;
		if (rightSide == null || leftSide == null)
		{
			isLocked = false;
		}
	}
}
