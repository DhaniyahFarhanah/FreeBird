using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spatial : MonoBehaviour
{
    private bool musicTransitioned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Entrance"))
        {
            AudioManager.Instance.Muffle();
        }

        if (gameObject.CompareTag("EasyExit"))
        {
            AudioManager.Instance.Transition();
            StartCoroutine(DelayThenMedium());
        }

        if(gameObject.CompareTag("MediumExit"))
        {
            AudioManager.Instance.Transition();
            StartCoroutine(DelayThenHard());
        }
    }

    private IEnumerator DelayThenMedium()
    {
        yield return new WaitForSeconds(3.0f);
        if (musicTransitioned == false)
        {
            AudioManager.Instance.PlayMusic("Medium");
            musicTransitioned = true;
        }
        yield return new WaitForSeconds(10.0f);
        musicTransitioned = false;
    }

    private IEnumerator DelayThenHard()
    {
        yield return new WaitForSeconds(3.0f);
        if (musicTransitioned == false)
        {
            AudioManager.Instance.PlayMusic("Hard");
            musicTransitioned = true;
        }
        yield return new WaitForSeconds(10.0f);
        musicTransitioned = false;
    }
}
