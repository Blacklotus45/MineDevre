using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddValue : MonoBehaviour {

	public Voltage vol;
	public ResistanceValue res;

	public GameObject element;



	public string voltageValue;
	public string resistanceValue;


	public InputField valueField;



	public void Start () {


		valueField.ActivateInputField ();
		vol = GameObject.Find("Battery_UI").GetComponent<Voltage>();
		res = GameObject.Find("Resistance_UI").GetComponent<ResistanceValue>();
	}
	public void Update(){
		valueField.ActivateInputField ();
	}

	public void GetInput(){
		
		string objectName;

		objectName = element.name;
			
		if(objectName =="Battery(Clone)"){

			vol.voltage =int.Parse (valueField.text); 
		}

		if (objectName == "Resistance(Clone)") {

			res.resistance = int.Parse (valueField.text);


		} else {
			
			Start();
		
		}
		}
			
			
				
		



}
