using UnityEngine;

public class AbilityWheel : MonoBehaviour
{
    public string[] abilities;   // Array of ability names
    private int currentAbilityIndex = 0;

    TeleportHandAbility teleportHandAbility;
    CommanderAbility commanderAbility;
    TongueAbility tongueAbility;



    private void Start()
    {
        teleportHandAbility = GetComponent<TeleportHandAbility>();
        commanderAbility = GetComponent<CommanderAbility>();
        tongueAbility = GetComponent<TongueAbility>();
    }
    private void Update()
    {
        
        // Get mouse wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput > 0f)
        {
            // Scroll up - switch to the next ability
            SwitchAbility(1);
        }
        else if (scrollInput < 0f)
        {
            // Scroll down - switch to the previous ability
            SwitchAbility(-1);
            
        }


        
        
    }

    private void SwitchAbility(int direction)
    {
        // Update the current ability index based on the scroll direction
        
        currentAbilityIndex += direction;
        CheckForAbilityChange();

        // Wrap around the array if the index goes beyond the bounds
        if (currentAbilityIndex < 0)
        {
            currentAbilityIndex = abilities.Length - 1;
            CheckForAbilityChange();
        }
        else if (currentAbilityIndex >= abilities.Length)
        {
            currentAbilityIndex = 0;
            CheckForAbilityChange();
        }

        // Print the selected ability
        Debug.Log("Current Ability: " + abilities[currentAbilityIndex]);
    }


    void CheckForAbilityChange()
    {
        if(currentAbilityIndex == 1)
        {
            teleportHandAbility.abilitySelected = true;
        }
        else
        {
            teleportHandAbility.abilitySelected = false;
        }

        if(currentAbilityIndex == 2)
        {
            commanderAbility.abilitySelected = true;
        }
        else
        {
            commanderAbility.abilitySelected = false;
        }

        if(currentAbilityIndex == 3)
        {
            tongueAbility.abilitySelected = true;
        }
        else
        {
            tongueAbility.abilitySelected = false;
        }
    }
}
