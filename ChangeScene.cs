using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    SceneFade sceneFade;
    void Start()
    {
        sceneFade = GameObject.Find("SceneManager").GetComponent<SceneFade>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sceneFade.LoadScene();
            Debug.Log("huso");
        }
    }
}
