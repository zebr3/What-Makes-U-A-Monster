using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dust : MonoBehaviour, IDropHandler
{
    [SerializeField] int requiredItemId = 22;

    bool itemDropped = false;

    InventoryItem droppedItem;
    

    void Update()
    {
        
        if (itemDropped)
        {

            //disable drop if a click is registered
            if (Input.GetMouseButtonDown(0))
            {
                itemDropped = false;
            }

            //calculate distance, if close enough start things
            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);

            if (distance <= 0.7f)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = GameObject.FindWithTag("Player").transform.position;
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = false;
                StartCoroutine(DestroyDust());



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
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = droppedItem.transform.position;
        }
    }

    //Coroutine for destroying object
    IEnumerator DestroyDust()
    {
        yield return new WaitForSeconds(1);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = true;
        GetComponent<Animator>().SetTrigger("sparkletrigger");
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
        
    }

}