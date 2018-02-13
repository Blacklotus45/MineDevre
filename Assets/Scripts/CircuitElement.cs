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
	public bool isChecked = false;

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

	public bool isUnknown ()
	{
		if (typeOfItem == ElementType.Earthing)
		{
			return gameObject.GetComponent<EarthlingNode>().isUnknown();
		}
		else
		{
			if (rightSide == null || leftSide == null)
			{
				Debug.LogError("Uncompleted element found, isUnknown() returns false.");
				return false;
			}
			else if (rightSide.isChecked && leftSide.isChecked)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}

	public CircuitElement GetNeighbour ()
	{
		if (typeOfItem == ElementType.Earthing)
		{
			return gameObject.GetComponent<EarthlingNode>().connectedNode;
		}

		if (leftSide == null || rightSide == null)
		{
			Debug.Log("Uncompleted element found, isUnknown() returns false.[" + name + "]");
			return null;
		}

		if (rightSide.isChecked && !leftSide.isChecked)
		{
			return leftSide;
		}
		else if (!rightSide.isChecked && leftSide.isChecked)
		{
			return rightSide;
		}
		else if (!rightSide.isChecked && !leftSide.isChecked)
		{
//			Debug.LogError("Access denied from non connected element!");
			return null;
		}
		else
		{
//			Debug.Log("Both elements are checked. Return null");
			return null;
		}
	}
}
