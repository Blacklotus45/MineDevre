using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClickSwitch : MonoBehaviour {

	Ray ray;
	RaycastHit hit;

	public Sprite spriteAnahtar1,spriteAnahtar2;
	SpriteRenderer sr;
	bool one_click = false;
	bool timer_running;
	float timer_for_double_click;

	float delay = 0.3f;

	private Switch closeFlag;

	void Start()
	{
		closeFlag = gameObject.GetComponent<Switch>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			if(!one_click) // first click no previous clicks
			{
				one_click = true;

				timer_for_double_click = Time.time; // save the current time

				//Debug.Log("tek tık!!!");
			} 
			else
			{
				one_click = false; // found a double click, now reset
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				//Debug.Log("Çift tık!!! üzerimde: "+this.name);
				if(Physics.Raycast (ray, out hit))
				{
					
					if(hit.collider.tag == "Anahtar")
					{
						sr = hit.collider.gameObject.GetComponent <SpriteRenderer>();
						if (sr.sprite == spriteAnahtar1) // if the spriteRenderer sprite = sprite1 then change to sprite2
						{
							sr.sprite = spriteAnahtar2;
							closeFlag.isClosed = true;
							Debug.Log("Switch is Closed");
						}
						else
						{
							sr.sprite = spriteAnahtar1; // otherwise change it back to sprite1
							closeFlag.isClosed = false;
							Debug.Log("Switch is Open");
						}
					}

				}
			}
		}
		if(one_click)
		{
			// if the time now is delay seconds more than when the first click started. 
			if((Time. time - timer_for_double_click) > delay)
			{

				//basically if thats true its been too long and we want to reset so the next click is simply a single click and not a double click.

				one_click = false;

			}
		}
	}
}
