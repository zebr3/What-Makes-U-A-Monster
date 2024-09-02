using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTime : MonoBehaviour
{
    public float changeTime;
    public string sceneName;
    public bool fadeToBlack;

    private bool isFading = false;
    private float fadeWait = 0f;

    void Update()
    {
        changeTime -= Time.deltaTime;

        if (changeTime <= 0 && !fadeToBlack)
        {
            SceneManager.LoadScene(sceneName);
        }
        else if (changeTime <= 0 && fadeToBlack && !isFading)
        {
            StartCoroutine(OnTimeFadeCo());
        }
    }

    public IEnumerator OnTimeFadeCo()
    {
        isFading = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = false;
        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("StartFade", true);
        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("EndFade", false);

        yield return new WaitForSeconds(fadeWait);

        GameObject.FindWithTag("Player").GetComponent<PlayerMovementMouse>().movementAllowed = true;
        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("StartFade", false);
        GameObject.FindWithTag("Fade").GetComponent<Animator>().SetBool("EndFade", true);

        yield return new WaitForSeconds(fadeWait);

        SceneManager.LoadScene(sceneName);
    }
}
