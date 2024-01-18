using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject[] comboHolders;     //gameobject of each combo piece
    [SerializeField] Sprite[] comboSprites;         //should be total of 12
    [SerializeField] GameObject endScreen;
    SpriteChanger spriteChange;
    
    [SerializeField] int numOfEasyCombos;           //num of different easy combos
    [SerializeField] int numOfMidCombos;            //num of different mid combos
    [SerializeField] int numOfHardCombos;           //num of different hard combos
    int maxComboForLevel;

    int lengthOfCombo;                              //how long is the combo
    [SerializeField] int current = 0;               //which positionin the combo is selected
    string currentCombo = "";                       //for generating new combo
    char clickedChar;                               //recording what character the player has clicked
    char selected;                                  //index of which is selected
    bool toShow;                                    //check to display the combo images
    bool toClick;                                   //checks if the combo will record the clicks if wrong

    [SerializeField] float delay;

    int numOfCompletedCombo;

    Dictionary<char, KeyCode> comboButtons = new Dictionary<char, KeyCode>();
    List<GameStateManager.ComboStates> comboStates = new List<GameStateManager.ComboStates>();

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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GetGameStatus() && !GameStateManager.GetEnd() && toShow && toClick)  //Playing the game
        {
            if (currentCombo.Length != lengthOfCombo) //Generate new combo because of 
            {
                StartCoroutine(WaitForNextCombo());
            }

            if(numOfCompletedCombo == maxComboForLevel) //move to next difficulty
            {
                numOfCompletedCombo = 0;
                GameStateManager.SetDifficulty(lengthOfCombo + 2);
                lengthOfCombo = GameStateManager.GetDifficulty();
                maxComboForLevel = lengthOfCombo;
                GenerateNewCombo();
                ActivateNeededComboHolders();
                DifficultyChecker();

            }

            if(current < lengthOfCombo)
            {
                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    CharClicked();
                    selected = currentCombo[current];

                    if (clickedChar == selected) //Correct character
                    {
                        //change sprite to green 
                        spriteChange = comboHolders[current].GetComponent<SpriteChanger>();
                        spriteChange.image.GetComponent<Image>().color = Color.green;

                        current++; //move to next character


                        if(current == lengthOfCombo) //End of combo, move to next
                        {
                            numOfCompletedCombo++;
                            StartCoroutine(WaitForNextCombo());
                        }
                    }
                    else if (clickedChar != selected) //Not correct character
                    {
                        //change sprite to red
                        spriteChange = comboHolders[current].GetComponent<SpriteChanger>();
                        spriteChange.image.GetComponent<Image>().color = Color.red;
                        StartCoroutine(WrongCharacter());
                    }
                }
            }

            if(GameStateManager.GetDifficulty() > 6)
            {
                //End game you win!. Probably will check if the bird is not dead lol
                endScreen.SetActive(true);
                EmptyCanvas();
                GameStateManager.Playing(false);
            }                                 
        }

        else //not playing the game
        {

        }

        if (GameStateManager.GetEnd())
        {
            PlayDedAnim();
            StopAllCoroutines();
            Cursor.visible = true;

        }
    }

    void CharClicked()
    {
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
        currentCombo = ""; //clear previous combo
        comboStates.Clear(); //clear previous states
        current = 0;

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

            comboStates.Add(GameStateManager.ComboStates.empty);
        }

        GameStateManager.SetCombo(currentCombo);
        toShow = true;
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
    }

    IEnumerator WaitForNextCombo()
    {
        toShow = false;

        yield return new WaitForSeconds(1f);

        EmptyCanvas();

        yield return new WaitForSeconds(delay);

        GenerateNewCombo();
        ActivateNeededComboHolders();
        DifficultyChecker();

        toShow = true;
    }

    IEnumerator WrongCharacter()
    {
        toClick = false;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < comboHolders.Length; i++)
        {
            spriteChange = comboHolders[i].GetComponent<SpriteChanger>();
            spriteChange.image.GetComponent<Image>().color = Color.white;
        }

        current = 0;
        toClick = true;
    }
    
}
