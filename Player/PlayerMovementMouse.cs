using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovementMouse : MonoBehaviour
{
    NavMeshAgent navMesh;
    Animator animator;

    [SerializeField] private AudioSource walkingSound;
    [SerializeField] private AudioClip[] walkingSounds;
    public VectorValue startingPosition;
    [HideInInspector] public Vector3 mousePosition;

    public bool movementAllowed = true;
    public bool soundOn;
    bool isWalking;

    // Index to keep track of the current walking sound
    public int currentWalkingSoundIndex = 0;

    void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.updateRotation = false;
        navMesh.updateUpAxis = false;
        mousePosition = transform.position;
    }

    void Start()
    {
        transform.position = startingPosition.initialValue;
        animator = GetComponent<Animator>();

        // Play the initial walking sound
        if (soundOn)
        {
            walkingSound.clip = walkingSounds[currentWalkingSoundIndex];
            walkingSound.Play();
        }
        }

    public void AllowMovement()
    {
        movementAllowed = true;
    }

    public void DisallowMovement()
    {
        movementAllowed = false;
    }

    void Update()
    {
        if (movementAllowed)
        {
            // Get mouse position for player to move
            if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            navMesh.SetDestination(new Vector3(mousePosition.x, mousePosition.y, transform.position.z));

            // Animation of Walkcycle
            Vector3 direction = (navMesh.destination - transform.position).normalized;
            if (navMesh.velocity.magnitude > 0.1f)
            {
                animator.SetFloat("Vertical", direction.y);
                animator.SetFloat("Horizontal", direction.x);

                if (soundOn)
                {
                    // Check if the walking sound needs to be changed
                    if (!walkingSound.isPlaying || walkingSound.clip != walkingSounds[currentWalkingSoundIndex])
                    {
                        // Change the walking sound
                        walkingSound.clip = walkingSounds[currentWalkingSoundIndex];
                        walkingSound.Play();
                    }
                }
            }
            else
            {
                if (soundOn)
                {
                    // Stop walking sound
                    if (walkingSound.isPlaying)
                    {
                        walkingSound.Stop();
                    }
                }
            }
        }
        else
        {
            mousePosition = transform.position;
            navMesh.SetDestination(mousePosition);
            walkingSound.Stop();
        }

        // When character is moving, set isWalking parameter to true
        isWalking = navMesh.velocity.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);
    }

    bool IsMouseOverUI()
    {
        // Check if the mouse is over any gameObject with the GameUI tag
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("GameUI"))
            {
                return true;
            }
        }

        return false;
    }

    public void EndBaseballAnimation()
    {
        GetComponent<Animator>().SetTrigger("EndBaseballSwing");
    }

    public void EndFlashlight()
    {
        GetComponent<Animator>().SetTrigger("EndFlashlight");
    }

    // Method to set the walking sound index
    public void SetWalkingSoundIndex(int soundIndex)
    {
        if (soundOn && soundIndex >= 0 && soundIndex < walkingSounds.Length)
        {
            currentWalkingSoundIndex = soundIndex;
        }
    }

    public void ActivateAbilities(string abilityName)
    {
        if(abilityName == "Kelly")
        {
           CommanderAbility ability = GetComponent<CommanderAbility>();
            ability.enabled = true;
        }
        if (abilityName == "Kana")
        {
            TongueAbility ability = GetComponent<TongueAbility>();
            ability.enabled = true;
        }
      
    }
}
