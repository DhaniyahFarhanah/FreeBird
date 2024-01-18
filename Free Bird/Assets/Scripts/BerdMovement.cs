using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined; //Confines cursor to borders of the screen
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() //for inputs
    {
        if (GameStateManager.GetGameStatus() && !GameStateManager.GetEnd())  //Playing the game
        {
            MoveBerdWithMouse();
        }

        else //not playing the game
        {

        }
    }

    void MoveBerdWithMouse()
    {
        mouseScreenPosition = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseXaxis = mouseWorldPosition.x;

        if(mouseXaxis < minBerdXaxis)
        {
            desiredPosition = new Vector3(minBerdXaxis ,berdYaxis);
        }
        else if(mouseXaxis > maxBerdXaxis)
        {
            desiredPosition = new Vector2(maxBerdXaxis, berdYaxis);
        }
        else
        {
            desiredPosition = new Vector3(mouseXaxis, berdYaxis, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoother * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Branch"))
        {
            //get hit
        }

        if (collided.CompareTag("Ground"))
        {
            Debug.Log("hitGround");
            GameStateManager.SetEnd(true);
            anim.SetBool("end", true);
        }
    }
}
