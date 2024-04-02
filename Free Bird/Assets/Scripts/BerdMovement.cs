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

    [SerializeField] Animator anim;

    [SerializeField] TMP_Text hpText;
    [SerializeField] GameObject hpHolder;
    [SerializeField] GameObject hud;
    [SerializeField] Animator featherAnim;
    [SerializeField] GameObject poof;
    [SerializeField] SpriteRenderer sprite;

    bool playOnce = false;

    [SerializeField] int hp;
    bool hit;
    [SerializeField] GameObject level;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined; //Confines cursor to borders of the screen
    }

    // Start is called before the first frame update
    void Start()
    {
        hit = true;
    }

    // Update is called once per frame
    void Update() //for inputs
    {
        hpText.text = hp + "hp";

        if (GameStateManager.GetGameStatus() && !GameStateManager.GetEnd() && !GameStateManager.GetCutscene())  //Playing the game
        {
            MoveBerdWithMouse();
            
        }

        else //not playing the game
        {

        }
        if(hp == 0) //death
        {
            AudioManager.Instance.Normalize();
            AudioManager.Instance.PlaySFX("Lose");
            GameStateManager.SetEnd(true);

            if(!playOnce)
            {
                poof.GetComponent<Animator>().SetTrigger("Hit");
                sprite.color = new Color(1, 1, 1, 0);
                playOnce = true;
            }
            
            
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
        sprite.color = Color.red;
        hit = false;
        level.GetComponent<FallingSimulator>().speed = 5;
        AudioManager.Instance.PlaySFX("Hurt");
        hpHolder.SetActive(true);
        poof.SetActive(true);

        yield return new WaitForSeconds(2f);

        hit = true;
        sprite.color = Color.white;

        if (hp == 0)
        {
            sprite.color = new Color(1, 1, 1, 0);
        }

        yield return new WaitForSeconds(3f);
        hpHolder.SetActive(false);
        poof.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Ground"))
        {
            AudioManager.Instance.Normalize();
            AudioManager.Instance.PlaySFX("Lose");
            Debug.Log("hitGround");
            GameStateManager.SetEnd(true);
            anim.SetBool("end", true);
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
                featherAnim.SetTrigger("Hit");
                poof.GetComponent<Animator>().SetTrigger("Hit");
                StartCoroutine(GotHit());
            }

            Debug.Log("hitBranch");
        }

        if (collided.CompareTag("Enemy"))
        {
            if (hit)
            {
                hp--;
                featherAnim.SetTrigger("Hit");
                poof.GetComponent<Animator>().SetTrigger("Hit");
                StartCoroutine(GotHit());
            }
        }

        if (collided.CompareTag("EasySensor"))
        {
            GameStateManager.SetSectionCode(2);
        }

        if (collided.CompareTag("MidSensor"))
        {
            GameStateManager.SetSectionCode(3);
        }

        if (collided.CompareTag("Shrub"))
        {
            gameObject.GetComponent<TrailRenderer>().enabled = true;
            sprite.color = new Color(1, 1, 1, 1);
            hud.SetActive(true);
        }
    }


}
