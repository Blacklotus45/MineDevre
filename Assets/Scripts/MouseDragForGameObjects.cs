using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragForGameObjects : MonoBehaviour {

    float distance = 30f;
    private GameObject actionPanel;

    void Start()
    {
        actionPanel = GameObject.Find("Action Panel");
    }

    void OnMouseDrag()
    {
		if (gameObject.GetComponent<CircuitElement> () != null)
		{
			if (gameObject.GetComponent<CircuitElement> ().isLocked)
			{
				return;
			}
		}

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
        actionPanel.transform.position = mousePosition;
    }

}
