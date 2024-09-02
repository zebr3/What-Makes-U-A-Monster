using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KanaQuest : MonoBehaviour, IDropHandler
{

    [SerializeField] GameObject[] tableObjects;
    [SerializeField] GameObject clothObject;
    [SerializeField] GameObject cutleryObject;
    [SerializeField] GameObject flashlightObject;
    [SerializeField] GameObject ceratioObject;
    int ceratioid = 24;
    int flashlight = 6;
    int cloth = 21;
    int cutlery = 2;


    int dropCount;
    bool itemDropped = false;

    public GameObject dialogueTriggerKana;
    public GameObject secondDialogueTriggerKana;
    InventoryItem droppedItem;
    public SecondDialogueTrigger kanaDialogueTrigger;
  

    void Start()
    {
        if (!GameManager.instance.activateKanaQuest)
        {
            gameObject.SetActive(false);

        }
        if (GameManager.instance.kanaCutleryActivated == true)
        {
            cutleryObject.SetActive(true);
        }
        if (GameManager.instance.kanaTableclothActivated == true)
        {
            clothObject.SetActive(true);
        }
        if (GameManager.instance.kanaFlashlightActivated == true)
        {
            flashlightObject.SetActive(true);
        }
        if(GameManager.instance.ceratioObjectActivated == true)
        {
            ceratioObject.SetActive(true);
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

            if (distance <= 1.5)
            {
                if(droppedItem.itemID != flashlight) 
                {
                    droppedItem.count--;
                }
                
                dropCount++;
                ActivateObject();
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


    public void OnDrop(PointerEventData eventData) 
    { 

        // Check if the dropped item has the correct ID(mirror shard)
        droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (droppedItem != null && (droppedItem.itemID == cloth || droppedItem.itemID == flashlight || droppedItem.itemID == cutlery ) || (droppedItem.itemID == ceratioid && GameManager.instance.allowCeratio == true) && dropCount < 4)
        {
            itemDropped = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = droppedItem.transform.position;
        }
    }

    void ActivateObject()
    {



        if (droppedItem.itemID == cutlery)
        {
            cutleryObject.SetActive(true);
            GameManager.instance.kanaCutleryActivated = true;

        }
        if (droppedItem.itemID == cloth)
        {
            clothObject.SetActive(true);
            GameManager.instance.kanaTableclothActivated = true;

        }
        if (droppedItem.itemID == flashlight)
        {
            tableObjects[2].SetActive(true);
            GameManager.instance.kanaFlashlightActivated = true;

        }
        if (droppedItem.itemID == ceratioid && GameManager.instance.allowCeratio)
        {
            ceratioObject.SetActive(true);
            secondDialogueTriggerKana.SetActive(true);
            GameManager.instance.ceratioObjectActivated = true;
            // tableObjects[3].SetActive(true);
        }

        if (tableObjects[3].activeInHierarchy)
        {
            secondDialogueTriggerKana.SetActive(true);
        }

        if (dropCount == 3)
        {
            dialogueTriggerKana.SetActive(true);
            gameObject.SetActive(true);
            kanaDialogueTrigger.enabled = true;
            GameManager.instance.kanaFlashlightActivated = false;
            flashlightObject.SetActive(false);
            GameManager.instance.allowCeratio = true;
        }

    }
}
