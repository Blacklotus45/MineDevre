using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitTraversal : MonoBehaviour {

	public static CircuitTraversal instance;

	//After we calculated the values for nodes, they can be accesed from this list
	public LinkedList<float> NodeList = new LinkedList<float>();

	//These lists for keeping track of elements
	private LinkedList<CircuitElement> VoltageSourceList = new LinkedList<CircuitElement>();
	private LinkedList<CircuitElement> PassiveList = new LinkedList<CircuitElement>();

	//These lists for traversing circuit
	private LinkedList<CircuitElement> CurrentTraversalElements = new LinkedList<CircuitElement>();
	private LinkedList<CircuitElement> UnknownTraversalElements = new LinkedList<CircuitElement>();


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
			UnknownTraversalElements.AddFirst (firstNode);

			//Runs as long as there is a potential new node
			while (UnknownTraversalElements.Count > 0)
			{
				CircuitElement topIterator = UnknownTraversalElements.First.Value;
				//If the element is already visited or surrounded
				if (!topIterator.isUnknown ())
				{
					UnknownTraversalElements.RemoveFirst ();
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
								UnknownTraversalElements.AddLast (nextItems[0]);
							redirection = true;
							iterator.checkSum = 1;
						}
						CurrentTraversalElements.RemoveFirst ();
						break;
					case CircuitElement.ElementType.Resistance:
					case CircuitElement.ElementType.Lamp:
						nextItems = iterator.GetNeighbour ();
						if (nextItems != null)
							UnknownTraversalElements.AddLast (nextItems[0]);
						PassiveList.AddLast(iterator);
						iterator.AttachNodeToActive(nodalID);
						redirection = true;
						CurrentTraversalElements.RemoveFirst ();
						break;
					case CircuitElement.ElementType.Battery:
						nextItems = iterator.GetNeighbour ();
						if (nextItems != null)
							UnknownTraversalElements.AddLast (nextItems[0]);
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
				Debug.Log("Removed the " + UnknownTraversalElements.First.Value.name);
				UnknownTraversalElements.RemoveFirst();

			}

			Debug.Log("Traversal is Finished correctly!!");
		}
		else
		{
			//we have multiple 0 nodes
			Debug.Log("Many Earthling found");
		}

		CalculateValues();
	}


	/*We have 3 Main Matrix A,x and z which have [Ax = z] relation
	Our unknown values are located at x matrix, z matrix is given values by user
	A matrix is Relational and Conductance matrix*/
	private void CalculateValues ()
	{
		//Number of voltage sources and Nodes
		int m,n;
		m = VoltageSourceList.Count;
		n = NodeList.Count - 1;				//-1 because we don't compute Node 0

		//Main Matrixes
		float[,] Amatrix = new float[n+m,n+m];
		float[,] Xmatrix = new float[n+m,1];
		float[,] Zmatrix = new float[n+m,1];

		/*A matrix is created from 4 sub matrixes G,B,C and D with following relation
		A = | G  B |
			| C  D |
		*/
		float[,] Gmatrix = new float[n,n];
		float[,] Bmatrix = new float[n,m];
		float[,] Cmatrix;					//Initialize after Bmatrix with Transpose
		float[,] Dmatrix = new float[m,m];

		//This Part creates Gmatrix which is created from passive elements and Conductance
		LinkedListNode<CircuitElement> ite = PassiveList.First;
		for (int i = 0; i < PassiveList.Count; i++)
		{
			CircuitElement ele = ite.Value;
			int lef = ele.leftNode - 1; 
			int rig = ele.rightNode - 1;
			int ohm = ele.temporaryResistance;

			if (lef > -1 && rig > -1)
			{
				//This part is for Conductance
				Gmatrix[lef , lef] += 1f/ohm;
				Gmatrix[rig , rig] += 1f/ohm;
				//This part is for Negative Conductance
				Gmatrix[lef , rig] -= 1f/ohm;
				Gmatrix[rig , lef] -= 1f/ohm;
			}
			else if (lef > -1)
			{
				//This part is for Conductance
				Gmatrix[lef , lef] += 1f/ohm;
			}
			else if (rig > -1)
			{
				//This part is for Conductance
				Gmatrix[rig , rig] += 1f/ohm;
			}
			else
			{
				//Short Circuit
				//abort
			}

			//Next passive element
			ite = ite.Next;
		}

		//This part Creates the Bmatrix from Voltage Soures relation to nodes
		ite = VoltageSourceList.First;
		for (int i = 0; i < VoltageSourceList.Count; i++)
		{
			CircuitElement source = ite.Value;
			int lef = source.leftNode - 1; 
			int rig = source.rightNode - 1;
			int volt = source.temporaryVoltage;

			if (lef > -1 && rig > -1)
			{
				//left side of the voltage is negative
				Bmatrix[lef , i] += -1f;
				Bmatrix[rig , i] += 1f;
			}
			else if (lef > -1)
			{
				Bmatrix[lef , i] += -1f;
			}
			else if (rig > -1)
			{
				Bmatrix[rig , i] += 1f;
			}
			else
			{
				//Short Circuit
				//abort
			}

			//Next voltage source
			ite = ite.Next;
		}

		//This part Creates the Cmatrix from Transpose of Bmatrix
		Cmatrix = MatrixOp.Transpose(Bmatrix);

		//Dmatrix is initialized as 0 mxm matrix which we will use as it is

		//Now we have all the sub matrixes let's compute Amatrix
		for (int i = 0; i < m+n; i++)
		{
			for (int j = 0; j < m+n; j++) 
			{
				
			}
		}
	}
}
