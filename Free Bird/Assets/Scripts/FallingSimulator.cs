using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This is to control the movement of the trees/falling
public class FallingSimulator : MonoBehaviour
{
    public float speed;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    [SerializeField] float timeTilMaxVelocity;
    float interval;

    [SerializeField] float endPointY;
    Vector2 endPoint;
    [SerializeField] TMP_Text distanceIndicator;

    [SerializeField] Transform player;
    [SerializeField] Transform ground;
    float distancetoDeath;
    [SerializeField] float maxDistance;


    // Start is called before the first frame update
    void Start()
    {
        minSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        distancetoDeath = (player.position.y - ground.position.y) * 1/maxDistance;

        distanceIndicator.text = ((int)distancetoDeath) + " feet";



        if (GameStateManager.GetGameStatus() && !GameStateManager.GetEnd()) //Playing the game
        {
            endPoint = new Vector2(transform.position.x, endPointY);

            if(speed < maxSpeed)
            {
                interval = (maxSpeed - minSpeed) / timeTilMaxVelocity;
                speed += interval;
            }

            transform.position = Vector2.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
        }

        else //not playing the game
        {

        }
    }
}
