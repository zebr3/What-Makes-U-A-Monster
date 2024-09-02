using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionTextWriter : MonoBehaviour
{
    bool isCoroutineActive = false;
    float textSpeed = 0.015f;

    [SerializeField] GameObject inventoryPage;



    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.descriptionBox.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.instance.descriptionBox.SetActive(false);
                StopCoroutine(DescriptionWritingEffect("descriptionText"));
                isCoroutineActive = false;
                GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().mousePosition = GameObject.FindWithTag("Player").transform.position;

                if (!inventoryPage.activeInHierarchy)
                {
                    GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = true;
                }
            }

        }
    }

    public void StartDescription(string descriptionText)
    {
        GameManager.instance.descriptionBox.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = false;
        string itemDescription = descriptionText;
        StartCoroutine(DescriptionWritingEffect(itemDescription));
    }


    private IEnumerator DescriptionWritingEffect(string descriptionText)
    {

        isCoroutineActive = true;
        GameManager.instance.descriptionText.text = descriptionText;

        var text = GameManager.instance.descriptionText.text;
        var newText = new System.Text.StringBuilder();
        var clearText = "<alpha=#00>";

        for (int i = 1; i < text.Length; i++)
        {
            if (isCoroutineActive)
            {
                newText.Clear();
                newText.Append(text.Substring(0, i));
                newText.Append(clearText);
                newText.Append(text.Substring(i));

                GameManager.instance.descriptionText.text = newText.ToString();

                yield return new WaitForSeconds(textSpeed);
            }
        }

        GameManager.instance.descriptionText.text = text;

        isCoroutineActive = false;
    }
}
