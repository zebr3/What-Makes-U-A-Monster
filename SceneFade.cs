using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    [SerializeField] Animator transition;

    [SerializeField] float transitionTime = 1f;

    [SerializeField] int sceneIndex;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void StartGame() { SceneManager.LoadScene("BriefScene"); }

   
    public void LoadScene()
    {
        StartCoroutine(LoadScenes(sceneIndex));
    }

    IEnumerator LoadScenes(int index)
    {
        transition.SetTrigger("StartFade");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadScene();
            Debug.Log("huso");
            Debug.Log(sceneIndex);
        }
    }
}
