using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour {

	public static SaveSystem instance;
	public string saveCode;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this);
		}
	}

	public void Save(int slot){
		string tmp = "save" + slot;
		PlayerPrefs.SetString (tmp, saveCode);
	}
	
	public void Load(int slot){
		string tmp = PlayerPrefs.GetString ("save"+slot);
		GameObject.FindObjectOfType<BuildCircuitFromCode> ().TaskOnClick(tmp);

	}
}
