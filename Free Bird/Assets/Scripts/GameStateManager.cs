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

    public static void Playing(bool start)
    {
        gameStart = start;
    }

    public static bool GetGameStatus()
    {
        return gameStart;
    }


}
