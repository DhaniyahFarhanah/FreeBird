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
            AudioManager.Instance.Normalize();
            AudioManager.Instance.Transition();
            StartCoroutine(DelayThenHard());
        }
    }

    private IEnumerator DelayThenMedium()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.PlayMusic("Medium");
    }

    private IEnumerator DelayThenHard()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.PlayMusic("Hard");
    }
}
