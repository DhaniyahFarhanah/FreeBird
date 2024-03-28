using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class SectionHandler : MonoBehaviour
{
    [SerializeField] GameObject easyBg;
    [SerializeField] GameObject midBg;
    [SerializeField] GameObject hardBg;

    [SerializeField] Vector2 midSpawn;
    [SerializeField] Vector2 hardSpawn;

    float timer = 0;
    int current = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GetGameStatus())
        {
            timer += Time.deltaTime;
        }
        
        if(current != GameStateManager.GetSectionCode())
        {
            Debug.Log("Section " + GameStateManager.GetSectionCode() + " Time Taken: " + timer);
            current = GameStateManager.GetSectionCode();
            ChangeBG(current);
        }
    }

    void ChangeBG(int bg)
    {
        switch (bg)
        {
            case 1:
                //in easy level
                easyBg.SetActive(true);
                midBg.SetActive(false);
                hardBg.SetActive(false);
                break;
            case 2:
                //in medium level
                easyBg.SetActive(false);
                midBg.SetActive(true);
                hardBg.SetActive(false);
                break;
            case 3:
                //in hard level
                easyBg.SetActive(false);
                midBg.SetActive(false);
                hardBg.SetActive(true);
                break;
        }
    }
}
