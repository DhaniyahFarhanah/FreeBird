using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    [SerializeField] GameObject player;
    [SerializeField] GameObject ground;
    float distancetoDeath;
    [SerializeField] float maxDistance;

    [SerializeField] TMP_Text TimerUI;
    [SerializeField] TMP_Text MinUI;
    float timer;
    int min;


    // Start is called before the first frame update
    void Start()
    {
        minSpeed = speed;
        timer = 0;
        min = 0;
    }

    // Update is called once per frameq
    void Update()
    {
        distancetoDeath = ground.transform.position.y * -1;

        distanceIndicator.text = (int)distancetoDeath + " feet";

        TimerUI.text = timer.ToString();
        MinUI.text = min.ToString();


        if (GameStateManager.GetGameStatus() && !GameStateManager.GetEnd() && !GameStateManager.GetCutscene()) //Playing the game
        {
            endPoint = new Vector2(transform.position.x, endPointY);

            if(speed < maxSpeed)
            {
                interval = (maxSpeed - minSpeed) / timeTilMaxVelocity;
                speed += interval;
            }

            timer += Time.deltaTime;

            if(timer >= 60.0)
            {
                min++;
                timer = 0;
            }

            transform.position = Vector2.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
        }

        else //not playing the game
        {

        }
    }
}
