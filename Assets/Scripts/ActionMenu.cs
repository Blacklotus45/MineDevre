using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour {

    public GameObject selectedElement;
    private CircuitElement circuitElement;


	public GameObject[] edges;


    public InputField valueField;
    bool firstTime = true;


    // Use this for initialization
    void Start () {
       
    }

    private void Awake()
    {
    
    }

    void OnEnable()
    {
        if (firstTime)
        {
            firstTime = false;
        }
        else
        {
            print("enabled");

            updateActionPanelValuesAndLocation();
        }
        
    }

    public void updateActionPanelValuesAndLocation()
    {
        transform.position = new Vector3((float)(((selectedElement.transform.position.x) + 26.66) / 53.32 * 1024), (((selectedElement.transform.position.y) + 20) / 40 * 768), 0);


        circuitElement = GameObject.Find(selectedElement.name).GetComponent<CircuitElement>();

        //print("HI");
        //print(circuitElement);
        //print("2nd HI");
        valueField.text = circuitElement.temporaryVal + "";
    }


    // Update is called once per frame
    void Update () {
        /* if (selectedElement != null)
        {
            gameObject.transform.position = selectedElement.transform.position;

        } */
    }

    public void OnClick(string actionName)
    {
        /*
        switch (actionName)
        {
            case "Move":
                moveObject();
                break;
            case "Delete":
                deleteObject();
                break;
        }
        */
    }

	//Working
    public void DeleteObject()
    {
        Destroy(selectedElement);
        print("Object is destroyed");
        gameObject.SetActive(false);
    }

	//Wip
	public void SplitObject()
	{
		edges = selectedElement.GetComponentsInChildren<GameObject> ();
		print ("Edges:\n");

	}


    private void MoveObject()
    {

    }

    public void SetVisible(bool b)
    {
        gameObject.SetActive(b);
    }


    public void SetValueOfGameObject()
    {
        circuitElement.temporaryVal = int.Parse(valueField.text);
    }

    public int getCircuitElementVal()
    {
        return circuitElement.temporaryVal;
    }





}
