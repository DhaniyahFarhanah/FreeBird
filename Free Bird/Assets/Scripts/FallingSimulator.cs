using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is to control the movement of the trees/falling
public class FallingSimulator : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;

    [SerializeField] float timeTilMaxVelocity;

    [SerializeField] GameObject EndPoint; //Position of where the end of the trees will go

    bool isPlaying;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isPlaying = GameStateManager.GetGameStatus();
        
        
        if (isPlaying) //Playing the game
        {
            if(speed < maxSpeed)
            {
                speed += maxSpeed / timeTilMaxVelocity;
            }

            transform.position = Vector2.MoveTowards(transform.position, EndPoint.transform.position, speed * Time.deltaTime);
        }

        else //not playing the game
        {

        }
    }
}
