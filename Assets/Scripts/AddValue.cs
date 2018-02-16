using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddValue : MonoBehaviour {


	public GameObject element;



	public string voltageValue;
	public string resistanceValue;


	public InputField resistanceField;



	public void Start () {


		resistanceField.ActivateInputField ();
	
	}
	public void Update(){
		resistanceField.ActivateInputField ();
	}

	public void GetInput(){
		
		string objectName;

		objectName = element.name;
			
		if(objectName =="Battery(Clone)"){
			voltageValue = resistanceField.text;
			int.Parse (voltageValue);
		}

		if (objectName == "Resistance(Clone)") {
			resistanceValue = resistanceField.text;
			int.Parse (resistanceValue);
		} else {
			
			Start();
		
		}
		}
			
			
				
		



}
