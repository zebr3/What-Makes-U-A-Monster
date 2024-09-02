using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    BoxCollider2D boxCollider;
    float boxColliderSpeed = -0.001f;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        boxCollider.offset = new Vector2(boxColliderSpeed, 0) + (Vector2)boxCollider.offset;

        //check position and flip sprite depending on that

        if (transform.position.x > 0)
        {
            
            transform.localScale = new Vector3(-1f, 1f, 1f);
           
           
        }
        else
        {
            // Reset the object's scale to its original state
            transform.localScale = new Vector3(1f, 1f, 1f);
           
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collect"))
        {
            InventoryManager.instance.addedItemName = collision.gameObject.GetComponent<PickupItems>().item.itemname;
            InventoryManager.instance.AddItem(collision.gameObject.GetComponent<PickupItems>().item);
            GameManager.instance.nameBox.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
