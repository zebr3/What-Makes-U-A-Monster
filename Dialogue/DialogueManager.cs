using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    public bool isDialogueActive = false;
    Story currentStory;
    static DialogueManager instance;

    [SerializeField] GameObject[] choices;
    TextMeshProUGUI[] choicesText;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one DialogueManager found in Scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    void Update()
    {
        if(!isDialogueActive)
        {
            return;
        }
        
    }

    public void StartDialogue(TextAsset inkjson)
    {
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        currentStory = new Story(inkjson.text);

        ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        if (currentStory.canContinue)
        {
            //display text
            dialogueText.text = currentStory.Continue();
            //display choices 
            ShowChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        isDialogueActive = false;
        dialogueText.text = "";
    }

    void ShowChoices()
    {
        List<Choice> currentChoice = currentStory.currentChoices;

        //check if ui has enough choice options
        if(currentChoice.Count > choices.Length)
        {
            Debug.LogError("More choices thank our UI supports;" + currentChoice.Count);
        }

        int index = 0;

        foreach(Choice choice in currentChoice)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }
}
