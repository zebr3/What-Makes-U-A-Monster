using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyStuff : MonoBehaviour, IDropHandler
{
    [SerializeField] int requiredItemId = 0;

    Animator animator;

    Animator playerAnimator;
    bool itemDropped = false;
    bool allowAnimation = true;
    float distanceForActivation = 1f;




    public void Start()
    {
        animator = GetComponent<Animator>();

    }
    public void OnDrop(PointerEventData eventData)
    {
        // Check if the dropped item has the correct ID(mirror shard)
        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (droppedItem != null && droppedItem.itemID == requiredItemId)
        {
            itemDropped = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = eventData.pointerDrag.transform.position;
            
           

        }
    }

    void Update()
    {
        if(itemDropped)
        {
            if(Input.GetMouseButton(0))
            {
                itemDropped = false;
            }
            
            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);
            if (distance <= distanceForActivation && allowAnimation)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = false;
                animator.SetTrigger("Destroy");
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("StartBaseballSwing");
                allowAnimation = false;


            }
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}

   
