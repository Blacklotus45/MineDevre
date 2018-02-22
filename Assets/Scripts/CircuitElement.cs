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

	//For battery fields, rightNode is the pozitive side
	private int leftNode = -1; 
	private int rightNode = -1;

	//For wire like features
	public int nodeId = -1;

	//For battery
	public void AttachNodeToActive (int nodalIdToAttach)
	{
		//Rightside is procesed and leftside is not
		if (rightSide != null && rightSide.checkSum > 1 && leftSide != null && leftSide.checkSum == 0)
		{
			rightNode = nodalIdToAttach;
			Debug.Log("Pozitive side");
		}
		//Leftside is processed and rightSide is not
		else if (rightSide != null && rightSide.checkSum == 0 && leftSide != null && leftSide.checkSum > 1)
		{
			leftNode = nodalIdToAttach;
			Debug.Log("Negative side");
		}
		else
		{
			//Leftnode is assigned and it is rightnodes turn
			if (leftNode != -1 && rightNode == -1)
			{
				rightNode = nodalIdToAttach;
				Debug.Log("Pozitive side");
			}
			//Leftnode is not assigned but rightside is assigned
			else if (leftNode == -1 && rightNode != -1)
			{
				leftNode = nodalIdToAttach;
				Debug.Log("Negative side");
			}
			//Error hadnling
			else
			{
				Debug.LogError("Problem occured at battery SideNode assignment!!!");
				Debug.LogError("RightNode: " + rightNode + " LeftNode: " + leftNode);
				return;
			}
		}

		checkSum++;
	}

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
			if (nodeId == -1)
			{
				return true;
			}
			else
			{
				Debug.Log("Already attached to a node! " + name + "\nNode ID is " + nodeId);
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

		if (rightSide.checkSum > 1 && leftSide.checkSum < 2)
		{
			return new CircuitElement[]{leftSide};
		}
		else if (rightSide.checkSum < 2 && leftSide.checkSum > 1)
		{
			return new CircuitElement[]{rightSide};
		}
		else if (rightSide.checkSum > 1 && leftSide.checkSum > 1)
		{
			return null;
		}
		else //this part needs to return both
		{
			Debug.Log("Both elements not checked");
			return new CircuitElement[]{rightSide,leftSide};
		}
	}

}
