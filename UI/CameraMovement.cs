using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform player; 
    [SerializeField] float smoothSpeed = 5f; 
    [SerializeField] float offsetX = 0f; 
    [SerializeField] float minX = 0f; 
    [SerializeField] float maxX = 10f; 

    private Vector3 targetPosition;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        // Calculate the target position for the camera
        float targetX = Mathf.Clamp(player.position.x + offsetX, minX, maxX);
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

        // Smoothly move the camera towards the target position
       transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
       
    }
}
