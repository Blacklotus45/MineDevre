using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapSprites : MonoBehaviour {


	public Button yourButton;
	public Sprite spritePil1; 
	public Sprite spritePil2;
	public Sprite spriteDirenc1; 
	public Sprite spriteDirenc2;
	public Sprite spriteLamba1; 
	public Sprite spriteLamba2;
	SpriteRenderer[] sprts ;
	GameObject[] objs;
	GameObject top;

	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
		top = transform.root.gameObject;
		Debug.Log (top.name + "!!!!");


		objs =  UnityEngine.Object.FindObjectsOfType<GameObject>() ;


		foreach(GameObject temp in objs){
			if (temp.name == "Battery(Clone)" || temp.name == "Resistance(Clone)" || temp.name == "Lamp(Clone)") {
				Debug.Log (temp.name);
				sprts = temp.GetComponentsInChildren<SpriteRenderer>();


				foreach(SpriteRenderer goTemp in sprts) {
					Debug.Log ("Go Temp : ----> "+goTemp.name);
					if(goTemp.tag == "Pil"){
						Debug.Log ("Bu bir pildir ve sprite ismi: "+ goTemp.sprite.name);
						if (goTemp.sprite == spritePil1) // if the spriteRenderer sprite = sprite1 then change to sprite2
						{
							goTemp.sprite = spritePil2;
						}
						else
						{
							goTemp.sprite = spritePil1; // otherwise change it back to sprite1
						}
					}else if(goTemp.tag == "Direnc"){
						Debug.Log ("Bu bir direnctir ve sprite ismi: "+ goTemp.sprite.name);
						if (goTemp.sprite == spriteDirenc1) // if the spriteRenderer sprite = sprite1 then change to sprite2
						{
							goTemp.sprite = spriteDirenc2;
						}
						else
						{
							goTemp.sprite = spriteDirenc1; // otherwise change it back to sprite1
						}
					}else if(goTemp.tag == "Lamba"){
						Debug.Log ("Bu bir lambadir ve sprite ismi: "+ goTemp.sprite.name);
						if (goTemp.sprite == spriteLamba1) // if the spriteRenderer sprite = sprite1 then change to sprite2
						{
							goTemp.sprite = spriteLamba2;
						}
						else
						{
							goTemp.sprite = spriteLamba1; // otherwise change it back to sprite1
						}
					}
					//Debug.Log ("Name : "+goTemp.name+" Tag: "+goTemp.tag);
					//Debug.Log (this.GetComponent<SpriteRenderer>().name); 
				}

			}
			
		}

	}
}
