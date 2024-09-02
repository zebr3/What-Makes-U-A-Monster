using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHearts : MonoBehaviour
{

    [SerializeField] GameObject FirstHeart;
    [SerializeField] GameObject SecondHeart;
    [SerializeField] GameObject ThirdHeart;

    Vector2 offsetToPlayer;
    [SerializeField] Transform player;
    float squareDistance;
    float lenghtCheck = 3f;

    bool alreadyTalked = false;
    Material Heart;

    void Start()
    {
        Heart = FirstHeart.GetComponent<Renderer>().material;
    }

    
    void Update()
    {
        offsetToPlayer = transform.position - player.position;
        squareDistance = offsetToPlayer.sqrMagnitude;

        if (squareDistance <= lenghtCheck * lenghtCheck)
        {
            FirstHeart.SetActive(true);
            SecondHeart.SetActive(true);
            ThirdHeart.SetActive(true);
        }
        else
        {
            FirstHeart.SetActive(false);
            SecondHeart.SetActive(false);
            ThirdHeart.SetActive(false);
        }

        if(alreadyTalked == true)
        {

            Heart.color = new Color( 75, 75, 75);


        }

        
    }

    public void Talked()
    {
        alreadyTalked = true;
    }
}
