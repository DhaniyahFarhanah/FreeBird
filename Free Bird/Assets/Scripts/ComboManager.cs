using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject[] comboHolders;     //gameobject of each combo piece
    SpriteChanger spriteChange;

    [Header("Combo Info")]

    [SerializeField] int numOfEasyCombos;           //num of different easy combos
    [SerializeField] int numOfMidCombos;            //num of different mid combos
    [SerializeField] int numOfHardCombos;           //num of different hard combos
    [SerializeField] int numOfInsaneCombos;         //num of different insane combos
    [SerializeField]int maxDifficultyInt;           //difficulty int (it's 8 for now)
    int maxComboForLevel;

    [Header("Skill Display")]

    [SerializeField] GameObject skill;              //gameobject that holds skill. Mainly for animation stuff
    [SerializeField] TMP_Text skillDisplay;         //gameobject for 
    int skillLevel = 0;                             //Skill. Each time fail the combo, count up. 0 means perfect

    int lengthOfCombo;                              //how long is the combo
    [SerializeField] int current = 0;               //which positionin the combo is selected
    string currentCombo = "";                       //for generating new combo
    char clickedChar;                               //recording what character the player has clicked
    char selected;                                  //index of which is selected
    bool toShow;                                    //check to display the combo images
    bool toClick;                                   //checks if the combo will record the clicks if wrong
    [SerializeField] float delay;

    [Header("Flight Bar Stuff")]

    [SerializeField] Image flightBarBarlol;
    [SerializeField] GameObject rightWing;
    [SerializeField] GameObject leftWing;
    [SerializeField] float lerpSpeed;

    int startedKeys = 0;
    int currentKeyShowcase = 0;
    int totalNumOfKeys = 0;
    float toBeFilledAmt = 0f;

    int numOfCompletedCombo;

    Dictionary<char, KeyCode> comboButtons = new Dictionary<char, KeyCode>();

