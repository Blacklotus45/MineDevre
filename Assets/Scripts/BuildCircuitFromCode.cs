using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;


public class BuildCircuitFromCode : MonoBehaviour {
	public Button myButton;
	public GameObject resistance_prefab;
	public GameObject battery_prefab;
	public GameObject lamb_prefab;
	public GameObject earthing_prefab;
	public GameObject switch_prefab;
	public GameObject wire_prefab;
	public GameObject connector_prefab;
	public InputField circuit_code;

	GameObject[] objs;

	string[] allElements;
	string[] resistance_general;
	string[] resistance_values;
	string[] battery_general;
	string[] battery_values;

	string[] lamb_values;
	string[] lamb_general;

	string[] earthing_values;
	string[] earthing_general;

	string[] switch_values;
	string[] switch_general;

	string[] wire_values;
	string[] wire_general;

	string[] connector_values;
	string[] connector_general;

	bool checking;

	float x = -1.0f;
	float y = 0.0f;


	void Start () {
		Button btn = myButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
/*		Debug.Log ("The code entered : "+circuit_code.text);
		 checking = inputValidate (circuit_code.text);
		Debug.Log ("CHECKıNG : " + checking);

		if (checking) {
			EditorUtility.DisplayDialog("Circuit Building From Code",
				"Your input is valid as a circuit code. Circuit will be generated " , "OK");
			fillGeneralVal (circuit_code.text);

			ClearScene ();

			CreateElements ();

		} else {
			EditorUtility.DisplayDialog("Circuit Building From Code",
				"Your input is invalid so check it then try again!! " , "OK");
			
		}*/
			
	}

	bool inputValidate(string code){

		int count = 0;
		bool valid = true;
		for(int i = 0; i< code.Length;i++){
			if (code [i] == '%') {
				count++;
			}
			if (!(char.IsDigit (code [i]))) {
				if (code [i] == '&' || code [i] == '%' || code [i] == '|' || code [i] == '!' || code[i] == '.' || code[i] == '-') {
					
				
				} else {
				
					valid = false;
				}
			}
			
		}
		Debug.Log ("Count ---> "+count);
		int len = code.Length;
		if (count != 7 || code[len-1] != '%')
			valid = false;

		return valid;
	}

	void fillGeneralVal(string code){
		


		allElements = code.Split ('%');

		if (allElements [0] != "") {
			resistance_general = allElements [0].Split ('&');
		}

		if (allElements [1] != "") {
			battery_general = allElements [1].Split ('&');
		}

		if(allElements[2] != ""){
		lamb_general = allElements [2].Split ('&');
		}

		if (allElements [3] != "") {
			wire_general = allElements [3].Split ('&');
		}

		if (allElements [4] != "") {
			switch_general = allElements [4].Split ('&');
		}

		if(allElements[5] != ""){
			connector_general = allElements [5].Split ('&');
		}

		if(allElements[6] != ""){
			earthing_general = allElements [6].Split ('&');
		}

		/*
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
		*/
	}

	void CreateElements(){
		if(resistance_general[0]!=""){
			for (int i = 0; i < resistance_general.Length - 1; i++) {
				resistance_values = resistance_general [i].Split ('|');

				x = float.Parse (resistance_values[1]);
				y = float.Parse (resistance_values[2]);

				GameObject clone = (GameObject)Instantiate (resistance_prefab, new Vector3 (x, y, 0), Quaternion.identity);
				clone.name = "Resistance " + i;
				clone.GetComponent<CircuitElement> ().temporaryResistance = int.Parse (resistance_values[0]);
			}
		}

		if(battery_general[0]!=""){
			for (int i = 0; i < battery_general.Length - 1; i++) {
				battery_values = battery_general [i].Split ('|');

				x = float.Parse (battery_values[1]);
				y = float.Parse (battery_values[2]);

				GameObject clone = (GameObject)Instantiate (battery_prefab, new Vector3 (x, y, 0), Quaternion.identity);
				clone.name = "Battery " + i;
				clone.GetComponent<CircuitElement> ().temporaryVoltage = int.Parse (battery_values[0]);
			}
		}

		if(lamb_general[0]!=""){
			for (int i = 0; i < lamb_general.Length - 1; i++) {
				lamb_values = lamb_general [i].Split ('|');

				x = float.Parse (lamb_values[1]);
				y = float.Parse (lamb_values[2]);

				GameObject clone = (GameObject)Instantiate (lamb_prefab, new Vector3 (x, y, 0), Quaternion.identity);
				clone.name = "Lamb " + i;
			}
		}

		if(wire_general[0]!=""){
			for (int i = 0; i < wire_general.Length - 1; i++) {
				wire_values = wire_general [i].Split ('|');

				x = float.Parse (wire_values[1]);
				y = float.Parse (wire_values[2]);

				GameObject clone = (GameObject)Instantiate (wire_prefab, new Vector3 (x, y, 0), Quaternion.identity);
				clone.name = "Wire " + i;

			}
		}

		if(switch_general[0]!=""){
			for (int i = 0; i < switch_general.Length - 1; i++) {
				switch_values = switch_general [i].Split ('|');

				x = float.Parse (switch_values[1]);
				y = float.Parse (switch_values[2]);

				GameObject clone = (GameObject)Instantiate (switch_prefab, new Vector3 (x, y, 0), Quaternion.identity);
				clone.name = "Switch " + i;

			}
		}

		if(connector_general[0]!=""){
			for (int i = 0; i < connector_general.Length - 1; i++) {
				connector_values = connector_general [i].Split ('|');

				x = float.Parse (connector_values[1]);
				y = float.Parse (connector_values[2]);

				GameObject clone = (GameObject)Instantiate (connector_prefab, new Vector3 (x, y, 0), Quaternion.identity);
				clone.name = "Connector " + i;

			}
		}

		if(earthing_general[0]!=""){
			for (int i = 0; i < earthing_general.Length - 1; i++) {
				earthing_values = earthing_general [i].Split ('|');

				x = float.Parse (earthing_values[1]);
				y = float.Parse (earthing_values[2]);

				GameObject clone = (GameObject)Instantiate (earthing_prefab, new Vector3 (x, y, 0), Quaternion.identity);
				clone.name = "Earthing " + i;

			}
		}


	}

	void ClearScene(){
		objs =  UnityEngine.Object.FindObjectsOfType<GameObject>() ;
		foreach (GameObject element in objs) {
			
			if(element.name.Contains("Resistance")  && (!element.name.Contains("UI"))){

				Destroy (element);


			}else if(element.name.Contains("Battery") && (!element.name.Contains("UI"))){

				Destroy (element);

			}else if(element.name.Contains("Lamp")  && (!element.name.Contains("UI"))){

				Destroy (element);


			}else if(element.name.Contains("Wire")  && (!element.name.Contains("UI"))){

				Destroy (element);

			}else if(element.name.Contains("Switch")  && (!element.name.Contains("UI"))){

				Destroy (element);

			}else if(element.name.Contains("Connector")  && (!element.name.Contains("UI"))){

				Destroy (element);

			}else if(element.name.Contains("Earthing")  && (!element.name.Contains("UI"))){

				Destroy (element);;
			}

		}
	
	}
}
