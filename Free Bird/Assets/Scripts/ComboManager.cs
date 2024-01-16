using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] comboHolders;                      //gameobject of each combo piece
    [SerializeField]
    Sprite[] comboSprites;                          //should be total of 12
    
    int currentNumOfCombos;                         //current number of combos for the difficulty
    [SerializeField] int numOfEasyCombos;           //num of different easy combos
    [SerializeField] int numOfMidCombos;            //num of different mid combos
    [SerializeField] int numOfHardCombos;           //num of different hard combos
    int maxComboForLevel;

    int lengthOfCombo;                              //how long is the combo
    [SerializeField] int current = 0;               //which positionin the combo is selected
    string currentCombo = "";                       //for generating new combo
    char clickedChar;
    char selected;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GetGameStatus() && !GameStateManager.GetWin())  //Playing the game
        {
            Debug.Log("length of combo: " + lengthOfCombo);
            if (currentCombo.Length != lengthOfCombo)
            {
                NextCombo();

                Debug.Log("length of current combo: " + currentCombo.Length);
            }

            if(numOfCompletedCombo == maxComboForLevel) //move to next difficulty
            {
                numOfCompletedCombo = 0;
                GameStateManager.SetDifficulty(lengthOfCombo + 2);
                lengthOfCombo = GameStateManager.GetDifficulty();
                maxComboForLevel = lengthOfCombo;
                //start coroutine after this
                NextCombo();
            }

            if(current < lengthOfCombo)
            {
                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    CharClicked();
                    selected = currentCombo[current];

                    if (clickedChar == selected) //same (?)
                    {
                        current++;

                        if(current ==  lengthOfCombo)
                        {
                            numOfCompletedCombo++;
                            GenerateNewCombo() ;
                        }
                    }
                    else if (clickedChar != selected) 
                    {
                        current = 0;
                    }
                }
            }
                                 
        }


        else //not playing the game
        {

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

    void FillWithSprite()
    {

    }
    void NextCombo()
    {
        GenerateNewCombo();
        ActivateNeededComboHolders();
        DifficultyChecker();
    }    
    
}
