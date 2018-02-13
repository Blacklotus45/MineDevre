using System.Collections;
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
	
	// Update is called once per frame
	void Update () {
		
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

			/*if (firstNode.connectedNode == null)
			{
				Debug.LogError ("Nothing is connected to earthling");
				return;															//Actually it should be different but implement later
			}*/

			UnknownElements.AddLast (firstNode);

			while (UnknownElements.Count > 0)
			{
				CircuitElement topIterator = UnknownElements.First.Value;

				Debug.Log ("Unknown element's name is " + topIterator.name);

				//If the element is already visited or surrounded
				if (!topIterator.isUnknown ())
				{
					Debug.Log ("Already visited this element");
					UnknownElements.RemoveFirst ();
					continue;
				}


				//Make new Nodal list from current count
				int nodalID = NodeList.Count;
				CurrentTraversalElements = new LinkedList<CircuitElement> ();
				NodalList = new LinkedList<CircuitElement> ();
				NodeList.AddLast (NodalList);

				//Traverse and tag connected elements here
				CurrentTraversalElements.AddFirst (topIterator);
				while (CurrentTraversalElements.Count > 0)
				{
					CircuitElement iterator = CurrentTraversalElements.First.Value;
					CircuitElement nextItem;

					bool redirection = false;

					//Did we processed this before?
					if (iterator.isChecked == true)
					{
						Debug.Log ("I'm already processed " + iterator.name);
						CurrentTraversalElements.RemoveFirst();
						continue;
					}
					//Make it known if it's not parsed
					Debug.Log (iterator.name);
					iterator.isChecked = true;
					

					//What element are we and what actions should we take
					switch (iterator.typeOfItem)
					{
					case CircuitElement.ElementType.Wire:
						NodalList.AddLast (iterator);
						CurrentTraversalElements.RemoveFirst ();
						break;
					case CircuitElement.ElementType.Switch:
						if (iterator.GetComponent<Switch> ().isClosed)
						{
							NodalList.AddLast (iterator);
						}
						else
						{
							nextItem = iterator.GetNeighbour ();
							if (nextItem != null) UnknownElements.AddLast (nextItem);	
							redirection = true;
						}
						CurrentTraversalElements.RemoveFirst ();
						break;
					case CircuitElement.ElementType.Resistance:
					case CircuitElement.ElementType.Lamp:
						nextItem = iterator.GetNeighbour ();
						if (nextItem != null) UnknownElements.AddLast (nextItem);;
						CurrentTraversalElements.RemoveFirst ();
						redirection = true;
						break;
					case CircuitElement.ElementType.Battery:
						nextItem = iterator.GetNeighbour ();
						if (nextItem != null)
						{
							UnknownElements.AddLast (nextItem);
							VoltageSourceList.AddLast (iterator);
						}
						CurrentTraversalElements.RemoveFirst ();
						redirection = true;
						break;
					case CircuitElement.ElementType.Earthing:
						CurrentTraversalElements.RemoveFirst ();
						break;
					}

					//Sometimes we need to stop traversing, battery resistance things
					if (redirection) continue;
					

					//What comes next?
					nextItem = iterator.GetNeighbour ();
					if (nextItem == null)
					{
						Debug.Log ("Abort this operation. Either disconnected edge or already covered element.");
						break;
					}
					else if (nextItem.isChecked)
					{
						Debug.Log ("Abort this operation. Already checked element");
						break;
					}
					else
					{
						//If the connected type is multi connector, add the array
						if (nextItem.typeOfItem == CircuitElement.ElementType.Connector)
						{
							Debug.Log("Connector found");
							nextItem.isChecked = true;
							LinkedList<CircuitElement> tempList = nextItem.GetComponent<Connector> ().ItemList;
							foreach (CircuitElement celement in tempList)
							{
								if (celement != iterator)
								{
									Debug.Log("Added " + celement);
									CurrentTraversalElements.AddLast (celement);	
								}
							}
						}
						//If the conencted type is 2 way add it
						else 
						{
							CurrentTraversalElements.AddLast (nextItem);
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
