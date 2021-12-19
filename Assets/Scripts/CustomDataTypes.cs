using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    s1_Loading,
    s2_NotStarted,
    s3_Playing,
    s4_NextState,
    s5_CalculatePlayerStats,
    s6_SaveGame,
    s7_Finished,
    s_Failed
}

[System.Serializable]
public struct Car
{
    public Vector3[] carPos;
    public Quaternion[] carRot;
    public GameObject carGO;
    public GameObject startGO;
    public GameObject finishGO;
}


[System.Serializable]
public struct LevelInfo
{
    public GameObject[] stagesGO;
    public Car[] cars;
    

    public LevelInfo(int numOfStages)
    {
        cars = new Car[numOfStages];
        stagesGO = new GameObject[numOfStages];
    }
}