using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    [SerializeField] int requiredItemId; 
    

    public void OnDrop(PointerEventData eventData)
    {
        // Check if the dropped item has the correct ID
        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (droppedItem != null && droppedItem.itemID == requiredItemId)
        {
           
            droppedItem.count--;

            if(droppedItem.count <= 0)
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
