using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSensor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySnake("Rattle");
        }
    }
}
