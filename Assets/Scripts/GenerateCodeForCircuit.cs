using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;

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
	string lamb_code = "";
	string earthing_code = "";
	string switch_code = "";
	string wire_code = "";
	string connector_code = "";

	string tempStorage="";
	double tempX= 0.0f;
	double tempY = 0.0f;

	int lamb_counter = 0;
	int earthing_counter = 0;
	int switch_counter = 0;
	int wire_counter = 0;
	int connector_counter = 0;

	// Use this for initialization
	void Start () {
		Button btn = myButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	void TaskOnClick(){
		resistance_code = "";
		battery_code = "";
		lamb_code = "";
		earthing_code = "";
		switch_code = "";
		wire_code = "";
		connector_code = "";


		lamb_counter = 0;
		earthing_counter = 0;
		switch_counter = 0;
		wire_counter = 0;
		connector_counter = 0;
		top = transform.root.gameObject;
		Debug.Log (top.name + "!!!!");

		objs =  UnityEngine.Object.FindObjectsOfType<GameObject>() ;

		foreach (GameObject element in objs) {
			tempStorage = "";
			tempX= 0.0f;
			tempY = 0.0f;
			if(element.name.Contains("Resistance")  && (!element.name.Contains("UI"))){

				circuitElement = element.GetComponent<CircuitElement>();
				tempStorage = circuitElement.temporaryResistance+"";

				tempX = System.Math.Round (element.transform.position.x,2);
				tempY = System.Math.Round (element.transform.position.y,2);
				tempStorage = tempStorage + "|" + tempX + "|" + tempY;

				resistance_code = resistance_code + tempStorage+ "&";

			
			}else if(element.name.Contains("Battery") && (!element.name.Contains("UI"))){

				circuitElement2 = element.GetComponent<CircuitElement>();
				tempStorage = circuitElement2.temporaryVoltage+"";
				tempX = System.Math.Round (element.transform.position.x,2);
				tempY = System.Math.Round (element.transform.position.y,2);
				tempStorage = tempStorage + "|" + tempX + "|" + tempY;

				battery_code = battery_code + tempStorage+ "&";
			
			}else if(element.name.Contains("Lamp")  && (!element.name.Contains("UI"))){

				lamb_counter = lamb_counter + 1;

				tempX = System.Math.Round (element.transform.position.x,2);
				tempY = System.Math.Round (element.transform.position.y,2);
				tempStorage = tempStorage + "|" + tempX + "|" + tempY;

				lamb_code = lamb_code + tempStorage+ "&";


			}else if(element.name.Contains("Wire")  && (!element.name.Contains("UI"))){

				wire_counter = wire_counter + 1;

				tempX = System.Math.Round (element.transform.position.x,2);
				tempY = System.Math.Round (element.transform.position.y,2);

				tempStorage = tempStorage + "|" + tempX + "|" + tempY;

				wire_code = wire_code + tempStorage+ "&";

			}else if(element.name.Contains("Switch")  && (!element.name.Contains("UI"))){

				switch_counter = switch_counter + 1;

				tempX = System.Math.Round (element.transform.position.x,2);
				tempY = System.Math.Round (element.transform.position.y,2);

				tempStorage = tempStorage + "|" + tempX + "|" + tempY;

				switch_code = switch_code + tempStorage+ "&";

			}else if(element.name.Contains("Connector")  && (!element.name.Contains("UI"))){
				
				connector_counter = connector_counter + 1;

				tempX = System.Math.Round (element.transform.position.x,2);
				tempY = System.Math.Round (element.transform.position.y,2);

				tempStorage = tempStorage + "|" + tempX + "|" + tempY;

				connector_code = connector_code + tempStorage+ "&";

			}else if(element.name.Contains("Earthing")  && (!element.name.Contains("UI"))){
				
				earthing_counter = earthing_counter + 1;

				tempX = System.Math.Round (element.transform.position.x,2);
				tempY = System.Math.Round (element.transform.position.y,2);

				tempStorage = tempStorage + "|" + tempX + "|" + tempY;

				earthing_code = earthing_code + tempStorage+ "&";
			}
		
	  }
		generalCode = resistance_code + "%" + battery_code + "%"+ lamb_code + "%"  + wire_code + "%" +  switch_code + "%" + connector_code + "%"+ earthing_code + "%";
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
		

		GUI.Label(new Rect(60, 80, 220, 60), "Your Code is (In short form hashed) : "+getSHA1(generalCode));


		if (GUI.Button(new Rect(50, 150, 75, 30), "OK"))
		{
			showPopUp = false;

		}

		if(GUI.Button(new Rect(180,150,75,30), "Copy")) {

			GUIUtility.systemCopyBuffer = generalCode;		
		
		}

	}

	string getSHA1(string g_code){

		SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider ();

		sh.ComputeHash (ASCIIEncoding.ASCII.GetBytes(g_code));
		byte[] re = sh.Hash;
		StringBuilder sb = new StringBuilder ();

		foreach (byte b in re) {
			sb.Append (b.ToString("x2"));
		}

		return sb.ToString();
	
	}




}
