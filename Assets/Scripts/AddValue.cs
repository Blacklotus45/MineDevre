using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddValue : MonoBehaviour {





	public string voltageValue;
	public string resistanceValue;

	public InputField voltageField;
	public InputField resistanceField;

	public void Start () {

		voltageField.text = "Enter Voltage Here...";
		resistanceField.text = "Enter Resistance Here...";
	
	}

	public void GetInput(){



				voltageValue = voltageField.text;
				int.Parse (voltageValue);
			

			
				resistanceValue = resistanceField.text;
				int.Parse (resistanceValue);
		
	}


}
