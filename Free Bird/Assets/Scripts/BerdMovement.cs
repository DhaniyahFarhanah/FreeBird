using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BerdMovement : MonoBehaviour
{
    //Cursor stuff
    Vector3 mouseScreenPosition;            //Position of where mouse is in the screen units
    Vector3 mouseWorldPosition;             //Position of where the mouse is to the world units
    Vector3 desiredPosition;                //Position of where berd wants to go


    [SerializeField] float smoother;        //Time for smoothing of movement
    [SerializeField] float mouseXaxis;      //Location of the mouse in the X axis 
    [SerializeField] float berdYaxis;       //Locks berd Y axis
    [SerializeField] float minBerdXaxis;    //Left tree border location on X axis
    [SerializeField] float maxBerdXaxis;    //Right tree border location on Y axis

    Animator anim;

    [SerializeField] TMP_Text hpText;
    int hp = 3;
    bool hit;
    [SerializeField] GameObject level;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined; //Confines cursor to borders of the screen
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hit = true;
    }

    // Update is called once per frame
    void Update() //for inputs
    {
        hpText.text = hp + " HP";

        if (GameStateManager.GetGameStatus() && !GameStateManager.GetEnd())  //Playing the game
        {
            MoveBerdWithMouse();
            
        }

        else //not playing the game
        {

        }
        if(hp == 0) //death
        {
            GameStateManager.SetEnd(true);
            anim.SetBool("end", true);
        }
    }

    void MoveBerdWithMouse()
    {
        mouseScreenPosition = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseXaxis = mouseWorldPosition.x;

        if (mouseXaxis < minBerdXaxis)
        {
            desiredPosition = new Vector3(minBerdXaxis, berdYaxis);
        }
        else if (mouseXaxis > maxBerdXaxis)
        {
            desiredPosition = new Vector2(maxBerdXaxis, berdYaxis);
        }
        else
        {
            desiredPosition = new Vector3(mouseXaxis, berdYaxis, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoother * Time.deltaTime);
    }

    IEnumerator GotHit()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        hit = false;
        level.GetComponent<FallingSimulator>().speed = 5;
        AudioManager.Instance.PlaySFX("Hurt");
        

        yield return new WaitForSeconds(1f);

        hit = true;
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Ground"))
        {
            Debug.Log("hitGround");
            GameStateManager.SetEnd(true);
            anim.SetBool("end", true);
            AudioManager.Instance.PlayMusic("Lose");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Branch"))
        {
            if (hit)
            {
                hp--;
                StartCoroutine(GotHit());
            }

            Debug.Log("hitBranch");
        }
        if (collided.CompareTag("Sensor"))
        {
            Debug.Log("Difficulty Reached");
        }
    }


}
