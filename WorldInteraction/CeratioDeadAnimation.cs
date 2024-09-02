using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeratioDeadAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dead()
    {
        GetComponent<Animator>().SetTrigger("ceratiotot");
        GameManager.instance.uiElementsToHide.SetActive(true);
    }
}
