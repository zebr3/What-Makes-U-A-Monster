using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ceratio : MonoBehaviour, IDropHandler
{
    [SerializeField] int requiredItemId = 6;
    [SerializeField] Animator animator;

    bool itemDropped = false;

    InventoryItem droppedItem;
    Vector2 targetPosition = new Vector2(-1f, -1.15f);
    [SerializeField] GameObject pickupCeratio;
    


    void Update()
    {
        if (itemDropped)
        {
            //if registered click, disable itemdropped
            if (Input.GetMouseButtonDown(0))
            {
                itemDropped = false;
            }


            if ((Vector2)GameObject.FindWithTag("Player").transform.position == targetPosition)
            {

                StartCoroutine(ActivateAnimation());
                itemDropped = false;
                GameManager.instance.uiElementsToHide.SetActive(false);
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = false;


            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
       
        // Check if the dropped item has the correct ID(mirror shard)
        droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (droppedItem != null && droppedItem.itemID == requiredItemId)
        {
            itemDropped = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = targetPosition;
        }
    }

    public IEnumerator ActivateAnimation()
    {
        Debug.Log("start");
        GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("StartFlashlight");
        yield return new WaitForSeconds(2);
        GetComponentInParent<Animator>().SetTrigger("ceratioshake");
        yield return new WaitForSeconds(3.3f);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = true;
        GameManager.instance.uiElementsToHide.SetActive(true);
        pickupCeratio.SetActive(true);
        Destroy(transform.parent.parent.gameObject);
        
    }

   
}