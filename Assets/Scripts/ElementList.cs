using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementList : MonoBehaviour {

	static public ElementList TheList;

	public GameObject[] RcElements;

	// Use this for initialization
	void Start () {
		if (TheList == null)
		{
			TheList = this;
		}
		else
		{
			Destroy(this);
		}
	}

}
