using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //check for distance to player, show "button" if player is nearby
    [Header("Player Distance")]
    [SerializeField] Transform playerTransform;
    [SerializeField] float lenghtCheckForUI = 3f;
    [SerializeField] float lenghtCheckForDialogue = 3f;
    float squareDistanceToPlayer;
    Vector2 offsetToPlayer;




    //objects to show for the dialogue
    [Header("Dialogue Objects")]
    [SerializeField] GameObject objectOverHead;
    [SerializeField] GameObject button;

    
    [SerializeField] TextAsset inkjson;

    DialogueManager dialogueManager;



    private void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }
    void Update()
    {
        //check distance to player with magnitude
        offsetToPlayer = transform.position - playerTransform.position;
        squareDistanceToPlayer = offsetToPlayer.sqrMagnitude;

        //check if player is close to npc, if yes he can see visual over head
        if (squareDistanceToPlayer <= lenghtCheckForUI * lenghtCheckForUI)
        {
            objectOverHead.SetActive(true);
        }

        else
        {
            objectOverHead.SetActive(false);

        }


        //check if player is close to npc, if yes he can start dialogue
        if (squareDistanceToPlayer <= lenghtCheckForDialogue * lenghtCheckForDialogue)
        {
            button.SetActive(true);

            if (dialogueManager.isDialogueActive == true)
            {
                button.SetActive(false);
            }

        }

        else
        {
            button.SetActive(false);

        }

        
       
        
    }

    public void TriggerDialogue()
    {
        DialogueManager.GetInstance().StartDialogue(inkjson);
    }
    

    










}
