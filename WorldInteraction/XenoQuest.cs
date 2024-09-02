using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XenoQuest : MonoBehaviour
{
    bool questTriggered = false;
    [SerializeField] InteractableItems inter1;
    [SerializeField] InteractableItems inter2;
    [SerializeField] InteractableItems inter3;
    [SerializeField] InteractableItems inter4;
    [SerializeField] DestroyStuff destroyStuff1;
    [SerializeField] DestroyStuff destroyStuff2;
    [SerializeField] DestroyStuff destroyStuff3;
    [SerializeField] DestroyStuff destroyStuff4;
    [SerializeField] GameObject collider;
    [SerializeField] GameObject exits;

    [SerializeField] GameObject firstDialogue;
    [SerializeField] GameObject secondDialogue;
    [SerializeField] GameObject lastDialogue;

    public GameObject Greeneye;
    void Start()
    {
        if(GameManager.instance.activateXenoQuest)
        {
            inter1.enabled = true;
            inter2.enabled = true;
            inter3.enabled = true;
            inter4.enabled = true;
            destroyStuff1.enabled = true;
            destroyStuff2.enabled = true;
            destroyStuff3.enabled = true;
            destroyStuff4.enabled = true;
            collider.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.xenoQuest)
        {
            if (transform.childCount == 4)
            {
                firstDialogue.SetActive(true);
                exits.SetActive(true);
            }
            if (transform.childCount == 2)
            {
                secondDialogue.SetActive(true);
            }
            if (transform.childCount == 1)
            {
                lastDialogue.SetActive(true);
                exits.SetActive(false);

            }
        }
       
    }
}
