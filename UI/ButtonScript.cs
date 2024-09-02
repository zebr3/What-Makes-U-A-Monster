using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public void StopPlayerMovement()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = false;
    }

    public void AllowPlayerMovement()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = true;
       

    }

    public void ActivateOnNo()
    {
        GameManager.instance.objectToDeactivate.SetActive(true);
    }

}
