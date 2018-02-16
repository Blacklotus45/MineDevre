using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenu : MonoBehaviour {

    public GameObject selectedElement;

	public GameObject[] edges;

	// Use this for initialization
	void Start () {

    }

    private void Awake()
    {

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
}
