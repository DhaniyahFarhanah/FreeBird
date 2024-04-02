using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] Animator cutsceneAnim;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject lossCanvas;
    [SerializeField] GameObject winCanvas;
    [SerializeField] AudioSource musicSource;
    [SerializeField] GameObject hud;

    //Leaderboard
    [SerializeField] TMP_Text perfComboAmt;
    [SerializeField] TMP_Text goodComboAmt;
    [SerializeField] TMP_Text badComboAmt;
    [SerializeField] Image endCutsceneImg;
    [SerializeField] Sprite secretEnd;
    [SerializeField] Sprite normEnd;
    [SerializeField] TMP_Text endQuote;

    [SerializeField] float cutsceneDelay;
    [SerializeField] float endCutsceneDelay;

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
        if (GameStateManager.GetGameStatus() && GameStateManager.GetCutscene())
        {
            StartGame();
            startMenu.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !GameStateManager.GetEnd())
        {
            Pause();
        }

        if (GameStateManager.GetEnd() && !GameStateManager.GetWin()) //loss
        {
            AudioManager.Instance.Normalize();
            AudioManager.Instance.PlaySFX("Lose");
            StartCoroutine(ShowEnd());
            Cursor.visible = true;
        }
        else if (GameStateManager.GetEnd() && GameStateManager.GetWin()) //win
        {
            hud.SetActive(false);
            StartCoroutine(EndCutscene());
            winCanvas.SetActive(true);
            perfComboAmt.text = GameStateManager.GetPerfCombo().ToString();
            goodComboAmt.text = GameStateManager.GetGoodCombo().ToString();
            badComboAmt.text = GameStateManager.GetBadCombo().ToString();

            if(GameStateManager.GetGoodCombo() == 0 && GameStateManager.GetBadCombo() == 0)
            {
                endCutsceneImg.sprite = secretEnd;
                endQuote.text = "\"" + "S-son...?" + "\"";
            }
            else
            {
                endCutsceneImg.sprite = normEnd;
                endQuote.text = "\"" + "Fly high...free bird..." + "\"";
            }

            Cursor.visible = true;

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
        }

        else if(isPaused)
        {
            isPaused = false;
            Cursor.visible = false;
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
        }
    }

    IEnumerator EndCutscene()
    {
        yield return new WaitForSeconds(endCutsceneDelay);
        MainMenu();
    }

    IEnumerator ShowEnd()
    {
        yield return new WaitForSeconds(2f);
        lossCanvas.SetActive(true);
    }

    public void StartButton()
    {
        //show the controls choice
        controlPanel.SetActive(true);
    }
    public void wasdChoice()
    {
        GameStateManager.SetControls(false);
        StartGame();
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.PlayMusic("Easy");
    }

    public void arrowChoice()
    {
        GameStateManager.SetControls(true);
        StartGame();
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.PlayMusic("Easy");
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        StartCoroutine(WaitForCutscene());
    }

    IEnumerator WaitForCutscene()
    {
        cutsceneAnim.SetBool("Started", true);
        yield return new WaitForSeconds(cutsceneDelay);
        GameStateManager.SetCutscene(false);
        Cursor.visible = false;
        GameStateManager.Playing(true);
        
    }

    public void RestartButton()
    {
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("Menu");
        GameStateManager.SetEnd(false);
        GameStateManager.SetDifficulty(2);
        GameStateManager.SetCombo("");
        startMenu.SetActive(false);
        GameStateManager.SetCutscene(true);
        GameStateManager.Playing(true);
        GameStateManager.SetWin(false);
        GameStateManager.SetSectionCode(1);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //AudioManager.Instance.resetUI();
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("Menu");
        GameStateManager.SetEnd(false);
        GameStateManager.SetDifficulty(2);
        GameStateManager.SetCombo("");
        startMenu.SetActive(true);
        GameStateManager.SetCutscene(true);
        GameStateManager.Playing(false);
        GameStateManager.SetWin(false);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
