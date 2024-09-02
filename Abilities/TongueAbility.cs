using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueAbility : MonoBehaviour
{
    [SerializeField] GameObject tonguePrefab; // GameObject als Zunge
    [SerializeField] float tongueSpeed = 2f; // Geschwindigkeit, mit der sich die Zunge bewegt
    [SerializeField] float maxTongueLength = 10f; // Maximale Länge der Zunge
    [SerializeField] float tongueRetractSpeed = 1f; // Geschwindigkeit, mit der sich die Zunge zurückzieht

    bool isTongueActive = false; // Ist die Fähigkeit aktiviert?
    Vector2 targetPosition; // Zielposition, an der die Zunge landen soll
    GameObject currentTongue; // Das aktuelle Zungen-GameObject
    Vector2 tongueStartPoint; // Startpunkt der Zunge

    float currentYScale = 0f; // Aktuelle Y-Skala der Zunge
    float targetYScale = 0f; // Ziel-Y-Skala der Zunge
    bool isRetracting = false; // Gibt an, ob die Zunge zurückgezogen wird

   

    public bool abilitySelected = false;


    void Update()
    {
        
        if(currentTongue != null)
        {
            currentTongue.transform.position = transform.position;
        }
        //activate ability with right click
        if (Input.GetMouseButtonDown(1) && !isTongueActive && !isRetracting && abilitySelected && GameManager.instance.allowAbility)
        {
            isTongueActive = true;
            currentTongue = Instantiate(tonguePrefab, (Vector2)transform.position + new Vector2(0, 4f), Quaternion.identity);
            tongueStartPoint = transform.position; // Setze den Startpunkt der Zunge auf die Position des Charakters
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Set the target Y scale based on the distance to the target position
            float distanceToTarget = Vector2.Distance(currentTongue.transform.position, targetPosition);
            targetYScale = Mathf.Clamp(distanceToTarget, 0f, maxTongueLength);
        }

        if (isTongueActive && !isRetracting)
        {
            // Rotate the tongue towards the target position
            currentTongue.transform.up = targetPosition - (Vector2)currentTongue.transform.position;

            // Update the current Y scale based on the tongue speed
            currentYScale = Mathf.MoveTowards(currentYScale, targetYScale, tongueSpeed * Time.deltaTime);
            currentTongue.transform.localScale = new Vector3(transform.localScale.x, currentYScale, transform.localScale.z);

            // If the tongue has reached its target length, retract the tongue
            if (currentYScale >= targetYScale)
            {
                isRetracting = true;
                targetYScale = 0f;
            }
        }
        else if (isRetracting)
        {
            // Update the current Y scale based on the tongue retract speed
            currentYScale = Mathf.MoveTowards(currentYScale, targetYScale, tongueRetractSpeed * Time.deltaTime);
            currentTongue.transform.localScale = new Vector3(transform.localScale.x, currentYScale, transform.localScale.z);

            // If the tongue has retracted completely, deactivate the ability and destroy the tongue GameObject
            if (currentYScale <= 0f)
            {
                isTongueActive = false;
                isRetracting = false;
                Destroy(currentTongue);
            }
        }
    }

    

    public void Activate()
    {
       
    }
}