// Start is called before the first frame update
    void Start()
    {
        comboButtons.Add('W', KeyCode.W);
        comboButtons.Add('A', KeyCode.A);
        comboButtons.Add('S', KeyCode.S);
        comboButtons.Add('D', KeyCode.D);

        lengthOfCombo = GameStateManager.GetDifficulty();
        maxComboForLevel = lengthOfCombo;
        toShow = true;
        toClick = true;
        skill.SetActive(false);
        skillLevel = 0;
        totalNumOfKeys = numOfEasyCombos * 2 + numOfMidCombos * 4 + numOfHardCombos * 6 + numOfInsaneCombos * 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GetGameStatus() && !GameStateManager.GetEnd() && toShow && toClick && !GameStateManager.GetCutscene())  //Playing the game
        {
            if (currentCombo.Length != lengthOfCombo) //Generate new combo because of 
            {
                StartCoroutine(WaitForNextCombo());
            }

            if(numOfCompletedCombo == maxComboForLevel && !GameStateManager.GetWin()) //move to next difficulty
            {
                numOfCompletedCombo = 0;
                GameStateManager.SetDifficulty(lengthOfCombo + 2);
                lengthOfCombo = GameStateManager.GetDifficulty();

                DifficultyChecker();
                GenerateNewCombo();
                ActivateNeededComboHolders();
            }

            if(current < lengthOfCombo)
            {
                if (GameStateManager.GetControls()) //false arrow means wasd
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        CharClickedArrow();
                        selected = currentCombo[current];

                        if (clickedChar == selected) //Correct character
                        {
                            //change sprite to green 
                            spriteChange = comboHolders[current].GetComponent<SpriteChanger>();
                            spriteChange.image.GetComponent<Image>().color = Color.green;
                            currentKeyShowcase++;

                            current++; //move to next character

                            if (current == lengthOfCombo) //End of combo, move to next
                            {
                                numOfCompletedCombo++;
                                if (numOfCompletedCombo == numOfInsaneCombos && GameStateManager.GetDifficulty() == maxDifficultyInt)
                                {
                                    GameStateManager.SetWin(true);
                                    GameStateManager.Playing(false);
                                    GameStateManager.SetEnd(true);
                                }
                                else
                                {
                                    StartCoroutine(WaitForNextCombo());
                                    AudioManager.Instance.PlaySFX("Correct");
                                }
                            }
                        }
                        else if (clickedChar != selected) //Not correct character
                        {
                            //change sprite to red
                            skillLevel++;
                            spriteChange = comboHolders[current].GetComponent<SpriteChanger>();
                            spriteChange.image.GetComponent<Image>().color = Color.red;
                            StartCoroutine(WrongCharacter());
                            AudioManager.Instance.PlaySFX("Wrong");
                            currentKeyShowcase = startedKeys;
                        }
                    }
                }
                else if (!GameStateManager.GetControls())
                {

                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                    {
                        CharClickedWASD();
                        selected = currentCombo[current];

                        if (clickedChar == selected) //Correct character
                        {
                            //change sprite to green 
                            spriteChange = comboHolders[current].GetComponent<SpriteChanger>();
                            spriteChange.image.GetComponent<Image>().color = Color.green;
                            currentKeyShowcase++;

                            current++; //move to next character

                            if (current == lengthOfCombo) //End of combo, move to next
                            {
                                numOfCompletedCombo++;
                                if (numOfCompletedCombo == numOfInsaneCombos && GameStateManager.GetDifficulty() == maxDifficultyInt)
                                {
                                    GameStateManager.SetWin(true);
                                    GameStateManager.Playing(false);
                                    GameStateManager.SetEnd(true);
                                }
                                else
                                {
                                    StartCoroutine(WaitForNextCombo());
                                    AudioManager.Instance.PlaySFX("Correct");
                                }
                            }
                        }
                        else if (clickedChar != selected) //Not correct character
                        {
                            //change sprite to red
                            skillLevel++;
                            spriteChange = comboHolders[current].GetComponent<SpriteChanger>();
                            spriteChange.image.GetComponent<Image>().color = Color.red;
                            StartCoroutine(WrongCharacter());
                            AudioManager.Instance.PlaySFX("Wrong");
                            currentKeyShowcase = startedKeys;
                        }
                    }
                }
            }   
        }

        else //not playing the game
        {

        }

        toBeFilledAmt = currentKeyShowcase / (totalNumOfKeys * 1.0f);

        if (flightBarBarlol.fillAmount != toBeFilledAmt)
        {
            //lerp the bar?
            flightBarBarlol.fillAmount = Mathf.Lerp(flightBarBarlol.fillAmount, toBeFilledAmt, Time.deltaTime * lerpSpeed);
        }

        if (GameStateManager.GetEnd() && !GameStateManager.GetWin()) //lose
        {
            skill.SetActive(true);
            skillDisplay.text = "Dumbass";
            skillDisplay.color = Color.white;
            PlayDedAnim();
            StopAllCoroutines();
            Cursor.visible = true;
        }
        if(GameStateManager.GetEnd() && GameStateManager.GetWin()) //win
        {
            StopAllCoroutines();
            EmptyCanvas();
            GameStateManager.Playing(false);
            Cursor.visible = true;
        }
    }

    void CharClickedWASD()
    {
        AudioManager.Instance.PlaySFX("Click");
        if (Input.GetKeyDown(KeyCode.W))
        {
            clickedChar = 'W';
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            clickedChar = 'A';
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            clickedChar = 'S';
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            clickedChar = 'D';
        }
    }
    void CharClickedArrow()
    {
        AudioManager.Instance.PlaySFX("Click");
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            clickedChar = 'W';
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            clickedChar = 'A';
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            clickedChar = 'S';
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            clickedChar = 'D';
        }
    }

    void ActivateNeededComboHolders()
    {
        for(int i = 0; i < comboHolders.Length; i++)
        {
            if(i < lengthOfCombo)
            {
                comboHolders[i].SetActive(true);
            }
            else
            {
                comboHolders[i].SetActive(false);
            }
        }
    }

    void GenerateNewCombo()
    {
        skill.SetActive(false);
        skillLevel = 1;
        currentCombo = ""; //clear previous combo
        current = 0;
        startedKeys = currentKeyShowcase;

        for (int i = 0; i < lengthOfCombo; i++)
        {
            switch (Random.Range(0, 4))
            {
                case 0: 
                    currentCombo += "W";
                    break;
                case 1:
                    currentCombo += "A";
                    break;
                case 2:
                    currentCombo += "S";
                    break;
                case 3:
                    currentCombo += "D";
                    break;
            }

        }

        GameStateManager.SetCombo(currentCombo);
    }

    void DifficultyChecker()
    {
        switch (GameStateManager.GetDifficulty())
        {
            case 2:
                maxComboForLevel = numOfEasyCombos;
                break;
            case 4:
                maxComboForLevel = numOfMidCombos;
                break;
            case 6:
                maxComboForLevel = numOfHardCombos;
                break;
            case 8:
                maxComboForLevel = numOfInsaneCombos;
                break;
        }
    }

    void EmptyCanvas()
    {
        for (int i = 0; i < comboHolders.Length; i++)
        {
            comboHolders[i].SetActive(false);          
        }
    }

    void PlayDedAnim()
    {
        for (int i = 0; i < comboHolders.Length; i++)
        {
            if (comboHolders[i].activeInHierarchy)
            {
                comboHolders[i].GetComponent<Animator>().SetBool("end", true);
            }
        }

        skill.GetComponent<Animator>().SetBool("end", true);
    }

    IEnumerator WaitForNextCombo()
    {
        toShow = false;

        yield return new WaitForSeconds(1f);

        EmptyCanvas();
        SkillDisplay();
        skill.SetActive(true);
        ResetComboToWhite();

        yield return new WaitForSeconds(delay);

        GenerateNewCombo();
        DifficultyChecker();
        ActivateNeededComboHolders();

        skill.SetActive(false);
        toShow = true;
    }

    IEnumerator WrongCharacter()
    {
        toClick = false;

        yield return new WaitForSeconds(0.5f);

        ResetComboToWhite();

        current = 0;
        toClick = true;
    }

    void ResetComboToWhite()
    {
        for (int i = 0; i < comboHolders.Length; i++)
        {
            spriteChange = comboHolders[i].GetComponent<SpriteChanger>();
            spriteChange.image.GetComponent<Image>().color = Color.white;
        }
    }

    void SkillDisplay()
    {
        if(skillLevel == 1)
        {
            skillDisplay.text = "Perfect!!";
            skillDisplay.color = Color.magenta;
        }
        else if(skillLevel > 1 && skillLevel < 4)
        {
            skillDisplay.text = "Good!";
            skillDisplay.color = Color.green;
        }
        else if (skillLevel >= 4)
        {
            skillDisplay.text = "Skill Issue";
            skillDisplay.color = Color.red;
        }
        else
        {
            skillDisplay.text = "";
            skillDisplay.color = Color.white;
        }
        
    }

}

