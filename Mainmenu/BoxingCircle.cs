using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxingCircle : MonoBehaviour, IPointerClickHandler
{
    public Sprite leftClickSprite;
    public Sprite rightClickSprite;
    public Sprite middleClickSprite;
    [HideInInspector] float fadeSpeed = 1.3f;
    public float maxAlpha = 1f;

    private int leftOrRight;
    private SpriteRenderer buttonSpriteRenderer;
    private Transform parentTransform;
    private float currentAlpha = 0f;
    private bool isIncreasing = true;

    float addedScore = 3;
    private void Start()
    {
        // Randomly select 0 or 1
        leftOrRight = Random.Range(0, 3);
        buttonSpriteRenderer = GetComponent<SpriteRenderer>();
        parentTransform = transform.parent;

        // Set the initial sprite based on the leftOrRight value
        if (leftOrRight == 0)
        {
            buttonSpriteRenderer.sprite = leftClickSprite;
        }
        else if (leftOrRight == 1)
        {
            buttonSpriteRenderer.sprite = rightClickSprite;
        }
        else
        {
            buttonSpriteRenderer.sprite = middleClickSprite;
        }
    }

    private void Update()
    {
        addedScore -= Time.deltaTime;

        if (isIncreasing)
        {
            currentAlpha += fadeSpeed * Time.deltaTime;
            if (currentAlpha >= maxAlpha)
            {
                currentAlpha = maxAlpha;
                isIncreasing = false;
            }
        }
        else
        {
            currentAlpha -= fadeSpeed * Time.deltaTime;
            if (currentAlpha <= 0f)
            {
                currentAlpha = 0f;
                Destroy(parentTransform.gameObject);
            }
        }

        // Update the alpha of the object and its parent
        buttonSpriteRenderer.color = new Color(buttonSpriteRenderer.color.r, buttonSpriteRenderer.color.g, buttonSpriteRenderer.color.b, currentAlpha);
        if (parentTransform != null)
        {
            SpriteRenderer parentSpriteRenderer = parentTransform.GetComponent<SpriteRenderer>();
            if (parentSpriteRenderer != null)
            {
                parentSpriteRenderer.color = new Color(parentSpriteRenderer.color.r, parentSpriteRenderer.color.g, parentSpriteRenderer.color.b, currentAlpha);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the clicked mouse button matches the leftOrRight value
        if ((leftOrRight == 0 && eventData.button == PointerEventData.InputButton.Left) ||
            (leftOrRight == 1 && eventData.button == PointerEventData.InputButton.Right) ||
            (leftOrRight == 2 && eventData.button == PointerEventData.InputButton.Middle))
        {
            GameObject.FindWithTag("BoxingManager").GetComponent<BoxingMinigame>().score += addedScore * 10;
            GameObject.FindWithTag("BoxingManager").GetComponent<BoxingMinigame>().UpdateScore();
            Destroy(parentTransform.gameObject);
            Debug.Log(GameObject.FindWithTag("BoxingManager").GetComponent<BoxingMinigame>().score);
        }
        else
        {
            GameObject.FindWithTag("BoxingManager").GetComponent<BoxingMinigame>().score -= addedScore * 10;
            GameObject.FindWithTag("BoxingManager").GetComponent<BoxingMinigame>().UpdateScore();
            Destroy(parentTransform.gameObject);
        }
    }
}
