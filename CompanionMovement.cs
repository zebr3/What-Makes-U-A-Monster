using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    public float speed = 10f;
    float lenghtCheck = 1f;

    Vector2 offsetToPlayer;
    float squareDistance;

    void Update()
    {

        offsetToPlayer = transform.position - player.position;
        squareDistance = offsetToPlayer.sqrMagnitude;

        if(squareDistance >= lenghtCheck * lenghtCheck)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
