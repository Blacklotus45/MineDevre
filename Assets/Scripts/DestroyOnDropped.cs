using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DestroyOnDropped : MonoBehaviour, IDropHandler {

	static public DestroyOnDropped instance;
	static public bool DestroyedFlag = false;

	void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void OnDrop(PointerEventData data)
    {
		if (DragHandeler.icon != null)
        {
			Debug.Log("Got Dropped");
			DestroyedFlag = true;
        }
    }
}
