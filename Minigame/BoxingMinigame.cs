using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class BoxingMinigame : MonoBehaviour
{
    [SerializeField] GameObject circle;

    float waitTime = 0.65f;
    float roundtime = 20f;

    public float score = 0;
    [SerializeField] TextMeshProUGUI scoreText;

    float minimumScore = 1;

    Vector2 targetPosition = new Vector2(0.7f, -0.9f);
    Vector2 startPosition = new Vector2(4f, -0.9f);
    [SerializeField] GameObject handschuh;

    bool hitKelly;
    bool aprilReturn;


    public TextMeshProUGUI cooldownText;
    float cooldownTime = 4f;
    float fadeDuration = 1f;
    int currentRound = 1;
    public Slider playerHealth;
    [SerializeField] Slider kellyBar;
    public float damage = 0.34f;
    bool miniGameStart;
    public bool reset;
    public GameObject sceneTransition;


    void Start()
    {
        GameManager.instance.uiElementsToHide.SetActive(false);
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        // Display "Round 1"
        cooldownText.color = new Color(1f, 1f, 1f);
        cooldownText.text = "Round " + currentRound;
        yield return new WaitForSeconds(1f);

        // Fade out "3"
        cooldownText.text = cooldownTime.ToString();
        StartCoroutine(FadeOutText());

        yield return new WaitForSeconds(1f);

        // Other actions after the cooldown
    }

    private IEnumerator FadeOutText()
    {
        float duration = fadeDuration; // Duration of the fade-out effect
        float timer = 0f; // Timer to track the duration

        Color originalColor = cooldownText.color; // Store the original color of the text

        while (cooldownTime > 1)
        {
            timer = 0f; // Reset the timer

            // Lower cooldownTime by one and update the text
            cooldownTime--;
            cooldownText.text = cooldownTime.ToString();

            // Fade out the text
            while (timer < duration)
            {
                timer += Time.deltaTime;

                // Calculate the alpha value based on the timer
                float alpha = Mathf.Lerp(1f, 0f, timer / duration);

                // Set the new color with the updated alpha value
                cooldownText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                yield return null;
            }

            // Make sure the text is completely faded out
            cooldownText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

            // Set full alpha for the text
            cooldownText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        }

        // Change the text to "Start!"
        cooldownText.text = "Start!";
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.15f);
            cooldownText.enabled = false;
            yield return new WaitForSeconds(0.15f);
            cooldownText.enabled = true;

        }

        cooldownText.enabled = false;
        StartCoroutine(Minigame());

    }






    // Update is called once per frame
    void Update()
    {
        if (miniGameStart)
        {
            roundtime -= Time.deltaTime;
            GameManager.instance.uiElementsToHide.SetActive(false);
        }

        if (hitKelly)
        {
            GameObject.FindWithTag("Player").transform.position = Vector2.MoveTowards(GameObject.FindWithTag("Player").transform.position, targetPosition, 1.2f * Time.deltaTime);

        }

        if (aprilReturn && GetComponent<SmoothSliderValue>().targetValue > 0.1)
        {
            GameObject.FindWithTag("Player").transform.position = Vector2.MoveTowards(GameObject.FindWithTag("Player").transform.position, startPosition, 1.2f * Time.deltaTime);
            
        }
        if (reset)
        {
            StopAllCoroutines();
            StartCoroutine(RestartGame());

            reset = false;
        }
        //Debug.Log(reset);

    }

    public void UpdateScore()
    {
        scoreText.text = "Score : " + score.ToString("0000");
    }

    void SpawnCircle()
    {

        Instantiate(circle, new Vector2(Random.Range(0.5f, 3.5f), Random.Range(-2, 0.5f)), Quaternion.identity);


    }

    public IEnumerator RestartGame()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.15f);
            cooldownText.enabled = false;
            yield return new WaitForSeconds(0.15f);
            cooldownText.enabled = true;



        }
        yield return new WaitForSeconds(3);
        Debug.Log("restart");
        //SceneManager.LoadScene(11);
        SceneManager.LoadScene("Minigame");
    }

    public IEnumerator Minigame()
    {
        miniGameStart = true;
        for (int round = 1; round <= 5; round++)
        {
            // Round start
            while (roundtime > 0)
            {
                SpawnCircle();
                yield return new WaitForSeconds(waitTime);

            }

            yield return new WaitForSeconds(2f);

            if (score >= minimumScore)
            {
                // Kelly schaden machen
                hitKelly = true;
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("WalkEast");

                while ((Vector2)GameObject.FindWithTag("Player").transform.position != targetPosition)
                    yield return null;

                hitKelly = false;
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("StartBaseball");
                GameObject.FindWithTag("KellyBre").GetComponent<Animator>().SetTrigger("StartDestroy");
                yield return new WaitForSeconds(0.5f);
                GetComponent<SmoothSliderValue>().sliderIndex = 1;
                GetComponent<SmoothSliderValue>().SetSliderValue(GetComponent<SmoothSliderValue>().sliders[GetComponent<SmoothSliderValue>().sliderIndex].value -= damage);

                if (GetComponent<SmoothSliderValue>().targetValue < 0.2)
                {
                    cooldownText.enabled = true;
                    cooldownText.text = "You Win";
                    cooldownText.color = new Color(0.945f, 0.690f, 0.129f);

                    for (int i = 0; i < 4; i++)
                    {
                        yield return new WaitForSeconds(0.15f);
                        cooldownText.enabled = false;
                        yield return new WaitForSeconds(0.15f);
                        cooldownText.enabled = true;



                    }
                    yield return new WaitForSeconds(2);
                    GameManager.instance.uiElementsToHide.SetActive(true);
                    
                    sceneTransition.GetComponent<SceneTransister>().LoadScene("12Fightroom", 1);
                   
                }
                yield return new WaitForSeconds(0.2f);



                aprilReturn = true;

                // Move player to the start position
                while ((Vector2)GameObject.FindWithTag("Player").transform.position != startPosition)
                    yield return null;

                GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("idle");
                aprilReturn = false;

                minimumScore += 500;
                waitTime -= 0.1f;
            }
            else
            {
                Instantiate(handschuh, new Vector2(0.15f, -0.65f), Quaternion.identity);
                yield return new WaitForSeconds(1);

                minimumScore += score;
            }

            yield return new WaitForSeconds(2);

            Debug.Log("round " + round + " over");
            currentRound++;
            cooldownText.enabled = true;
            cooldownText.text = "Round " + currentRound;
            yield return new WaitForSeconds(1);
            cooldownText.text = "Start!";

            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.15f);
                cooldownText.enabled = false;
                yield return new WaitForSeconds(0.15f);
                cooldownText.enabled = true;

            }

            cooldownText.enabled = false;
            roundtime = 20f;

        }

        Debug.Log("game is over");
        //SceneManager.LoadScene("12Fightroom");
    }
}
