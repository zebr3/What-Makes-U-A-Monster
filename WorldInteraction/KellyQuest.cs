using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KellyQuest : MonoBehaviour
{
   int dustLeft;
   public bool noDustLeft;
    public static KellyQuest instance;
    [SerializeField] GameObject Exit;
    [SerializeField] GameObject dialogueTrigger;
    void Start()
    {
        instance = this;
        dustLeft = transform.childCount;
    }

    public bool CheckIfDustGone()
    {
       if (noDustLeft == true) { return true; }
       else { return false; }

      
    }

    // Update is called once per frame
    void Update()
    {
        dustLeft = transform.childCount;

        if(transform.childCount == 0)
        {
            Exit.SetActive(true);
            dialogueTrigger.SetActive(false);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = true;

        }
        else
        {
            noDustLeft = false;
        }
    }
}
