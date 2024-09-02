using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransister : MonoBehaviour
{

    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    float fadeWait = 1f;
    public static SceneTransister sceneTransisterInstance;

    public void Awake()
    {
        sceneTransisterInstance = this;
    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            StartCoroutine(FadeCo());
            
        }
    }

    public void OnClickLoad(string sceneLoad)
    {
        StartCoroutine(FadeCo());
    }
    public void OnClickLoadNoTransition(string sceneLoad)
    {

        SceneManager.LoadScene(sceneLoad);
    }

    public void LoadScene(string sceneToLoad, int withScreenTransition)
    {
        if (withScreenTransition == 1)
        {
            StartCoroutine(FadeCo());
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
       
    }

    public IEnumerator FadeCo()
    {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>() != null)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = false;
        }

     
        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("StartFade", true);
        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("EndFade", false);

        yield return new WaitForSeconds(fadeWait);

        if (GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>() != null)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = true;
        }
       
        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("StartFade", false);
        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("EndFade", true);
        SceneManager.LoadScene(sceneToLoad);
    }

    public IEnumerator JustFade()
    {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>() != null)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = false;
        }


        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("StartFade", true);
        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("EndFade", false);

        yield return new WaitForSeconds(fadeWait);

        if (GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>() != null)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = true;
        }

        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("StartFade", false);
        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("EndFade", true);
        
    }
}