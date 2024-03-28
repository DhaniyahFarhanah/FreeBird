using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedScript : MonoBehaviour
{
    [SerializeField] GameObject guidedGuy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Player"))
        {
            Debug.Log("test");
            guidedGuy.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (collided.CompareTag("Player"))
        {
            StartCoroutine(ShowGuy());
        }
    }

    IEnumerator ShowGuy()
    {
        guidedGuy.SetActive(true);
        guidedGuy.GetComponent<Animator>().SetTrigger("Show");
        yield return new WaitForSeconds(0.5f);
        guidedGuy.SetActive(false);
    }
}
