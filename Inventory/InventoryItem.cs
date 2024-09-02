using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;


public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [Header("Camera")]
    private RectTransform itemRectTransform;
    private Vector3 offset;
    private Camera mainCamera;

    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;

    float xOffset;
    float yOffset = 0.3f;
    Vector3 targetPosition;

    GameObject infoButton;
    Transform currentParent;



    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Item item;
    bool isHovering;
    public int itemID;



    public void Start()
    {
        mainCamera = Camera.main;
        itemRectTransform = GetComponent<RectTransform>();
        countText.gameObject.SetActive(false);

        infoButton = GetComponentInChildren<InfoButton>().gameObject;
        infoButton.SetActive(false);

        parentAfterDrag = transform.parent;



    }

    void Update()
    {
        if(isHovering)
        {
            MoveNameBox();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EndDrag();
        mainCamera = Camera.main;

    }

    void MoveNameBox()
    {
        targetPosition = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);
        InventoryManager.instance.nameBox.transform.position = targetPosition;
    }

    //Method to update the stack value of an item
    public void RefreshCounter()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    //methode to initialise item(get all parameters on the copy prefab)
    public void InitialiseItem(Item newitem)
    {
        item = newitem;
        image.sprite = newitem.Image;
        itemID = item.itemid;

    }

    public Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        // Get the mouse/touch position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen space position to world space using the camera's ScreenToWorldPoint method
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Set the z position to 0 to ensure it's on the same plane as the UI
        worldPosition.z = 0f;

        return worldPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            // Return if the button pressed is not the left mouse button
            return;
        }

        infoButton.SetActive(false);

        // Store the initial offset between the item's position and the mouse/touch position
        offset = itemRectTransform.position - GetMouseWorldPosition();

        //disable raycast and parent transformation
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;

        transform.SetParent(transform.root);




    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            // Return if the button pressed is not the left mouse button
            return;
        }

        if (!image.raycastTarget)
        {

            // Update the item's position based on the mouse/touch position
            itemRectTransform.position = GetMouseWorldPosition() + offset;
        }
        InventoryManager.instance.nameBox.SetActive(false);

    }
    //What actually happens when you end the drag
    public void EndDrag()
    {

        //enable raycast and parent transformation 
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        if (GetComponentInParent<InventorySlot>().index > 6)
        {
            infoButton.SetActive(true);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            // Return if the button pressed is not the left mouse button
            return;
        }

        EndDrag();




    }

    private bool isNameBoxActive = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        if (!isNameBoxActive)
        {
            isNameBoxActive = true;
            InventoryManager.instance.nameBox.SetActive(true);
            InventoryManager.instance.nameText.text = item.itemname;
            MoveNameBox();

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isNameBoxActive = false;
        InventoryManager.instance.nameBox.SetActive(false);
        isHovering = false;
    }

    public void OnDrop(PointerEventData eventData)
    {

        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        Transform thisParent = this.gameObject.transform.parent;


        if (droppedItem.gameObject.GetComponent<InventoryItem>().item.combinationID == item.combinationID && item.allowCombination == true)
        {
            int slotIndex = GetComponentInParent<InventorySlot>().index;


            Destroy(gameObject);
            Destroy(droppedItem.gameObject);
            InventoryManager.instance.SpawnNewItem(InventoryManager.instance.CombinationItems[item.combinationID - 1], InventoryManager.instance.inventorySlots[slotIndex]);
            AudioManager.instance.PlaySFX("GeilesKlonk");

        }
        else
        {
            gameObject.transform.SetParent(droppedItem.parentAfterDrag);
            parentAfterDrag = transform.parent;
            droppedItem.parentAfterDrag = thisParent;
            AudioManager.instance.PlaySFX("InventarSocketSound");





        }

    }
}
