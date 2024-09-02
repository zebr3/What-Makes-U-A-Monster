using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mirror : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField] int requiredItemId = 15;
    [SerializeField] GameObject[] mirrors;
    [SerializeField] GameObject letter;
    Camera mainCamera;
    float cameraSizeRate = 0.0007f;

    Animator fade;
    [SerializeField] GameObject mirrorFilter;
    [SerializeField] Collider2D doorCollider;
    GameObject buttons;
    public GameObject sceneTransition;
    public bool allowSwitch;
    int dropCount;
    bool allowClick = true;
    bool itemDropped = false;
    InventoryItem droppedItem;

    float distanceToTrigger = 1f;
    bool clicked = false;

    public void Start()
    {
        fade = GameObject.FindWithTag("Fade").GetComponent<Animator>();
        mainCamera = Camera.main;
        buttons = GameObject.FindWithTag("HideOnDialogue");
    }

    void Update()
    {
        //method to call when mirror is full and clicked
        if (clicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clicked = false;
            }

            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);


            //if all criterias match, start coroutine to switch worlds
            if (mirrors[4].activeSelf && allowClick && distance <= distanceToTrigger)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = false;
                // sceneTransition.SetActive(true);
                StartCoroutine(WorldSwitch());
                //allowClick = false;
                clicked = false;
                buttons.SetActive(false);


            }
        }

        if (itemDropped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clicked = false;
            }

            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);

            if (distance <= 2)
            {
                droppedItem.count--;
                ActivateNextMirror();
               


                if (droppedItem.count <= 0)
                {
                    Destroy(droppedItem.gameObject);
                }
                else
                {
                    droppedItem.RefreshCounter();
                }
                itemDropped = false;
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        // Check if the dropped item has the correct ID(mirror shard)
        droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (droppedItem != null && droppedItem.itemID == requiredItemId && mirrors[4].activeSelf == false)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = droppedItem.transform.position;
            itemDropped = true;
        }
    }

    //Switch worlds
    public void OnPointerClick(PointerEventData eventData)
    {

        if (allowClick)
        {
            clicked = true;
        }
    }

    //Camera and Fade for mirror world
    IEnumerator WorldSwitch()
    {
        if (allowClick)
        {
            allowClick = false;
            GameObject.FindWithTag("Fade").GetComponent<Image>().color = Color.white;
            Vector3 initialPosition = mainCamera.transform.position;
            Vector3 targetPosition = transform.position;
            float cameraSpeed = 0.1f;

            //Zoom onto mirror
            while (mainCamera.orthographicSize > 2.1f)
            {
                if (mainCamera.orthographicSize < 2.5f)
                {
                    fade.SetBool("StartFade", true);
                    fade.SetBool("EndFade", false);
                }

                mainCamera.orthographicSize -= cameraSizeRate;
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraSpeed * Time.deltaTime);
                yield return null;
            }

            //Revert to original settings
            mainCamera.transform.position = initialPosition;
            mirrorFilter.SetActive(!mirrorFilter.activeSelf);

            //Mirrow world mechanik(switch)
            if (mirrorFilter.activeSelf)
            {
                if (letter != null)
                {
                    letter.SetActive(true);
                }
            }
            else
            {
                if (letter != null)
                {
                    letter.SetActive(false);
                }
            }

            if (doorCollider.enabled)
            {
                doorCollider.enabled = false;
            }
            else
            {
                doorCollider.enabled = true;
            }

            if (doorCollider.gameObject.GetComponent<StorageroomDoor>().closedBg.activeSelf)
            {
                doorCollider.gameObject.GetComponent<StorageroomDoor>().closedBg.SetActive(false);
                doorCollider.gameObject.GetComponent<StorageroomDoor>().openBg.SetActive(true);
            }
            else
            {
                doorCollider.gameObject.GetComponent<StorageroomDoor>().closedBg.SetActive(true);
                doorCollider.gameObject.GetComponent<StorageroomDoor>().openBg.SetActive(false);
            }

            while (mainCamera.orthographicSize < 2.8f)
            {
                mainCamera.orthographicSize += cameraSizeRate;

                if (mainCamera.orthographicSize > 2.5)
                {

                    fade.SetBool("StartFade", false);
                    fade.SetBool("EndFade", true);
                }
                yield return null;
            }

            buttons.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = true;
            GameObject.FindWithTag("Fade").GetComponent<Image>().color = Color.black;
            allowClick = true;

        }
    }

        void ActivateNextMirror()
        {
            if (dropCount < mirrors.Length - 1)
            {
                mirrors[dropCount].SetActive(false);
                dropCount++;
                mirrors[dropCount].SetActive(true);
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = GameObject.FindWithTag("Player").transform.position;
            }

            if (dropCount == 3)
            {
                requiredItemId = 14;
            }
        }
    
}
