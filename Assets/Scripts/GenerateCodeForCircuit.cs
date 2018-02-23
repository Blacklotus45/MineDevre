using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateCodeForCircuit : MonoBehaviour {
	public Button myButton;
	GameObject top;
	GameObject[] objs;
	private bool showPopUp = false;
	private CircuitElement circuitElement;
	private CircuitElement circuitElement2;



	public string generalCode;
	string resistance_code = "";
	string battery_code = "";
	int lamb_code = -1;
	int earthing_code = -1;
	int switch_code = -1;
	int wire_code = -1;
	int connector_code = -1;

	// Use this for initialization
	void Start () {
		Button btn = myButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	void TaskOnClick(){
		resistance_code = "";
		battery_code = "";
		lamb_code = -1;
		earthing_code = -1;
		switch_code = -1;
		wire_code = -1;
		connector_code = -1;
		top = transform.root.gameObject;
		Debug.Log (top.name + "!!!!");

		objs =  UnityEngine.Object.FindObjectsOfType<GameObject>() ;

		foreach (GameObject element in objs) {
			if(element.name.Contains("Resistance")  && (!element.name.Contains("UI"))){
				
				circuitElement = element.GetComponent<CircuitElement>();
				resistance_code = resistance_code + circuitElement.temporaryResistance+ "&";
			
			}else if(element.name.Contains("Battery") && (!element.name.Contains("UI"))){
				circuitElement2 = element.GetComponent<CircuitElement>();

				battery_code = battery_code + circuitElement2.temporaryVoltage+ "&";
			
			}else if(element.name.Contains("Lamp")){
				lamb_code = lamb_code + 1;
			}else if(element.name.Contains("Wire")){
				wire_code = wire_code + 1;
			}else if(element.name.Contains("Switch")){
				switch_code = switch_code + 1;
			}else if(element.name.Contains("Connector")){
				connector_code = connector_code + 1;
			}else if(element.name.Contains("Earthing")){
				earthing_code = earthing_code + 1;
			}
		
	  }
		generalCode = resistance_code + "%" + battery_code + "%" + lamb_code + "%" + wire_code + "%" + switch_code + "%" + connector_code + "%" + earthing_code + "%";
		Debug.Log ("General Code : " + generalCode);
		showPopUp = true;


	
	}


	void OnGUI()
	{
		if (showPopUp)
		{
			GUI.Window(0, new Rect((Screen.width/2)-250, (Screen.height/2)-75
				, 300, 250), ShowGUI, "Generated Code For Circuit");

		}
	}

	void ShowGUI(int windowID)
	{
		

		GUI.Label(new Rect(60, 80, 220, 60), "Your Code is : "+generalCode);


		if (GUI.Button(new Rect(50, 150, 75, 30), "OK"))
		{
			showPopUp = false;

		}

		if(GUI.Button(new Rect(180,150,75,30), "Copy")) {

			GUIUtility.systemCopyBuffer = generalCode;		
		
		}

	}




}
