using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PickupItems : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public Item item;


    //Checks
    bool itemClicked = false;
    [SerializeField] float distanceToPickup = 1.5f;


    Vector2 targetPosition;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;
    bool isHovering = false;

    bool commanderActivate;
    float speed = 1.5f;



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


        if ((SceneManager.GetActiveScene().name == "13.0LeftHallway" || SceneManager.GetActiveScene().name == "13.8Library") && isHovering)
        {
            MoveNameBox();

        }

        //Happens when the item is pressed
        if (itemClicked && !commanderActivate)
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

        //complete commander ability section
        if (commanderActivate)
        {
            Vector3 previousPosition = transform.position;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindWithTag("Player").transform.position, speed * Time.deltaTime);

            float velocityX = transform.position.x - previousPosition.x;

            if (velocityX < 0)
            {
                transform.GetChild(0).gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (velocityX >= 0)
            {
                transform.GetChild(0).gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }

            previousPosition = transform.position;

            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);
            if (distance <= 0.3f)
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
        isHovering = true;

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
        isHovering = false;
        //When not hovering, deactivate namebox
        if (GameManager.instance.nameText != null)
        {
            GameManager.instance.nameBox.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        itemClicked = true;

        if(GameObject.FindWithTag("Player").GetComponent<CommanderAbility>().abilitySelected && eventData.button == PointerEventData.InputButton.Right && transform.childCount == 1)
        {
            commanderActivate = true;
        }
    }
}
