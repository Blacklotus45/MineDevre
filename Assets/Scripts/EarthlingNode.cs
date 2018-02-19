using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthlingNode : CircuitElement {

	public CircuitElement connectedNode = null;

	void Start ()
	{
		checkSum = 1;
	}


	public void ConnectTo(GameObject other)
	{
		connectedNode = other.GetComponentInParent<CircuitElement>();
		other.GetComponent<EdgeHandler>().ConnectToParentNonRec(gameObject);
		isLocked = true;
	}


	public void DisconnectFrom ()
	{
		if (connectedNode == null)
		{
			Debug.LogWarning ("Tried to disconnect null element from earthling\nDisconnect function error!");
		}
		else
		{
			connectedNode = null;
			isLocked = false;
		}

	}

	new public bool isUnknown ()
	{
		if (connectedNode == null)
		{
			Debug.Log("Uncompleted element found, isUnknown() returns false.[Earthling]");
			return false;
		}

		if (nodeAttached == null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}


}
