using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {

	public LinkedList<CircuitElement> ItemList = new LinkedList<CircuitElement>();

	public void ConnectElement (EdgeHandler edge)
	{
		CircuitElement connectEle = edge.GetComponentInParent<CircuitElement>();
		ItemList.AddLast(connectEle);
		edge.ConnectToParentNonRec(edge);
		Debug.Log(connectEle.gameObject.name + " is added to the linkedlist");
	}

	public void DisconnectElement (CircuitElement connectEle)
	{
		ItemList.Remove(connectEle);
		Debug.Log(connectEle.gameObject.name + " is deleted from the linkedlist");
	}

}
