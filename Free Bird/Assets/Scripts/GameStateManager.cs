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
    public static int stage = 2; //2 is easy, 4 is mid, 6 is difficult. 8 is the worst It's the amount of combo pieces per difficulty
    public static string currentCombo = "";
    public static bool arrows = true;

    public static void Playing(bool start)
    {
        gameStart = start;
    }

    public static bool GetGameStatus()
    {
        return gameStart;
    }

    public static void SetEnd(bool success)
    {
        win = success;
    }

    public static bool GetEnd()
    {
        return win;
    }

    public static void SetDifficulty(int setdifficulty)
    {
         stage = setdifficulty;
    }

    public static int GetDifficulty()
    {
        return stage;
    }

    public static void SetCombo(string combo)
    {
        currentCombo = combo;
    }
    
    public static string GetCombo()
    {
        return currentCombo;
    }

    public static void SetControls(bool controls)
    {
        arrows = controls;
    }

    public static bool GetControls()
    {
        return arrows;
    }

}
