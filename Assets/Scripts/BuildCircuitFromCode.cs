using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildCircuitFromCode : MonoBehaviour {
	public Button myButton;
	public InputField circuit_code;

	void Start () {
		Button btn = myButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		Debug.Log ("The code entered : "+circuit_code.text);
	}

}
