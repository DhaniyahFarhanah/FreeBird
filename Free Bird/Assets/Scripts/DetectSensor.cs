using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSensor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Player") && gameObject.CompareTag("Tail"))
        {
            AudioManager.Instance.PlaySnake("Rattle");
        }

        if (collided.CompareTag("Player") && gameObject.CompareTag("Lookout"))
        {
            AudioManager.Instance.PlayRaccoon("Chitter");
        }
    }

}
