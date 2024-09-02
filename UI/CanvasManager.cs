using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject tabUI;
    private Canvas cvs;
    public GameObject settingsPage;
    public GameObject KellyNpcPage;
    public GameObject KanaNpcPage;
    public GameObject XenoNpcPage;
    public Button settingsButton;
    public Button tabButton;
    public GameObject darkBox;
    public GameObject uiElementsToHide;
    private PlayerMovementMouse playerMovement;
    private GameObject NPCTriggerButtonsParent;
    private Image[] NPCTriggerButtonImages;
    private SecondDialogueManager dialogueManager;
    private static CanvasManager canvasManagerInstance;


    public bool isTabActive = false;
    public bool isSettingsActive = false;

    private void Start()
    {
        // Retrieve components
        cvs = GetComponent<Canvas>();
        SecondDialogueManager dialogueManager = GetComponent<SecondDialogueManager>();

      
    }

    public static CanvasManager GetInstance()
    {
        return canvasManagerInstance;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName.Contains("Cutscene"))
        {
            uiElementsToHide.SetActive(false);
        }
        else
        {
            uiElementsToHide.SetActive(true);
        }
    }

    private void Update()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>();
        UiToggle();

        //NPCTriggerButtonsParent = GameObject.FindWithTag("NPCButton"); 
       // NPCTriggerButtonImages = NPCTriggerButtonsParent.GetComponentsInChildren<Image>();
        
       
    }

    private void UiToggle()
    {


        if (tabUI.activeSelf)
        {
            isTabActive = true;
        }
        else
        {
            isTabActive = false;
        }

        if (settingsPage.activeInHierarchy)
        {
            isSettingsActive = true;
        }
        else
        {
            isSettingsActive = false;
        }

        if (KellyNpcPage.activeSelf)
        {
            if (NPCTriggerButtonImages != null)
            { foreach (Image npcButtonImage in NPCTriggerButtonImages)
                {
                    npcButtonImage.raycastTarget = false;
                 }
            }
            isTabActive = false;
            isSettingsActive = false;
            settingsButton.interactable = false;
            tabButton.interactable = false;
        }


        if (KanaNpcPage.activeSelf)
        {
            if (NPCTriggerButtonImages != null)
            {
                foreach (Image npcButtonImage in NPCTriggerButtonImages)
                {
                    npcButtonImage.raycastTarget = false;
                }
            }
            isTabActive = false;
            isSettingsActive = false;
            settingsButton.interactable = false;
            tabButton.interactable = false;
        }


        if (XenoNpcPage.activeSelf)
        {
            if (NPCTriggerButtonImages != null)
            {
                foreach (Image npcButtonImage in NPCTriggerButtonImages)
                {
                    npcButtonImage.raycastTarget = false;
                }
            }
            isTabActive = false;
            isSettingsActive = false;
            settingsButton.interactable = false;
            tabButton.interactable = false;
        }



        if (isTabActive)
        {
            // Disable raycast target of the SettingsButton
            playerMovement.movementAllowed = false;
            settingsButton.interactable = false;
            tabUI.SetActive(true);

            settingsPage.SetActive(false);

            if (NPCTriggerButtonImages != null)
            {
                foreach (Image npcButtonImage in NPCTriggerButtonImages)
                {
                    npcButtonImage.raycastTarget = false;
                }
            }

        }
        else
        {
            // Enable raycast target of the SettingsButton

            tabUI.SetActive(false);


        }

        if (isSettingsActive)
        {
            playerMovement.movementAllowed = false;
            settingsPage.SetActive(true);
            tabButton.interactable = false;
            tabUI.SetActive(false);

            if (NPCTriggerButtonImages != null)
            {
                foreach (Image npcButtonImage in NPCTriggerButtonImages)
                {
                    npcButtonImage.raycastTarget = false;
                }
            }

        }
        else
        {
            tabButton.interactable = true;
            settingsPage.SetActive(false);



        }

        if (!isSettingsActive && !isTabActive && !KellyNpcPage.activeSelf && !KanaNpcPage.activeSelf && !XenoNpcPage.activeSelf)
        {
            if (NPCTriggerButtonImages != null)
            {
                foreach (Image npcButtonImage in NPCTriggerButtonImages)
                {
                    npcButtonImage.raycastTarget = true;
                }
            }

            tabButton.interactable = true;
            settingsButton.interactable = true;

        }
    }


}