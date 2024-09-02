using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.Audio;


public class autoPlayingDialogueManager : MonoBehaviour
{
    [Header("Parameters")]
    private float typingSpeed = 0.02f;

    [Header("Load Globals JSON")]
    private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    private GameObject dialoguePanel;
    // [SerializeField] private GameObject uiElementsToHide;
    private GameObject darkBox;
    private GameObject continueIcon;
    private TextMeshProUGUI dialogueText;
    private TextMeshProUGUI displayNameText;
    private Animator portraitAnimator;
    private Animator playerPortraitAnimator;

    [Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo;
    [SerializeField] private DialogueAudioInfoSO[] audioInfos;
    private DialogueAudioInfoSO currentAudioInfo;
    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;
    [SerializeField] private bool makePredictable;
    [SerializeField] private AudioMixerGroup mixerGroup;

     private AudioSource audioSource;

    public CanvasManager canvasManager;
     [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private PlayerMovementMouse playerMovement;
    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private Coroutine displayLineCoroutine;

    private bool canContinueToNextLine = false;

    private static autoPlayingDialogueManager instance;

    private const string SPEAKER_TAG = "speaker";

    private const string PORTRAIT_TAG = "portrait";

    private const string PLAYER_PORTRAIT_TAG = "playerPortrait";

    //private const string LAYOUT_TAG = "speaker";

    private DialogueVariables dialogueVariables;
    public float delayBetweenLines;
    private void Awake()
    {
        loadGlobalsJSON = GameManager.instance.loadGlobalsJSON;
        dialoguePanel = GameManager.instance.dialoguePanel;
        darkBox = GameManager.instance.darkBox;
        continueIcon = GameManager.instance.continueIcon;
        dialogueText = GameManager.instance.dialogueText;
        displayNameText = GameManager.instance.displayNameText;
        portraitAnimator = GameManager.instance.portraitAnimator;
        playerPortraitAnimator = GameManager.instance.playerPortraitAnimator;
        choices = GameManager.instance.choices;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        if (instance != null)
        {
            Debug.LogWarning("More than one Dialogue in the scene");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);

        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.GetComponent<AudioSource>().outputAudioMixerGroup = mixerGroup;
        currentAudioInfo = defaultAudioInfo;

    }

    public static autoPlayingDialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        InitializeAudioInfoDictionary();
    }
    private void InitializeAudioInfoDictionary()
    {
        audioInfoDictionary = new Dictionary<string, DialogueAudioInfoSO>();
      //  audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo);
        foreach (DialogueAudioInfoSO audioInfo in audioInfos)
        {
            audioInfoDictionary.Add(audioInfo.id, audioInfo);
        }

    }
    private void SetCurrentAudioInfo(string id)
    {
        DialogueAudioInfoSO audioInfo = null;
        audioInfoDictionary.TryGetValue(id, out audioInfo);
        if (audioInfo != null)
        {
            this.currentAudioInfo = audioInfo;
        }
        else
        {
            Debug.Log("Failed to find audio info for id:" + id);
        }
    }
    private void Update()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>();
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (canContinueToNextLine &&
            currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        //uiElementsToHide.SetActive(false);

        darkBox.SetActive(true);

        playerMovement.movementAllowed = false;
        currentStory.BindExternalFunction("loadScene", (string sceneToLoad, int withScreenTransition) =>
        {
            SceneTransister sceneTransister = FindObjectOfType<SceneTransister>(); // Find the HeartSystem instance in the scene
            sceneTransister.sceneToLoad = sceneToLoad; // Set the NPC ID in the HeartSystem
            
            sceneTransister.LoadScene(sceneToLoad, withScreenTransition);

        });

        dialogueVariables.StartListening(currentStory);

        displayNameText.text = "???";
        portraitAnimator.Play("default");
        playerPortraitAnimator.Play("default_playerPortrait");

        ContinueStory();
    }


    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        //uiElementsToHide.SetActive(true);
        darkBox.SetActive(false);

        dialogueText.text = "";
        playerMovement.movementAllowed = true;
        SetCurrentAudioInfo(defaultAudioInfo.id);
    }
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();
            Debug.Log("'" + nextLine + "'");

            // ---- ADD THIS
            // handle the random case where there's a new line for some reason
            while (nextLine.Equals("\n") && currentStory.canContinue)
            {
                Debug.Log("Blank Line Skipped");
                nextLine = currentStory.Continue();
            }
            // ----

            // handle case where the last line is an external function
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                StartCoroutine(ExitDialogueMode());
            }
            // otherwise, handle the normal case for continuing the story
            else
            {
                // handle tags
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false);
      HideChoices();

        canContinueToNextLine = false;

        foreach (char letter in line.ToCharArray())
        {

            if (!dialogueIsPlaying)
            {
               

            }

            else
            {
                PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]);
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        continueIcon.SetActive(true);

        yield return new WaitForSeconds(delayBetweenLines);

        canContinueToNextLine = true;
    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
    {
        AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;
        int frequencyLevel = currentAudioInfo.frequencyLevel;
        float minPitch = currentAudioInfo.minPitch;
        float maxPitch = currentAudioInfo.maxPitch;
        bool stopAudioSource = currentAudioInfo.stopAudioSource;

        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if (stopAudioSource)
            {
                audioSource.Stop();
            }
            AudioClip soundClip = null;

            if (makePredictable)
            {
                int hashCode = currentCharacter.GetHashCode();
                int predictableIndex = hashCode % dialogueTypingSoundClips.Length;
                soundClip = dialogueTypingSoundClips[predictableIndex];
                int minPitchInt = (int)(minPitch * 100);
                int maxPitchInt = (int)(maxPitch * 100);
                int pitchRangeInt = maxPitchInt - minPitchInt;
                if (pitchRangeInt != 0)
                {
                    int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                    float predictablePitch = predictablePitchInt / 100f;
                    audioSource.pitch = predictablePitch;
                }
                else
                {
                    audioSource.pitch = minPitch;
                }
            }
            else
            {
                int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
                soundClip = dialogueTypingSoundClips[randomIndex];
                audioSource.pitch = Random.Range(minPitch, maxPitch);
            }

            audioSource.PlayOneShot(soundClip);
        }
    }


    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }
  
    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case PLAYER_PORTRAIT_TAG:
                    playerPortraitAnimator.Play(tagValue);
                    break;
                //case LAYOUT_TAG:
                //  break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled:" + tag);
                    break;
            }
        }
    }
    /*
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        // StartCoroutine(SelectFirstChoice());
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }
}
    */
}