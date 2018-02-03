using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {

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

	public ElementType typeOfItem = ElementType.Connector;

	public LinkedList<CircuitElement> ItemList = new LinkedList<CircuitElement>();

	public void ConnectElement (CircuitElement connectEle)
	{
		ItemList.AddLast(connectEle);
	}

	public void DisconnectElement (CircuitElement connectEle)
	{
		ItemList.Remove(connectEle);
	}

}
