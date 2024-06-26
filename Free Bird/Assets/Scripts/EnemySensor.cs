using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    [SerializeField] Animator animator;
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

        if(collided.CompareTag("Player") && gameObject.CompareTag("Snake"))
        {
            animator.SetTrigger("Attack");
            AudioManager.Instance.PlaySnake("Strike");
        }

        if (collided.CompareTag("Player") && gameObject.CompareTag("Raccoon"))
        {
            animator.SetTrigger("Attack");
            AudioManager.Instance.PlayRaccoon("Strike");
        }
    }
}
