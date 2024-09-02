using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collect"))
        {
            InventoryManager.instance.addedItemName = collision.gameObject.GetComponent<PickupItems>().item.itemname;
            InventoryManager.instance.AddItem(collision.gameObject.GetComponent<PickupItems>().item);
            Destroy(collision.gameObject);

        }
    }

   
}
