﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenu : MonoBehaviour {

    public GameObject selectedElement;

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
        switch (actionName)
        {
            case "Move":
                moveObject();
                break;
            case "Delete":
                deleteObject();
                break;
        }
    }

    public void deleteObject()
    {
        Destroy(selectedElement);
        print("Object is destroyed");
        gameObject.SetActive(false);
    }

    private void moveObject()
    {

    }
}