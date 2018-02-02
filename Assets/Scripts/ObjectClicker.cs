using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour {

    public GameObject actionPanel;

    public ActionMenu actionMenuScript;

    void Start()
    {
        actionMenuScript = GameObject.Find("Action Panel").GetComponent<ActionMenu>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
          
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    actionMenuScript.selectedElement = hit.transform.parent.gameObject;
//                    PrintName(hit.transform.parent.gameObject);
                    actionPanel.SetActive(true);
                }
            }
            else
            {
                //actionPanel.SetActive(false);
            }
        }
    }

    private void PrintName(GameObject go)
    {
        print(go.name);
    }




}
