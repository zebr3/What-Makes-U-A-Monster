using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public int index;
    public Color selectedColor, notSelectedColor;
    
    

  
    //if childcount of the slot is 0, set parentafterdrag(next parent) to this slot
    public void OnDrop(PointerEventData eventData)
    {
       if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
            AudioManager.instance.PlaySFX("InventarSocketSound");
            

        }
        
    }

    //change color of slot when hovering over
    public void OnPointerEnter(PointerEventData eventData)
    {

        image.color = selectedColor;
    }
    
        
        
    
    //change color back to normal
    public void OnPointerExit(PointerEventData eventData)
    {
        
        image.color = notSelectedColor;

    }
}
