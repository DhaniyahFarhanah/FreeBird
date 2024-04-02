using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TranScript : MonoBehaviour
{

    [SerializeField] float secondDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveNext());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator MoveNext()
    {
        yield return new WaitForSeconds(secondDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
