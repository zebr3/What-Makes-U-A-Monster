
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModifiedToolbarController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject toolbar;
    [SerializeField] GameObject toolbarArrow;
    [SerializeField] float moveDistance = 0.55f;
    [SerializeField] float speed = 5f;
    [SerializeField] private PlayerMovementMouse playerMovement;

    [SerializeField] GameObject inventoryPage;
    Image image;
    Collider2D collider2d;

    Vector3 initialPosition;
    Vector3 targetPosition;

    bool toolbarOpen = false;

    private RectTransform canvasRectTransform;
    private Camera mainCamera;

    void Start()
    {
        // Get Components
        image = GetComponent<Image>();
        collider2d = GetComponent<Collider2D>();

        // Save the original position
        initialPosition = transform.position;

        // Set the target position
        targetPosition = initialPosition + Vector3.up * moveDistance;

        canvasRectTransform = GetComponentInParent<RectTransform>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (toolbarOpen || inventoryPage.activeInHierarchy)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
        }

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

    private void LateUpdate()
    {
        if (canvasRectTransform != null && mainCamera != null)
        {
            // Set the position of the toolbar UI to follow the player's x-coordinate while maintaining the camera's y-coordinate
            Vector3 playerPosition = playerMovement.transform.position;
            Vector3 cameraPosition = mainCamera.transform.position;

            // Convert the player's world position to screen coordinates
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(playerPosition);

            // Convert the screen coordinates to canvas local position
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, mainCamera, out canvasPosition);

            // Set the toolbar position to the converted canvas local position
            Vector3 toolbarPosition = new Vector3(canvasPosition.x, toolbar.transform.localPosition.y, toolbar.transform.localPosition.z);
            toolbar.transform.localPosition = toolbarPosition;

            // Calculate the distance between the toolbar's y-position and the bottom of the screen
            float toolbarBottomDistance = toolbar.transform.localPosition.y - canvasRectTransform.rect.height * 0.5f;

            // Calculate the target position of the toolbar
            Vector3 toolbarTargetPosition = toolbar.transform.localPosition;

            if (toolbarOpen && toolbarBottomDistance < 0f)
            {
                // If the toolbar is open and it is below the bottom of the screen, move it up
                toolbarTargetPosition.y -= toolbarBottomDistance;
            }

            // Smoothly move the toolbar towards the target position
            toolbar.transform.localPosition = Vector3.Lerp(toolbar.transform.localPosition, toolbarTargetPosition, speed * Time.deltaTime);

            // Set the toolbar arrow position to the middle of the toolbar on the x-axis
            Vector3 toolbarArrowPosition = new Vector3(toolbar.transform.localPosition.x, toolbarArrow.transform.localPosition.y, toolbarArrow.transform.localPosition.z);
            toolbarArrow.transform.localPosition = toolbarArrowPosition;

            // Align the toolbar with the camera's rotation
            toolbar.transform.rotation = Quaternion.Euler(0f, 0f, mainCamera.transform.rotation.eulerAngles.z);

        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        toolbarOpen = !toolbarOpen;

        FlipObject();

        if (toolbarOpen)
        {
            // Move the toolbar up in the y-axis
            toolbar.transform.position += Vector3.up * moveDistance;
        }
        else
        {
            // Move the toolbar back down to its initial position
            toolbar.transform.position -= Vector3.up * moveDistance;
        }

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

    private void FlipObject()
    {
        // Get the current scale of the object
        Vector3 scale = transform.localScale;

        // Flip object on the y axis
        scale.y *= -1;

        // Apply the new scale to the object
        transform.localScale = scale;
    }
}