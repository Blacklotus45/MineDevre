using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragParent : MonoBehaviour {

	float distance = 30f;
    public GameObject actionPanel;

    void Start()
    {
        actionPanel = GameObject.Find("Action Panel");
    }

    void OnMouseDrag ()
	{
		if (gameObject.GetComponentInParent<CircuitElement> () != null)
		{
			if (gameObject.GetComponentInParent<CircuitElement> ().isLocked)
			{
				return;
			}
		}
		else if (gameObject.GetComponentInParent<EarthlingNode>() != null)
		{
			//check if its connected
		}

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition) - transform.localPosition;

//		Debug.Log("MousePosition: " + mousePosition + "\nobjPosition: " + objPosition);
        transform.parent.position = objPosition;
        actionPanel.transform.position = mousePosition;

    }
}
