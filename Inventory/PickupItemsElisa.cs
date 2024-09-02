using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PickupItemsElisa : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    [SerializeField] Item item;
    [SerializeField] GameObject currentObject;
    [SerializeField] GameObject objectAfterItem;


    //Checks
    bool itemClicked = false;
    float proximityThreshold = 1.5f;


    Vector2 targetPosition;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;
    bool isHovering = false;

    Collider2D thisCollider;





    void Start()
    {
        thisCollider = GetComponent<Collider2D>();

         GameManager.instance.nameText.raycastTarget = false;

        if(item.pickedUp)
        {
            objectAfterItem.SetActive(true);
            currentObject.SetActive(false);
            thisCollider.enabled = false;
        }
    }

    void Update()
    {


        if ((SceneManager.GetActiveScene().buildIndex == 9 || SceneManager.GetActiveScene().buildIndex == 16 || SceneManager.GetActiveScene().buildIndex == 24) && isHovering)
        {
            MoveNameBox();

        }

        //Happens when the item is pressed
        if (itemClicked)
        {
            //If after clicking on item mouse is pressed somewhere else do not pick up item
            if (Input.GetMouseButtonDown(0))
            {
                itemClicked = false;
            }

            //Change color of received item text field if item is permanent(not usable)
            if (item.usable == true)
            {


            }

            //Distance calculations for range to pick up item
            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);
            if (distance <= proximityThreshold && thisCollider.enabled == true)
            {
                InventoryManager.instance.addedItemName = item.itemname;
                GameManager.instance.nameBox.SetActive(false);
                InventoryManager.instance.AddItem(item);
                item.pickedUp = true;
                currentObject.SetActive(false);
                objectAfterItem.SetActive(true);
                thisCollider.enabled = false;
                

            }
        }

    }

    //Move namebox according to items
    void MoveNameBox()
    {
        targetPosition = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);
        GameManager.instance.nameBox.transform.position = targetPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {


        if ((Vector2)GameManager.instance.nameBox.transform.position != targetPosition)
        {
            MoveNameBox();
        }

        //When hovering over object, show namebox
        if (GameManager.instance.nameText != null)
        {
            GameManager.instance.nameText.text = item.itemname;
            GameManager.instance.nameBox.SetActive(true);

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        //When not hovering, deactivate namebox
        if (GameManager.instance.nameText != null)
        {
            GameManager.instance.nameBox.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        itemClicked = true;
    }
}
