using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pipe : MonoBehaviour, IDropHandler
{
    InteractableItems interactableItems;
    CapsuleCollider2D tileCollider;

    [SerializeField] Item pipeItem;

    int requiredItemId = 0;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] GameObject[] backgroundsWater;
    int dropcount;

    Animator fade;

    void Start()
    {
        interactableItems = GetComponent<InteractableItems>();
        tileCollider = GetComponent<CapsuleCollider2D>();
        fade = GameObject.FindWithTag("Fade").GetComponent<Animator>();

    }
    public void OnDrop(PointerEventData eventData)
    {
        // Check if the dropped item has the correct ID(mirror shard)
        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (droppedItem != null && droppedItem.itemID == requiredItemId && dropcount < 2)
        {
            ActivateNextBackground();

        }
    }
    void ActivateNextBackground()
    {

        dropcount++;

        if (dropcount == 1)
        {
            interactableItems.item = pipeItem;
            backgroundsWater[0].SetActive(false);
            backgroundsWater[1].SetActive(true);

        }

        if (dropcount == 2)
        {
            tileCollider.enabled = false;
            backgroundsWater[1].SetActive(false);
            backgroundsWater[2].SetActive(true);
            StartCoroutine(Pipebreak());
        }


    }

    IEnumerator Pipebreak()
    {
        yield return new WaitForSeconds(2);

        GameObject.FindWithTag("Fade").GetComponent<Image>().color = Color.white;

        float duration = 3f; // Duration of the camera shake
        float magnitude = 0.01f; // Magnitude of the camera shake

        // Get the original position of the camera
        Vector3 originalPosition = Camera.main.transform.position;

        float timer = 0.0f;

        while (timer < duration)
        {
            // Generate a random offset for the camera position
            float offsetX = Random.Range(-3f, 3f) * magnitude;
            float offsetY = Random.Range(-3f, 3f) * magnitude;

            // Apply the camera shake by modifying the position
            Camera.main.transform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            // Wait for the next frame
            yield return null;

            timer += Time.deltaTime;

            if (timer > 2)
            {
                fade.SetBool("StartFade", true);
                fade.SetBool("EndFade", false);
            }
        }

        yield return new WaitForSeconds(1);

        // Reset the camera position to the original position
        Camera.main.transform.position = originalPosition;
        backgroundsWater[2].SetActive(false);

        for (int i = 3; i < backgroundsWater.Length; i++)
        {
            backgroundsWater[i].SetActive(true);
           
        }

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }



        fade.SetBool("StartFade", false);
        fade.SetBool("EndFade", true);

        yield return new WaitForSeconds(2);

        GameObject.FindWithTag("Fade").GetComponent<Image>().color = Color.black;




    }


}
