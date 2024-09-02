using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handschuh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)GameObject.FindWithTag("Player").transform.position + new Vector2(0, 0.5f), 3 * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindWithTag("PlayerBre").GetComponent<Animator>().SetTrigger("StartDestroy");
            GameObject.FindWithTag("BoxingManager").GetComponent<SmoothSliderValue>().sliderIndex = 0;
            GameObject.FindWithTag("BoxingManager").GetComponent<SmoothSliderValue>().SetSliderValue(GameObject.FindWithTag("BoxingManager").GetComponent<SmoothSliderValue>().sliders[GameObject.FindWithTag("BoxingManager").GetComponent<SmoothSliderValue>().sliderIndex].value -= GameObject.FindWithTag("BoxingManager").GetComponent<BoxingMinigame>().damage);
            if (GameObject.FindWithTag("BoxingManager").GetComponent<SmoothSliderValue>().targetValue < 0.2)
            {


                GameObject.FindWithTag("BoxingManager").GetComponent<BoxingMinigame>().cooldownText.enabled = true;
                GameObject.FindWithTag("BoxingManager").GetComponent<BoxingMinigame>().cooldownText.text = "You Lose";
                GameObject.FindWithTag("BoxingManager").GetComponent<BoxingMinigame>().cooldownText.color = new Color(0f, 0f, 0f);

                GameObject.FindWithTag("BoxingManager").GetComponent<BoxingMinigame>().reset = true;

            }
            Destroy(gameObject);
        }
    }
}
