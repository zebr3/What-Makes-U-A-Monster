using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerActivate : MonoBehaviour
{
    public GameObject darkBox;
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public PlayerMovementMouse playerMovement;

    private void Start()
    {


        objectToDeactivate = GameManager.instance.objectToDeactivate;
        //playerMovement = GameManager.instance.playerMovement;

    }
    public void Update()
    {
        if (objectToActivate.activeSelf) { playerMovement.movementAllowed = false; }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            objectToActivate.SetActive(true);
            darkBox.SetActive(true);
            objectToDeactivate.SetActive(false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            objectToActivate.SetActive(false);
            darkBox.SetActive(false);
            objectToDeactivate.SetActive(true);

        }
    }
}
