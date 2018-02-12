using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddValue : MonoBehaviour {

	public GameObject selectedElement;

	public string voltageValue;
	public string currentValue;

	public InputField voltageField;
	public InputField currentField;

	public void Start () {

		voltageField.text = "Enter Voltage Here...";
		currentField.text = "Enter Current Here...";
	
	}

	public void Awake(){
	
	}

	public void GetInput(){

		voltageValue = voltageField.text;
		currentValue = currentField.text;
		int.Parse (voltageValue);
		int.Parse (currentValue);
	}

}
