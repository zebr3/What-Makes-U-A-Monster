using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class XenoTriggerQuest : MonoBehaviour, IDropHandler
{
    InventoryItem droppedItem;
    float requiredItemId = 8;
    bool itemDropped;
    public GameObject dialogueTriggerObject;

    public void OnDrop(PointerEventData eventData)
    {

        // Check if the dropped item has the correct ID(mirror shard)
        droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (droppedItem != null && droppedItem.itemID == requiredItemId)
        {
            itemDropped = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = droppedItem.transform.position;


        }
    }

    void Update()
    {
        if (itemDropped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                itemDropped = false;
            }

            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);

            if (distance <= 1)
            {
                droppedItem.count--;

                dialogueTriggerObject.SetActive(true);

                itemDropped = false;
                
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = GameObject.FindWithTag("Player").transform.position;

                if (droppedItem.count <= 0)
                {
                    Destroy(droppedItem.gameObject);
                }
                else
                {
                    droppedItem.RefreshCounter();
                }

            }
        }
    }


}