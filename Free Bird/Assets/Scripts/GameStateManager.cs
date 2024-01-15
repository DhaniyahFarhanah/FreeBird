using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I'mma be honest idk how this works but I wanna change "play" and "not playing" So I can lock cursor to the confines of the space. Help I'm rusty in code
public class GameStateManager
{
    //Singleton method ig  idk if necessary
    /*private static GameStateManager instance;

    //constructor
    public GameStateManager() { }

    //check instance, if already exists, don't create new
    public static GameStateManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameStateManager();
            }

            return instance;
        }
    }*/

    public static bool gameStart = false;
    public static bool win = false;
    public static int numOfSuccessfulCombos = 0;
    public static int stage; //1 is easy, 2 is mid, 3 is difficult


    public static void Playing(bool start)
    {
        gameStart = start;
    }

    public static bool GetGameStatus()
    {
        return gameStart;
    }

    public static void SetWin(bool success)
    {
        win = success;
    }

    public static bool GetWin()
    {
        return win;
    }

    public static void SetNumOfSuccessfulCombos(int difficultyStage)
    {
        numOfSuccessfulCombos = difficultyStage;
    }

    public static int GetNumOfSuccessfulCombos()
    {
        return numOfSuccessfulCombos;
    }

    public static void SetDifficulty(int setdifficulty)
    {
         stage = setdifficulty;
    }

    public static int GetDifficulty()
    {
        return stage;
    }






}
