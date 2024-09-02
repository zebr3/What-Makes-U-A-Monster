using UnityEngine;
using TMPro;

public class PickupLineStretch : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent; 
    [SerializeField] float widthScaleSpeed = 2500f; 

    private RectTransform rectTransform;
    private float maxWidth;
    private bool isMaxWidthReached = false; 

    float timeToWait = 2f;
    float waitTimer;

    private Vector2 originalSize;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        CalculateMaxWidth();
        waitTimer = timeToWait;
        originalSize = rectTransform.sizeDelta;
    }

    // Calculate the maximum width based on the length of the text
    void CalculateMaxWidth()
    {
        
        maxWidth = textComponent.preferredWidth * 1.03f;
    }

    // Reset the line to its initial state
    public void ResetLine()
    {
        gameObject.SetActive(false);
        isMaxWidthReached = false;
        waitTimer = timeToWait;
        rectTransform.sizeDelta = originalSize;
        gameObject.SetActive(true);



    }

    private void Update()
    {

       

        // Get the current size of the rectangle
        Vector2 currentSize = rectTransform.sizeDelta;

        // Calculate the new width value
        float newWidth = currentSize.x + (widthScaleSpeed * Time.deltaTime);

        // Limit the width to the maximum width
        newWidth = Mathf.Clamp(newWidth, 0f, maxWidth);

        // Set the new width if the maximum width is not reached yet
        if (!isMaxWidthReached)
        {
            CalculateMaxWidth();
            rectTransform.sizeDelta = new Vector2(newWidth, currentSize.y);
        }

        // Check if the rectangle has reached its maximum width
        if (!isMaxWidthReached && newWidth >= maxWidth)
        {
            isMaxWidthReached = true;
        }

        // Update the waitTimer if the maximum width is reached
        if (isMaxWidthReached)
        {
            waitTimer -= Time.deltaTime;
        }

        // Check if the maximum width is reached and the waitTimer is below 0
        if (isMaxWidthReached && waitTimer <= 0f)
        {
            // Calculate the new width for shrinking back to the original size
            float shrinkWidth = currentSize.x - (widthScaleSpeed * Time.deltaTime);

            // Limit the width to the original size
            shrinkWidth = Mathf.Clamp(shrinkWidth, originalSize.x, maxWidth);

            // Set the new width for shrinking
            rectTransform.sizeDelta = new Vector2(shrinkWidth, currentSize.y);

            // Check if the width has reached the original size and update the flag
            if (shrinkWidth <= originalSize.x)
            {
                isMaxWidthReached = false;
                waitTimer = timeToWait;
                this.gameObject.SetActive(false);
            }
        }


    }
}