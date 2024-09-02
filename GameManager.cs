using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject dialoguePanel;
    public GameObject uiElementsToHide;
    public GameObject darkBox;
    public GameObject continueIcon;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI displayNameText;
    public Animator portraitAnimator;
    public Animator playerPortraitAnimator;
    public GameObject[] choices;

    [SerializeField] public TextAsset loadGlobalsJSON;
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    // public PlayerMovementMouse playerMovement;

    [SerializeField] Canvas canvas;
    private Camera mainCamera;

    public GameObject announcementLine;
    public TextMeshProUGUI announcementText;
    public GameObject descriptionBox;
    public TextMeshProUGUI descriptionText;
    public GameObject nameBox;
    public TextMeshProUGUI nameText;


    public bool classroomLeave;
    public bool fightroomLeave;
    public bool lowerHallwayLeave;
    public bool mensaLeave;
    public bool campusLeave;

    [SerializeField] VectorValue afterClassroom;
    [SerializeField] VectorValue afterFightroom;

    public bool xenoQuest = false;

    public bool activateHand;
    public bool activateTongue;
    public bool activateKelly;

    public GameObject xenoAbilityPage; 
    public GameObject xenoAbilityPageText;
    public GameObject kanaAbilityPage;
    public GameObject kanaAbilityPageText;
    public GameObject kellyAbilityPage;
    public GameObject kellyAbilityPageText;

    public bool activateKanaQuest;
    public bool activateXenoQuest;

    public bool kanaFlashlightActivated;
    public bool kanaTableclothActivated;
    public bool kanaCutleryActivated;

    public bool allowCeratio;

    public bool allowAbility = true;

    public bool ceratioObjectActivated = false;

    public bool questSolved = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        nameText.raycastTarget = false;
    }



    private void OnDestroy()
    {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void activateBoolean(int index)
    {
        if(index == 1)
        {
            activateHand = true;
            InventoryManager.instance.ShowLine("You received the Hand Ability");
            questSolved = true;
            xenoAbilityPage.SetActive(false);
            xenoAbilityPageText.SetActive(true);
        }
        if (index == 2)
        {
            activateTongue = true;
            InventoryManager.instance.ShowLine("You received the Tongue Ability");
            questSolved = true;
            kanaAbilityPage.SetActive(false);
            kanaAbilityPageText.SetActive(true);
        }
        if (index == 3)
        {
            activateKelly = true;
            InventoryManager.instance.ShowLine("You received the Commander Ability");
            questSolved = true;
            kellyAbilityPage.SetActive(false);
            kellyAbilityPageText.SetActive(true);
        }
        if (index == 4)
        {
            activateKanaQuest = true;
        }
        if (index == 5)
        {
            activateXenoQuest = true;
        }

    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //set camera to the main camera of scene
        mainCamera = Camera.main;
        canvas.worldCamera = mainCamera;
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName.Contains("Cutscene"))
        {
            uiElementsToHide.SetActive(false);
        }
        else
        {
            uiElementsToHide.SetActive(true);
        }

        if(SceneManager.GetActiveScene().buildIndex == 20)
        {
            classroomLeave = true;
        }

        if(classroomLeave)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().startingPosition = afterClassroom;
            classroomLeave = false;
        }

        if (SceneManager.GetActiveScene().name == "13.Fightroom")
        {
            fightroomLeave = true;
        }

        if (fightroomLeave)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().startingPosition = afterFightroom;
            fightroomLeave = false;
        }
    }
}
