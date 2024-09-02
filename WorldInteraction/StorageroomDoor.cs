using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageroomDoor : MonoBehaviour, IPointerClickHandler
{
    public GameObject closedBg;
    public GameObject openBg;
   // [SerializeField] GameObject mirrorFilter;
    public bool closed = true;
    bool clicked = false;
    


    void Update()
    {
        if(clicked)
        {
            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);

            if (distance <= 2)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = GameObject.FindWithTag("Player").transform.position;

                if (!closed)
                {
                    clicked = false;
                    closedBg.SetActive(false);
                    openBg.SetActive(true);
                    //endscreen.SetActive(true);
                    closed = true;
                }
                else
                {
                    closedBg.SetActive(true);
                    openBg.SetActive(false);
                    closed = false;
                    clicked = false;
                }
            }

           
        }

       
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        clicked = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = transform.position;

      
    }

}
