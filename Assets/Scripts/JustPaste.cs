using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JustPaste : MonoBehaviour {
	public Button myButton;
	public InputField circuit_code;
	// Use this for initialization
	void Start () {
		Button btn = myButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	public void TaskOnClick(){
		circuit_code.text = UniClipboard.GetText ();
	}
}
