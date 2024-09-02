using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommanderAbility : MonoBehaviour
{
    public bool abilitySelected = false;
    float speed = 3f;
    bool itemClicked = true;
    GameObject item;
    
   


    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        //is ability is selected and right mouse button is pressed, check if on mouseposition item has collect tag, if yes then move item towards player
        if (Input.GetMouseButtonDown(1) && abilitySelected) 
        {
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            
            if (hit.collider != null && hit.collider.CompareTag("Collect"))
            {
                itemClicked = true;
                item = hit.collider.gameObject;
               
            }
        }

       if(itemClicked)
        {
            item.transform.position = Vector2.MoveTowards(item.transform.position, GameObject.FindWithTag("Player").transform.position, speed * Time.deltaTime);
        }
      
    }


    public void Activate()
    {
        
    }

   
}
