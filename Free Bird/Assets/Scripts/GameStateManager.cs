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
    public static bool gameEnd = false;
    public static bool win = false;
    public static int stage = 2; //2 is easy, 4 is mid, 6 is difficult. 8 is the worst It's the amount of combo pieces per difficulty
    public static string currentCombo = "";
    public static bool cutscene = true;
    public static bool arrows = true;
    public static int difficultyArea = 1;
    public static float BGMvolume = 0.5f;
    public static float SFXvolume = 0.5f;

    public static int perfect = 0;
    public static int good = 0;
    public static int bad = 0;

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
        gameEnd = success;
    }

    public static bool GetEnd()
    {
        return gameEnd;
    }

    public static void SetWin(bool winner)
    {
        win = winner;
    }

    public static bool GetWin()
    {
        return win;
    }

    public static void SetCutscene(bool done)
    {
        cutscene = done;
    }

    public static bool GetCutscene()
    {
        return cutscene;
    }

    public static void SetDifficulty(int setdifficulty)
    {
         stage = setdifficulty;
    }

    public static int GetSectionCode()
    {
        return difficultyArea;
    }

    public static void SetSectionCode(int eep)
    {
        difficultyArea = eep;
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

    //audio stuff
    public static void SetVolumes(float bgmVolume, float sfxVolume)
    {
        BGMvolume = bgmVolume;
        SFXvolume = sfxVolume;
    }

    public static float GetBGMVolume()
    {
        return BGMvolume;
    }

    public static float GetSFXVolume()
    {
        return SFXvolume;
    }

    //leaderboard stuff
    public static int GetPerfCombo()
    {
        return perfect;
    }

    public static void SetPerfCombo(int perf)
    {
        perfect = perf;
    }

    public static int GetGoodCombo()
    {
        return good;
    }

    public static void SetGoodCombo(int gd)
    {
        good = gd;
    }

    public static int GetBadCombo()
    {
        return bad;
    }

    public static void SetBadCombo(int bd)
    {
        bad = bd;
    }

}
