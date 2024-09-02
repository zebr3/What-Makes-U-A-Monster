using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SecondDialogueTrigger : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private Button dialogueButton;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool isClicked;
   
    private bool playerInRange;
    [SerializeField] GameObject cue;
 
    private void Awake()
    {
      
        playerInRange = false;
        visualCue.SetActive(false);
        dialogueButton.onClick.AddListener(OnDialogueButtonClick);
        dialogueButton.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (playerInRange && !SecondDialogueManager.GetInstance().dialogueIsPlaying)
        {
            //visualCue.SetActive(true);
            dialogueButton.gameObject.SetActive(true);
        }
        else
        {
            //visualCue.SetActive(false);
            dialogueButton.gameObject.SetActive(false);
        }

        if(isClicked)
        {   if(Input.GetMouseButtonDown(0))
            {
                isClicked = false;
            }
            float distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);

            if(distance <= 1.5)
            {
                isClicked = false;
                cue.SetActive(false);
                
                SecondDialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void OnDialogueButtonClick()
    {
       
        SecondDialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClicked = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cue.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cue.SetActive(false);
    }
}
