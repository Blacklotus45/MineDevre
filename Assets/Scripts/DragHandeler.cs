using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public enum ElementType
    {
    	Wire,
    	Resistance,
    	Battery,
    	Switch,
    	Lamp,
		Earthing,
		Connector
    }
	SpriteRenderer[] sprts ;
	SpriteRenderer[] sprtsNew ;
	Sprite spritePil; 

	Sprite spriteDirenc; 

	Sprite spriteLamba; 

	GameObject[] objs;
    //Trying to give id to elements
    static int id = 0; 

	int foundRes = 0;
	int foundBattery = 0;
	int foundLamb = 0;

	static public DragHandeler draggedItem;
    static public GameObject icon;
    static public GameObject[] CircuitElements;

	public ElementType typeOfItem;

	public delegate void DragEvent(DragHandeler item);
    static public event DragEvent OnItemDragStartEvent;                             // Drag start event

    public void OnBeginDrag(PointerEventData eventData)
    {
//        Debug.Log("OnBeginDrag");

        draggedItem = this;                                                         // Set as dragged item
        icon = new GameObject("Icon");                                              // Create object for item's icon
        Image image = icon.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;
        image.raycastTarget = false;                                                // Disable icon's raycast for correct drop handling
        RectTransform iconRect = icon.GetComponent<RectTransform>();
        // Set icon's dimensions
        iconRect.sizeDelta = new Vector2(   GetComponent<RectTransform>().sizeDelta.x,
                                            GetComponent<RectTransform>().sizeDelta.y);
        Canvas canvas = GetComponentInParent<Canvas>();                             // Get parent canvas
        if (canvas != null)
        {
            // Display on top of all GUI (in parent canvas)
            icon.transform.SetParent(canvas.transform, true);                       // Set canvas as parent
            icon.transform.SetAsLastSibling();                                      // Set as last child in canvas transform
        }
        if (OnItemDragStartEvent != null)
        {
            OnItemDragStartEvent(this);                                             // Notify all about item drag start
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
//        Debug.Log("OnDrag");

		if (icon != null)
        {
            icon.transform.position = Input.mousePosition;                          // Item's icon follows to cursor
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
//        Debug.Log("OnEndDrag");

		if (icon != null)
        {
            Destroy(icon);                                                          // Destroy icon on item drop
        }
//        MakeVisible(true);                                                          // Make item visible in cell
		if (!DestroyOnDropped.DestroyedFlag)
		{
        	GameObject go;
			scanScene ();
        	switch (draggedItem.typeOfItem) {
        		case ElementType.Wire:
        			go = Instantiate(ElementList.TheList.RcElements[0]);
					go.name = "Wire " + id;
					id++;
					break;
				case ElementType.Resistance:
					go = Instantiate(ElementList.TheList.RcElements[1]);
                    go.name = "Resistance " + id;
                    id++;
					if(foundRes == 1){
					sprtsNew = go.GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer srNew in sprtsNew ){
							if (srNew.tag == "Direnc") {
								srNew.sprite = spriteDirenc;
							}
						}
					}
                    break;
				case ElementType.Battery:
					go = Instantiate(ElementList.TheList.RcElements[2]);
                    go.name = "Battery " + id;
                    id++;
					if(foundBattery == 1){
					sprtsNew = go.GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer srNew in sprtsNew ){
							if (srNew.tag == "Pil") {
								srNew.sprite = spritePil;
							}
						}
					}
					break;
				case ElementType.Switch:
					go = Instantiate(ElementList.TheList.RcElements[3]);
					break;
				case ElementType.Lamp:
					go = Instantiate(ElementList.TheList.RcElements[4]);
					
					if(foundLamb == 1){
					sprtsNew = go.GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer srNew in sprtsNew ){
						if (srNew.tag == "Lamba") {
							srNew.sprite = spriteLamba;
						}
					}
					}
					break;
				case ElementType.Earthing:
					go = Instantiate(ElementList.TheList.RcElements[5]);
					break;
				case ElementType.Connector:
					go = Instantiate(ElementList.TheList.RcElements[6]);
					break;
        		default:
        			Debug.LogWarning("ElementType not initialized");
        			go = new GameObject();
        			break;
        	}

			go.transform.position = spawnPosition(go.transform.position.z);
        }
		DestroyOnDropped.DestroyedFlag = false;
        draggedItem = null;
        icon = null;
    }

    private Vector3 spawnPosition(float z)
    {
		float orthoSize = Camera.main.orthographicSize;
		float aspectRatio = Camera.main.aspect;
    	float verticalUnitperPixel = orthoSize*2 / Camera.main.pixelHeight;
		float horizontalUnitperPixel = (orthoSize*2 * aspectRatio) 
										/ Camera.main.pixelWidth;
		float spawnX = Input.mousePosition.x * horizontalUnitperPixel - (orthoSize * aspectRatio);
		float spawnY = Input.mousePosition.y * verticalUnitperPixel - orthoSize;
		return new Vector3(spawnX, spawnY, z);
    }

	void scanScene(){
		objs =  UnityEngine.Object.FindObjectsOfType<GameObject>() ;
		foundRes = 0;
		foundBattery = 0;
		foundLamb = 0;

		foreach (GameObject element in objs) {
			if (element.name.Contains ("Resistance") && (!element.name.Contains ("UI"))) {
				if(foundRes == 0){
					sprts = element.GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer sr in sprts ){
						if (sr.tag == "Direnc") {
							spriteDirenc = sr.sprite;
						}
					}
					foundRes = 1;
				}

			

			} else if (element.name.Contains ("Battery") && (!element.name.Contains ("UI"))) {
				if(foundBattery == 0){
					sprts = element.GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer sr in sprts ){
						if (sr.tag == "Pil") {
							spritePil = sr.sprite;
						}
					}
					foundBattery = 1;
				}


			} else if (element.name.Contains ("Lamp") && (!element.name.Contains ("UI"))) {
				if(foundLamb == 0){
					sprts = element.GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer sr in sprts ){
						if (sr.tag == "Lamba") {
							spriteLamba = sr.sprite;
						}
					}
					foundLamb = 1;
				}
			}
		}
	
	}

}
