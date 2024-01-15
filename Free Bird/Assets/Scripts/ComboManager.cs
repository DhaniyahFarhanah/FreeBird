using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] comboHolders;                  //gameobject of each combo piece
    [SerializeField]
    Sprite[] comboSprites;                      //should be total of 12
    

    [SerializeField] int numOfEasyCombos;       //num of different easy combos
    [SerializeField] int numOfMidCombos;        //num of different mid combos
    [SerializeField] int numOfHardCombos;       //num of different hard combos

    int currentNumOfCombos;                     //current number of combos for the difficulty
    int numOfGameobjectToShow;                  //how many buttons for the combo
    bool pressedOnce = false;
    [SerializeField] int current = 0;

    List<char> currentCombo = new List<char>();                         //for generating new combo

    int difficulty = 0;                             //difficulty of the game, 1 is easy, 2 is mid, 3 is easy, 4 is win

    Dictionary<char, KeyCode> comboButtons = new Dictionary<char, KeyCode>();

// Start is called before the first frame update
    void Start()
    {
        comboButtons.Add('W', KeyCode.W);
        comboButtons.Add('A', KeyCode.A);
        comboButtons.Add('S', KeyCode.S);
        comboButtons.Add('D', KeyCode.D);
    }

    // Update is called once per frame
    void Update()
    {
        if (difficulty != GameStateManager.GetDifficulty()) //difficulty is changed, change values as well
        {
            ActivateNeededComboHolders();
            CheckDifficultyQteAmount();
            GenerateNewCombo();
        }
        
        if(GameStateManager.GetNumOfSuccessfulCombos() == currentNumOfCombos)
        {
            GameStateManager.SetNumOfSuccessfulCombos(0);
            GameStateManager.SetDifficulty(GameStateManager.GetDifficulty() + 1);
        }

        else
        {
            while(current <= currentCombo.Count)
            {
                if (Input.GetKeyDown(comboButtons[currentCombo[current]]))
                {
                    current++;

                    //change sprite
                    //move on to next
                }
                if (current == numOfGameobjectToShow)
                {
                    //finished combo
                    GenerateNewCombo();
                }
                else
                {
                    break;
                }
                
            }
        }
        Debug.Log(currentCombo.Count);

    }
    void CheckDifficultyQteAmount()
        // This is to check and associate the amount of combos for each level of difficulty
    {
        difficulty = GameStateManager.GetDifficulty();

        switch (difficulty)
        {
            case 1:
                // 4 easy qte, only 2/6 combo gameobjects active
                currentNumOfCombos = numOfEasyCombos;
                numOfGameobjectToShow = 4;
                break;

            case 2:
                // 8 easy qte, only 4/6 combo gameobjects active
                currentNumOfCombos = numOfMidCombos;
                numOfGameobjectToShow = 4;
                break;

            case 3:
                // 2 easy qte, only 2/6 combo gameobjects active
                currentNumOfCombos = numOfHardCombos;
                numOfGameobjectToShow = 6;
                break;

            case 4:
                //Game Win
                GameStateManager.SetWin(true);
                GameStateManager.Playing(false);
                break;

            default: break;
        }
    }
    void ActivateNeededComboHolders()
    {
        for(int i = 0; i < comboHolders.Length; i++)
        {
            if(i < numOfGameobjectToShow)
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
        currentCombo.Clear(); //clear previous combo
        current = 0;

        for (int i = 0; i < numOfGameobjectToShow; i++)
        {
            switch (Random.Range(0, 4))
            {
                case 0: currentCombo.Add('W');
                    break;
                case 1: currentCombo.Add('A');
                    break;
                case 2: currentCombo.Add('S');
                    break;
                case 3: currentCombo.Add('D');
                    break;
            }
        }
    }

}
