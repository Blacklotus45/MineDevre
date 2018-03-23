using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTheScene : MonoBehaviour {

	public Button myButton;
	GameObject[] objs;
	// Use this for initialization
	void Start () {
		Button btn = myButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void TaskOnClick () {

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
