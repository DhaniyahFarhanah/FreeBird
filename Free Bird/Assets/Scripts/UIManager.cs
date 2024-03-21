using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject endCanvas;
    [SerializeField] AudioSource musicSource;

    //To hide
    [SerializeField] GameObject sections;

    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.SetEnd(false); //IDK WHY THE FUCK IT DOESN'T WORK WITHOUT THIS DUDE
        sections.SetActive(false);
        StopAllCoroutines();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GetGameStatus())
        {
            startButton.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !GameStateManager.GetEnd())
        {
            Pause();
        }
        else if (GameStateManager.GetEnd())
        {
            Debug.Log(GameStateManager.GetEnd());
            StartCoroutine(ShowEnd());
        }

    }

    void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            GameStateManager.Playing(false);
        }

        else if(isPaused)
        {
            isPaused = false;
            Cursor.visible = false;
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
            GameStateManager.Playing(true);
            GameStateManager.Playing(true);
        }
    }

    IEnumerator ShowEnd()
    {
        yield return new WaitForSeconds(2f);
        endCanvas.SetActive(true);
    }

    public void StartButton()
    {
        Cursor.visible = false;
        GameStateManager.Playing(true);
        startButton.SetActive(false);
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.PlayMusic("Easy");
    }

    public void RestartButton()
    {
        AudioManager.Instance.PlaySFX("Click");
        GameStateManager.SetEnd(false);
        GameStateManager.SetDifficulty(2);
        GameStateManager.SetCombo("");
        GameStateManager.Playing(true);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //AudioManager.Instance.resetUI();
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
