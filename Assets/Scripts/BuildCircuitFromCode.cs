using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class BuildCircuitFromCode : MonoBehaviour {
	public Button myButton;

	public InputField circuit_code;

	string[] allElements;
	string[] resistance_values;
	string[] battery_values;
	int lamb_val;
	int earthing_val;
	int switch_val;
	int wire_val;
	int connector_val;
	bool checking;

	void Start () {
		Button btn = myButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		Debug.Log ("The code entered : "+circuit_code.text);
		 checking = inputValidate (circuit_code.text);
		Debug.Log ("CHECKıNG : " + checking);

		if (checking) {
			EditorUtility.DisplayDialog("Circuit Building From Code",
				"Your input is valid as a circuit code. Circuit will be generated " , "OK");
			fillGeneralVal (circuit_code.text);
		} else {
			EditorUtility.DisplayDialog("Circuit Building From Code",
				"Your input is invalid so check it then try again!! " , "OK");
		}
			
	}

	bool inputValidate(string code){

		int count = 0;
		bool valid = true;
		for(int i = 0; i< code.Length;i++){
			if (code [i] == '%') {
				count++;
			}
			if (!(char.IsDigit (code [i]))) {
				if (code [i] != '&' && code[i] != '%') {
					valid = false;
			
				}	
			}
			
		}
		Debug.Log ("Count ---> "+count);
		if (count != 7)
			valid = false;

		return valid;
	}

	void fillGeneralVal(string code){
		lamb_val = 0;
		earthing_val = 0;
		switch_val = 0;
		wire_val = 0;
		connector_val = 0;


		allElements = code.Split ('%');

		resistance_values = allElements [0].Split ('&');

		battery_values = allElements [1].Split ('&');

		lamb_val = int.Parse (allElements[2]);

		wire_val = int.Parse (allElements[3]);

		switch_val = int.Parse (allElements[4]);

		connector_val = int.Parse (allElements[5]);

		earthing_val = int.Parse (allElements[6]);

		Debug.Log ("Resistances : ");
		foreach(string temp in resistance_values){
			Debug.Log ("VAL:"+temp);
			
		}
		Debug.Log ("****************************");
		foreach(string temp1 in battery_values){
			Debug.Log ("VAL:"+temp1);

		}

		Debug.Log ("****************************");
		Debug.Log ("Lamb : "+lamb_val);
		Debug.Log ("****************************");
		Debug.Log ("wire : "+wire_val);
		Debug.Log ("****************************");
		Debug.Log ("Switch : "+switch_val);
		Debug.Log ("****************************");
		Debug.Log ("Connector : "+connector_val);
		Debug.Log ("****************************");
		Debug.Log ("Earthing : "+earthing_val);
		Debug.Log ("****************************");
	}
}
