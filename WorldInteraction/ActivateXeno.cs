using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateXeno : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!GameManager.instance.xenoQuest)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
