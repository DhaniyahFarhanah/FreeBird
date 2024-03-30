using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spatial : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Entrance"))
        {
            AudioManager.Instance.Muffle();
        }

        if (gameObject.CompareTag("Exit"))
        {
            AudioManager.Instance.Normalize();
        }
    }
}
