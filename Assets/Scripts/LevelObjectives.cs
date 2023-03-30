using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelObjectives : MonoBehaviour
{

    public static bool RespawningAtoms = false;

    public static int RespawnNumber = 0;
    
    public static int LevelShots;
    public static int LevelSpawn;
    public static int InitialSpawn = 30;

    public static int ShotsAllowed = 3;

    public static string LeftText;
    public static string RightText;

    private static TextMeshProUGUI scoreBoard;
    
	void Start ()
	{
	    Time.timeScale = 1;

        scoreBoard = GameObject.Find("ScoreBoard").GetComponent<TextMeshProUGUI>();
	    scoreBoard.SetText(Data.Score.ToString());

        if (Data.Respawns == 0)
	    {
	        Data.Respawns = 1;
	    }

        Debug.Log(Data.Respawns);

	    LevelSpawn = 31;
	    LevelShots = 3;

	    InitialSpawn = LevelSpawn - Data.Respawns;

	    ShotsAllowed = LevelShots - (Data.Respawns / 5);

        /*RespawningAtoms = false;
	    RespawnNumber = 0;

	    InitialSpawn = 25;



        Debug.Log(Data.Respawns);*/



	    LeftText = "Atoms: ";
	    RightText = "Shots: ";
    }

    public static void Score(int score)
    {
        if (!Data.Endgame)
        {
            Data.Score = Data.Score + (score*Data.Respawns);
            Debug.Log(Data.Score);
        }
        scoreBoard.SetText(Data.Score.ToString());
    }
}
