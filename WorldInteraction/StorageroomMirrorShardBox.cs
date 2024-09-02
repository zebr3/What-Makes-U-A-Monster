using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageroomMirrorShardBox : MonoBehaviour, IPointerClickHandler
{
    bool itemClicked;
    [SerializeField] GameObject closedBox;
    [SerializeField] GameObject openedBox;
    [SerializeField] GameObject secretDialogueTrigger;


    public void OnPointerClick(PointerEventData eventData)
    {
        itemClicked = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(itemClicked && closedBox.activeInHierarchy)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = transform.position;

            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);

            if(distance <= 2)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = GameObject.FindWithTag("Player").transform.position;
                
                secretDialogueTrigger.SetActive(true);
                openedBox.SetActive(true);
                closedBox.SetActive(false);
                AudioManager.instance.PlayMusic("MisteryMusic");
                itemClicked = false;
            }
        }

       
    }
}
