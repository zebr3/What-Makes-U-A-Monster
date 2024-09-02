using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField] Item item;
    public bool itemClicked = false;
    float speed = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(itemClicked)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameObject.Find("Player").transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            InventoryManager.instance.addedItemName = item.itemname;
            InventoryManager.instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}
