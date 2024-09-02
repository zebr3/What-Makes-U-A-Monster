using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InteractableItems : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public Item item;

    DescriptionTextWriter descriptionTextWriter;

    Vector2 targetPosition;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;
    bool isHovering = false;

    [SerializeField] float distanceForInteraction = 2f;
    bool clicked = false;



    void Start()
    {

        GameManager.instance.descriptionText.text = item.description;

        GameManager.instance.nameText.raycastTarget = false;
    }


    void Update()
    {
        if (clicked)
        {
            //When item is clicked and you click somewhere else, text does not appear anymore
            if (Input.GetMouseButtonDown(0))
            {
                clicked = false;
            }

            //distance check, if player is close enough start description
            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").GetComponent<Transform>().position);


            if (distance <= distanceForInteraction)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = false;
                GameManager.instance.descriptionText.GetComponent<DescriptionTextWriter>().StartDescription(item.description);
                GameManager.instance.nameBox.SetActive(false);
                clicked = false;
            }
        }

        //constantly update name in the rooms where camera is following player
        if ((SceneManager.GetActiveScene().name == "13.0LeftHallway" || SceneManager.GetActiveScene().name == "13.8Library") && isHovering)
        {
            MoveNameBox();

        }
    }


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
        if (GameManager.instance.nameText != null && GameManager.instance.descriptionBox.activeInHierarchy == false)
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
        clicked = true;



    }
}
