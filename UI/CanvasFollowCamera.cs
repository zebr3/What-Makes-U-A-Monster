using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour 
{

    public Transform playerTransform;
    private RectTransform canvasRectTransform;
    private Camera mainCamera;

    private void Start()
    {
        canvasRectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (playerTransform != null && canvasRectTransform != null && mainCamera != null)
        {
            // Get the player's position in world space
            Vector3 playerPosition = playerTransform.position;

            // Convert the player's world position to screen coordinates
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(playerPosition);

            // Convert the screen coordinates to canvas local position
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, mainCamera, out canvasPosition);

            // Set the canvas position to the converted canvas local position
            canvasRectTransform.anchoredPosition = canvasPosition;

            // Align the canvas with the camera's rotation
            canvasRectTransform.rotation = Quaternion.Euler(0f, 0f, mainCamera.transform.rotation.eulerAngles.z);
        }
    }
}
