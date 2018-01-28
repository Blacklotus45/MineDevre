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
		Earthing
    }

	public ElementType typeOfItem;

	public CircuitElement rightSide = null;
	public CircuitElement leftSide = null;

	public void ConnectToLeft(GameObject other)
	{
		leftSide = other.GetComponent<CircuitElement>();
	}

	public void ConnectToRight(GameObject other)
	{
		rightSide = other.GetComponent<CircuitElement>();
	}
	

}
