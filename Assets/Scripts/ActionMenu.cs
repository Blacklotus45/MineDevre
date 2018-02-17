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
        //valueField.text = circuitElement.temporaryVal + "";

        string type = circuitElement.typeOfItem.ToString();
        switch (type)
        {
            case "Resistance":
                valueField.text = circuitElement.temporaryResistance + "";
                break;

            case "Battery":
                valueField.text = circuitElement.temporaryVoltage + "";
                break;

            case "Lamp":
                valueField.text = circuitElement.temporaryResistance + "";
                break;

                //temp
            case "Wire":
                valueField.text = circuitElement.temporaryResistance + "";
                break;

                //temp
            case "Switch":
                valueField.text = circuitElement.temporaryResistance + "";
                break;

            default:
                break;


        }
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

        //((Battery)circuitElement).voltageValue = int.Parse(valueField.text);

        string type = circuitElement.typeOfItem.ToString();
        switch (type)
        {
            case "Resistance":
                circuitElement.temporaryResistance = int.Parse(valueField.text);
                break;

            case "Battery":
                circuitElement.temporaryVoltage = int.Parse(valueField.text);
                break;

            case "Lamp":
                circuitElement.temporaryResistance = int.Parse(valueField.text);
                break;

            default:
                break;


        }
       
        //circuitElement.temporaryVal = int.Parse(valueField.text);
    }

    //public int getCircuitElementVal()
    //{
    //    return circuitElement.temporaryVal;
    //}





}
