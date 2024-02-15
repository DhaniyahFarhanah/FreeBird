using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    [SerializeField] GameObject warning;

    bool showWarning;

    // Start is called before the first frame update
    void Start()
    {
        warning.SetActive(false);
    }

    private void FixedUpdate()
    {

        if (showWarning)
        {
            warning.SetActive(true);
        }

        else
        {
            warning.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Branch"))
        {
            showWarning = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Branch"))
        {
            showWarning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Branch"))
        {
            showWarning = false;
        }
    }
}
