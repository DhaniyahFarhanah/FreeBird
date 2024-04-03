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

        if (gameObject.CompareTag("EasyExit"))
        {
            AudioManager.Instance.Normalize();
            AudioManager.Instance.Transition();
            StartCoroutine(DelayThenMedium());
        }

        if(gameObject.CompareTag("MediumExit"))
        {
            AudioManager.Instance.Transition();
            AudioManager.Instance.Normalize();
            //AudioManager.Instance.PlayMusic("Hard");
            //AudioManager.Instance.Normalize();
            StartCoroutine(DelayThenHard());
        }
    }

    private IEnumerator DelayThenMedium()
    {
        yield return new WaitForSeconds(3.5f);
        AudioManager.Instance.PlayMusic("Medium");
    }

    private IEnumerator DelayThenHard()
    {
        yield return new WaitForSeconds(3.5f);
        AudioManager.Instance.PlayMusic("Hard");
    }
}
