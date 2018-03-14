using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour {

	private AddValue av;
    private ActionMenu actionMenu;


    //public GameObject actionPanelGameObject;


    void Start()
    {
        actionMenu = GameObject.Find("Action Panel").GetComponent<ActionMenu>();

        av = GameObject.Find("AddController").GetComponent<AddValue>();
    }

    // Update is called once per frame
    void Update ()
	{

		if (Input.GetMouseButtonDown (0))
		{
          
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, 100.0f))
			{
				//done for connector object, more efficient structure maybe used here
				if (hit.transform.parent == null)
				{
					actionMenu.selectedElement = hit.transform.gameObject;
                }
				else //circuit elements
				{
					actionMenu.selectedElement = hit.transform.parent.gameObject;
                    av.element = hit.transform.parent.gameObject;

                    actionMenu.updateActionPanelValuesAndLocation();
                    Vector3 v = Camera.main.WorldToScreenPoint(hit.transform.position);
                    actionMenu.SetVisible(v);

                }
            }
        }
    }



}
