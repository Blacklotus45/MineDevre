using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : CircuitElement {

	public LinkedList<CircuitElement> ItemList = new LinkedList<CircuitElement>();
	public CircuitElement[] connectedParts = new CircuitElement[10];

	private int index = 0;

	void Start()
	{
		typeOfItem = ElementType.Connector;
	}

	public void ConnectElement (GameObject edge)
	{
		CircuitElement connectEle = edge.GetComponentInParent<CircuitElement>();
		ItemList.AddLast(connectEle);
		edge.GetComponent<EdgeHandler>().ConnectToParentNonRec(gameObject);

		isLocked = true;


		//For debug purpose
		if (index < 10)
		{
			connectedParts[index] = connectEle;
			index++;
		}
	}

	public void DisconnectElement (GameObject edge)
	{
		CircuitElement connectEle = edge.GetComponentInParent<CircuitElement>();
		ItemList.Remove(connectEle);
		//Disconnect other element

		if (connectedParts.Length == 0)
		{
			isLocked = false;
		}
	}

	private void Desnap ()
	{
		//re orientate disconnected element
	}


	//Overloads from CircuitElement also hiding
	public new void ConnectToLeft(GameObject other)
	{
		ConnectElement(other);
	}
	public new void ConnectToRight(GameObject other)
	{
		ConnectElement(other);
	}
	public new void DisconnectFromLeft(GameObject other)
	{
		DisconnectElement(other);
	}
	public new void DisconnectFromRight(GameObject other)
	{
		DisconnectElement(other);
	}

}
