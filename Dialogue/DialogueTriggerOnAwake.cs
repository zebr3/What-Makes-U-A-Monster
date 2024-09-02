using UnityEngine;

public class DialogueTriggerOnAwake : MonoBehaviour
{
 
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    PlayerMovementMouse playerMovement;
    public bool repeatDialogue = false;

    private void Awake()
    {
        playerInRange = false;
       // visualCue.SetActive(false);
    }

    public void Update()
    {
       /* if (playerInRange && !SecondDialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            
        }
        else
        {
            visualCue.SetActive(false);
            
        }

        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>();
      */
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
            SecondDialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if(repeatDialogue) {playerMovement.AllowMovement(); }
            else
            {
                DialogueTriggerOnAwake.Destroy(gameObject);
                playerMovement.AllowMovement();
            }
           
        }
    }

    
}
