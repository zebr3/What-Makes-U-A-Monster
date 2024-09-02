using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PickupGlove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    [SerializeField] Item item;
    


    //Checks
    bool itemClicked = false;
    [SerializeField] float distanceToPickup = 1.5f;


    Vector2 targetPosition;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;
    bool isHovering = false;





    void Start()
    {
        if(item.pickedUp)
        {
            Destroy(gameObject);
        }

        // itemNameText.raycastTarget = false;
    }

    void Update()
    {


        if ((SceneManager.GetActiveScene().buildIndex == 8 || SceneManager.GetActiveScene().buildIndex == 9) && isHovering)
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
            if (distance <= distanceToPickup)
            {
                InventoryManager.instance.addedItemName = item.itemname;
                GameManager.instance.nameBox.SetActive(false);
                InventoryManager.instance.AddItem(item);
                item.pickedUp = true;
                Destroy(gameObject);
               
                

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
            GameManager.instance.nameText.text = "Stuffed Wardrobe";
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
