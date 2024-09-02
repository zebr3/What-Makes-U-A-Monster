using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class test : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    //Method to really add item to inventory
    public void Pickupitem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);

    }

}