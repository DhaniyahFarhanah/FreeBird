using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject startButton;

    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

    }

    void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            //set pause screen active
            Cursor.visible = true;
            GameStateManager.Playing(false);
        }

        else if(isPaused)
        {
            isPaused = false;
            Cursor.visible = false;
            //set pause screen inactive
            GameStateManager.Playing(true);
        }
    }

    public void StartButton()
    {
        Cursor.visible = false;
        GameStateManager.Playing(true);
        startButton.SetActive(false);
    }

}
