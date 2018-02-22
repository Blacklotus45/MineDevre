using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitTraversal : MonoBehaviour {

	public static CircuitTraversal instance;

	private LinkedList<float> NodeList = new LinkedList<float>();

	private LinkedList<CircuitElement> VoltageSourceList = new LinkedList<CircuitElement>();
	private LinkedList<CircuitElement> PassiveList = new LinkedList<CircuitElement>();
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
			CircuitElement firstNode = earthlings [0].GetComponent<CircuitElement> ();
			UnknownElements.AddFirst (firstNode);

			//Runs as long as there is a potential new node
			while (UnknownElements.Count > 0)
			{
				CircuitElement topIterator = UnknownElements.First.Value;
				//If the element is already visited or surrounded
				if (!topIterator.isUnknown ())
				{
					UnknownElements.RemoveFirst ();
					continue;
				}

				//Make new Nodal list from current count
				int nodalID = NodeList.Count;
				NodeList.AddLast (-0.0f);

				Debug.Log ("Unknown element's name is " + topIterator.name + " and starting node " + nodalID);

				//Traverse and tag connected elements here
				CurrentTraversalElements = new LinkedList<CircuitElement> ();
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

					Debug.Log ("I'm " + iterator.name);

					//What element are we and what actions should we take
					switch (iterator.typeOfItem)
					{
					case CircuitElement.ElementType.Wire:
						iterator.checkSum = 2;
						iterator.nodeId = nodalID;
						CurrentTraversalElements.RemoveFirst ();
						break;
					case CircuitElement.ElementType.Switch:
						if (iterator.GetComponent<Switch> ().isClosed)
						{
							//Behavior like a Wire
							iterator.checkSum = 2;
							iterator.nodeId = nodalID;
						}
						else
						{
							//Behavior like a Node Change element
							nextItems = iterator.GetNeighbour ();
							if (nextItems != null)
								UnknownElements.AddLast (nextItems[0]);
							redirection = true;
							iterator.checkSum = 1;
						}
						CurrentTraversalElements.RemoveFirst ();
						break;
					case CircuitElement.ElementType.Resistance:
					case CircuitElement.ElementType.Lamp:
						nextItems = iterator.GetNeighbour ();
						if (nextItems != null)
							UnknownElements.AddLast (nextItems[0]);
						PassiveList.AddLast(iterator);
						iterator.AttachNodeToActive(nodalID);
						redirection = true;
						CurrentTraversalElements.RemoveFirst ();
						break;
					case CircuitElement.ElementType.Battery:
						nextItems = iterator.GetNeighbour ();
						if (nextItems != null)
							UnknownElements.AddLast (nextItems[0]);
						VoltageSourceList.AddLast (iterator);
						iterator.AttachNodeToActive(nodalID);
						redirection = true;
						CurrentTraversalElements.RemoveFirst ();
						break;
					case CircuitElement.ElementType.Earthing:
						iterator.checkSum = 2;
						iterator.nodeId = 0;
						CurrentTraversalElements.RemoveFirst ();
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
								nextItems[i].checkSum = 2;
								LinkedList<CircuitElement> tempList = nextItems[i].GetComponent<Connector> ().ItemList;
								foreach (CircuitElement celement in tempList)
									if (celement != iterator) CurrentTraversalElements.AddFirst (celement);	
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
				Debug.Log("Removed the " + UnknownElements.First.Value.name);
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
