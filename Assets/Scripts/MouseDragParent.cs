using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragParent : MonoBehaviour {

	float distance = 20;
    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);


        transform.parent.position = objPosition;

    }
}
