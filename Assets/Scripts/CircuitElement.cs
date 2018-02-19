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

    // Temporary vars
    public int temporaryResistance;
    public int temporaryVoltage;

    public int checkSum = 0;					//Checksum is used for checked connectivity, 1 is for looked in single directipn 2 for both

	public ElementType typeOfItem;
	public bool isLocked = false;

	public CircuitElement rightSide = null;
	public CircuitElement leftSide = null;

	public LinkedList<CircuitElement> nodeAttached = null;

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
			if (nodeAttached == null)
			{
				return true;
			}
			else
			{
				Debug.Log("Already attached to a node! " + name);
				return false;
			}
		}
	}

	public CircuitElement[] GetNeighbour ()
	{

		if (typeOfItem == ElementType.Earthing)
		{
			return new CircuitElement[]{gameObject.GetComponent<EarthlingNode>().connectedNode};
		}

		if (leftSide == null || rightSide == null)
		{
			Debug.Log("Uncompleted element found, isUnknown() returns false.[" + name + "]");
			return null;
		}

		if (rightSide.checkSum > 1 && leftSide.checkSum < 1)
		{
			return new CircuitElement[]{leftSide};
		}
		else if (rightSide.checkSum < 1 && leftSide.checkSum > 1)
		{
			return new CircuitElement[]{rightSide};
		}
		else if (rightSide.checkSum > 1 && leftSide.checkSum > 1)
		{
			Debug.LogError("Both sides are checked");
			return null;
		}
		else //this part needs to return both
		{
			Debug.Log("Both elements not checked");
			return new CircuitElement[]{rightSide,leftSide};
		}
	}

}
