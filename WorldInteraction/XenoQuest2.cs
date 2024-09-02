using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XenoQuest2 : MonoBehaviour
{
    [SerializeField] GameObject teacher;
    [SerializeField] InteractableItems interactableItems;
    [SerializeField] PickupItemsElisa pickupItemsElisa;


    void Start()
    {
        if (GameManager.instance.xenoQuest)
        {
            teacher.SetActive(false);
            interactableItems.enabled = false;
            pickupItemsElisa.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
