using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {

	public LinkedList<CircuitElement> ItemList = new LinkedList<CircuitElement>();
	public CircuitElement[] connectedParts = new CircuitElement[10];

	private int index = 0;

	public void ConnectElement (EdgeHandler edge)
	{
		CircuitElement connectEle = edge.GetComponentInParent<CircuitElement>();
		ItemList.AddLast(connectEle);
		edge.ConnectToParentNonRec(edge);
		if (index < 10)
		{
			connectedParts[index] = connectEle;
			index++;
		}
	}

	public void DisconnectElement (EdgeHandler edge)
	{
		CircuitElement connectEle = edge.GetComponentInParent<CircuitElement>();
		ItemList.Remove(connectEle);
		//edge.ConnectToParentNonRec(edge);
	}

	private void Desnap ()
	{
		//re orientate disconnected element
	}

}
