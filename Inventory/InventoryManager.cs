using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Item[] startItems;
    public Item[] CombinationItems;
    
    public InventorySlot[] inventorySlots;
    [SerializeField] GameObject inventoryItemPrefab;
    public int maxStack = 6;

    
    public GameObject nameBox;
    public TextMeshProUGUI nameText;
    public Item slimeItem;
    public Item toothBrushItem;
    public Item loveLetterItem;

    public GameObject AnnouncementLine;
    public TextMeshProUGUI pickupText;
    public string addedItemName;

    void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        foreach(var Item in startItems)
        {
            AddItem(Item);
        }

       

    }

    public void ShowPickupLine()
    {
        if (AnnouncementLine.activeSelf)
        {
            pickupText.text = "You received " + addedItemName;
            AnnouncementLine.GetComponent<PickupLineStretch>().ResetLine();
            
        }
        else
        {
            pickupText.text = "You received " + addedItemName;
            AnnouncementLine.SetActive(true);
            

        }
    }

    public void ShowLine(string text)
    {
        if (AnnouncementLine.activeSelf)
        {
            pickupText.text = text;
            AnnouncementLine.GetComponent<PickupLineStretch>().ResetLine();

        }
        else
        {
            pickupText.text = text;
            AnnouncementLine.SetActive(true);


        }
    }


    //Search for slot with same item
    public bool AddItem(Item item)
    {
        //Search for already existing item in inventory to add to stack
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStack && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCounter();
                ShowPickupLine();
                return true;
            }
            
        }

        
    

   
        //Search for empty Slot and add new item to inventory
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                ShowPickupLine();
                return true;
            }
        }

        return false;
    }

    public bool AddSlimeItem()
    {
        //Search for already existing item in inventory to add to stack
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            addedItemName = slimeItem.itemname; 
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == slimeItem && itemInSlot.count < maxStack && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCounter();
               
                ShowPickupLine();
                return true;
            }

        }

        //Search for empty Slot and add new item to inventory
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(slimeItem, slot);
                ShowPickupLine();
                return true;
            }
        }

        return false;
    }

    public bool AddToothbrush()
    {
        //Search for already existing item in inventory to add to stack
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            addedItemName = toothBrushItem.itemname;
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == toothBrushItem && itemInSlot.count < maxStack && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCounter();

                ShowPickupLine();
                return true;
            }

        }

        //Search for empty Slot and add new item to inventory
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(toothBrushItem, slot);
                ShowPickupLine();
                return true;
            }
        }

        return false;
    }

    public bool AddLoveLetterItem()
    {
        //Search for already existing item in inventory to add to stack
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            addedItemName = loveLetterItem.itemname;
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == loveLetterItem && itemInSlot.count < maxStack && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCounter();

                ShowPickupLine();
                return true;
            }

        }

        //Search for empty Slot and add new item to inventory
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(loveLetterItem, slot);
                ShowPickupLine();
                return true;
            }
        }

        return false;
    }

    public bool CheckIfItemInInventory(int itemID)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item.itemid == itemID)
            {
                Debug.Log("Yes it is");
                return true;
            }
        }

        return false;
    }


    //Spawn Item on the slot
    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);

    }

   

    
}
