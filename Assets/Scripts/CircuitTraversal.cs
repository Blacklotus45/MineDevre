﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitTraversal : MonoBehaviour {

	public static CircuitTraversal instance;

	private LinkedList<CircuitElement> VoltageSourceList = new LinkedList<CircuitElement>();
	private LinkedList<LinkedList<CircuitElement>> NodeList = new LinkedList<LinkedList<CircuitElement>>();
	private LinkedList<CircuitElement> NodalList;

	private LinkedList<CircuitElement> CurrentTraversalElements = new LinkedList<CircuitElement>();
	private LinkedList<CircuitElement> UnknownElements = new LinkedList<CircuitElement>();


	void OnEnable ()
	{
		BeginTraversal();
	}

	// Use this for initialization
	void Start () {
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		//BeginTraversal();	
	}
	
	public void AddToNode (int index, CircuitElement elementToAdd)
	{
		LinkedListNode<LinkedList<CircuitElement>> temp = NodeList.First;

		for (int i = 0; i < index; i++)
		{
			temp = temp.Next;
		}

		temp.Value.AddLast(elementToAdd);
	}

	public void BeginTraversal ()
	{
		GameObject[] earthlings = GameObject.FindGameObjectsWithTag ("Earth");
		if (earthlings.Length == 0)
		{
			//start from random location
			Debug.Log ("Zero Earthling found");
		}
		else if (earthlings.Length == 1)
		{
			//single start point
			//Debug.Log ("One Earthling found");

			CircuitElement firstNode = earthlings [0].GetComponent<CircuitElement> ();

			UnknownElements.AddFirst (firstNode);

			while (UnknownElements.Count > 0)
			{
				CircuitElement topIterator = UnknownElements.First.Value;

				//If the element is already visited or surrounded
				if (!topIterator.isUnknown ())
				{
					Debug.Log ("Already visited this element" + topIterator.name);
					UnknownElements.RemoveFirst ();
					continue;
				}


				//Make new Nodal list from current count
				int nodalID = NodeList.Count;
				Debug.Log ("Unknown element's name is " + topIterator.name + " and starting node " + nodalID);
				CurrentTraversalElements = new LinkedList<CircuitElement> ();
				NodalList = new LinkedList<CircuitElement> ();
				NodeList.AddLast (NodalList);

				//Traverse and tag connected elements here
				CurrentTraversalElements.AddFirst (topIterator);
				while (CurrentTraversalElements.Count > 0)
				{
					CircuitElement iterator = CurrentTraversalElements.First.Value;
					CircuitElement[] nextItems;

					bool redirection = false;

					//Did we processed this before? also this field must be reference checking not bool checking
					if (iterator.checkSum > 1)
					{
						Debug.Log ("I'm already processed " + iterator.name);
						CurrentTraversalElements.RemoveFirst ();
						continue;
					}

					/*Make it known if it's not parsed
					iterator.isChecked = true;*/
					//Obsolete now that we update status at what element they are

					Debug.Log (iterator.name);

					//What element are we and what actions should we take
					switch (iterator.typeOfItem)
					{
					case CircuitElement.ElementType.Wire:
						NodalList.AddLast (iterator);
						CurrentTraversalElements.RemoveFirst ();
						iterator.checkSum = 2;
						iterator.nodeAttached = NodalList;
						break;
					case CircuitElement.ElementType.Switch:
						if (iterator.GetComponent<Switch> ().isClosed)
						{
							NodalList.AddLast (iterator);
							iterator.checkSum = 2;
							iterator.nodeAttached = NodalList;
						}
						else
						{
							nextItems = iterator.GetNeighbour ();
							if (nextItems != null)
								UnknownElements.AddFirst (nextItems[0]);	
							redirection = true;
							iterator.checkSum = 1;
						}
						CurrentTraversalElements.RemoveFirst ();
						break;
					case CircuitElement.ElementType.Resistance:
					case CircuitElement.ElementType.Lamp:
						nextItems = iterator.GetNeighbour ();
						if (nextItems != null)
							UnknownElements.AddFirst (nextItems[0]);
						CurrentTraversalElements.RemoveFirst ();
						redirection = true;
						iterator.checkSum = 1;
						break;
					case CircuitElement.ElementType.Battery:
						nextItems = iterator.GetNeighbour ();
						if (nextItems != null)
						{
							UnknownElements.AddFirst (nextItems[0]);
							VoltageSourceList.AddLast (iterator);
						}
						CurrentTraversalElements.RemoveFirst ();
						redirection = true;
						iterator.checkSum = 1;
						break;
					case CircuitElement.ElementType.Earthing:
						CurrentTraversalElements.RemoveFirst ();
						iterator.checkSum = 2;
						iterator.nodeAttached = NodalList;
						break;
					}

					//Sometimes we need to stop traversing, battery resistance things
					if (redirection)
						continue;
					

					//What comes next?
					nextItems = iterator.GetNeighbour ();
					if (nextItems == null)
					{
						Debug.Log ("Abort this operation. Either disconnected edge.");
						break;
					}
					else
					{
						for (int i = 0; i < nextItems.Length; i++)
						{
							//If the connected type is multi connector, add the array
							if (nextItems [i].typeOfItem == CircuitElement.ElementType.Connector)
							{
								Debug.Log("Connector found");
								nextItems[i].checkSum = 2;
								LinkedList<CircuitElement> tempList = nextItems[i].GetComponent<Connector> ().ItemList;
								foreach (CircuitElement celement in tempList)
								{
									if (celement != iterator)
									{
										Debug.Log("Added " + celement);
										CurrentTraversalElements.AddFirst (celement);	
									}
								}
							}
							//If the conencted type is 2 way add it
							else 
							{
								CurrentTraversalElements.AddFirst (nextItems[i]);
							}
						}
					}
				}


				//After traversed every connected element delete from the unknown
				UnknownElements.RemoveFirst();
			}

			Debug.Log("Traversal is Finished correctly!!");
		}
		else
		{
			//we have multiple 0 nodes
			Debug.Log("Many Earthling found");
		}
	}
}
