using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToolbarController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] RectTransform toolbar;
    [SerializeField] RectTransform arrow;
    [SerializeField] float moveDistance = 0.55f;
    [SerializeField] float speed = 5f;

    [SerializeField] GameObject inventoryPage;
    Image image;
    Collider2D collider2d;

    Vector3 initialPosition;
    Vector3 targetPosition;

    Vector3 toolbarInitialPosition;
    Vector3 toolbarTargetPosition;
    bool toolbarOpen = false;

    private void Start()
    {
        // Get Components
        image = GetComponent<Image>();
        collider2d = GetComponent<Collider2D>();

        //Arrow positions
        initialPosition = arrow.anchoredPosition;
        targetPosition = initialPosition + Vector3.up * moveDistance;

        //Toolbar positions
        toolbarInitialPosition = toolbar.anchoredPosition;
        toolbarTargetPosition = toolbarInitialPosition + Vector3.up * moveDistance;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Close toolbar when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (toolbarOpen)
        {
            toolbarOpen = false;
            FlipObject();
        }
    }

    private void Update()
    {
        //if arrow was pressed or inventory is open, get whole toolbar up, else let it down
        if (toolbarOpen || inventoryPage.activeInHierarchy)
        {
            toolbar.anchoredPosition = Vector3.MoveTowards(toolbar.anchoredPosition, toolbarTargetPosition, speed * Time.deltaTime);
            arrow.anchoredPosition = Vector3.MoveTowards(arrow.anchoredPosition, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            toolbar.anchoredPosition = Vector3.MoveTowards(toolbar.anchoredPosition, toolbarInitialPosition, speed * Time.deltaTime);
            arrow.anchoredPosition = Vector3.MoveTowards(arrow.anchoredPosition, initialPosition, speed * Time.deltaTime);
        }

        //while inventory page is active, disable the arrow
        if (inventoryPage.activeInHierarchy)
        {
            image.enabled = false;
            collider2d.enabled = false;
        }
        else
        {
            image.enabled = true;
            collider2d.enabled = true;
        }
    }

    //flip arrow object 
    private void FlipObject()
    {
       
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }



    public void OnPointerClick(PointerEventData eventData)
    {

        toolbarOpen = !toolbarOpen;
        FlipObject();


        /* falls wir wollen dass wenn die toolbar aktiv ist der spieler sich nicht bewegen darf (noch abzusprechen)
        if (toolbarOpen)
        {
            playerMovement.DisallowMovement();
        }
        else
        {
            playerMovement.AllowMovement();
        }
        */
    }


}
