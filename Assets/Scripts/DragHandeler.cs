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
		Earthing
    }

	static public DragHandeler draggedItem;
    static public GameObject icon;
    static public GameObject[] CircuitElements;

	public ElementType typeOfItem;

	public delegate void DragEvent(DragHandeler item);
    static public event DragEvent OnItemDragStartEvent;                             // Drag start event

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

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
        Debug.Log("OnEndDrag");

		if (icon != null)
        {
            Destroy(icon);                                                          // Destroy icon on item drop
        }
//        MakeVisible(true);                                                          // Make item visible in cell
		if (!DestroyOnDropped.DestroyedFlag)
		{
        	GameObject go;
        	switch (draggedItem.typeOfItem) {
        		case ElementType.Wire:
        			go = Instantiate(ElementList.TheList.RcElements[0]);
					break;
				case ElementType.Resistance:
					go = Instantiate(ElementList.TheList.RcElements[1]);
					break;
				case ElementType.Battery:
					go = Instantiate(ElementList.TheList.RcElements[2]);
					break;
				case ElementType.Switch:
					go = Instantiate(ElementList.TheList.RcElements[3]);
					break;
				case ElementType.Lamp:
					go = Instantiate(ElementList.TheList.RcElements[4]);
					break;
				case ElementType.Earthing:
					go = Instantiate(ElementList.TheList.RcElements[5]);
				break;
        		default:
        			Debug.LogWarning("ElementType not initialized");
        			go = new GameObject();
        			break;
        	}

			go.transform.position = spawnPosition();
        }
		DestroyOnDropped.DestroyedFlag = false;
        draggedItem = null;
        icon = null;
    }

    private Vector3 spawnPosition()
    {
		float orthoSize = Camera.main.orthographicSize;
		float aspectRatio = Camera.main.aspect;
    	float verticalUnitperPixel = orthoSize*2 / Camera.main.pixelHeight;
		float horizontalUnitperPixel = (orthoSize*2 * aspectRatio) 
										/ Camera.main.pixelWidth;
		float spawnX = Input.mousePosition.x * horizontalUnitperPixel - (orthoSize * aspectRatio);
		float spawnY = Input.mousePosition.y * verticalUnitperPixel - orthoSize;
		return new Vector3(spawnX, spawnY, 20);
    }

}
