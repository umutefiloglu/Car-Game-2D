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

//int i = 0;
//if (i == 1)
//{
//    //oyun başladı
//}
//else if (i == 2)
//{
//    //oyun bitti
//}

//GameState state = GameState.s2_NotStarted;
//if (state == GameState.s3_Playing)
//{
//    //oyun başladı
//}
//else if (state == GameState.s7_Finished)
//{
//    //oyun bitti
//}

//public enum kiyafet
//{
//    tisort,
//    manto,
//    corap
//}

//kiyafet k;
//k = kiyafet.tisort;
//if (k == kiyafet.corap)
//{
//    //çorabımı giymişim
//}


[System.Serializable]
public struct Car
{
    public Vector3[] carPos;
    public Quaternion[] carRot;
    public GameObject carGO;
    public GameObject startGO;
    public GameObject finishGO;
}

//[SerializeField]
//private int a;

//string oyuncu1_isim = "Ahmet";
//string oyuncu1_silah = "tabanca";
//int oyuncu1_age = 5;
//string oyuncu2_isim = "Mehmet";
//string oyuncu2_silah = "bıçak";
//int oyuncu2_age = 15;

//public struct Oyuncu
//{
//    public string isim;
//    public string silah;
//    public int age;

//    public Oyuncu(string i, string s, int a)
//    {
//        isim = i;
//        silah = s;
//        age = a;
//    }
//}
//Oyuncu oyuncu1 = new Oyuncu();
//oyuncu1.isim = "Ahmet";
//oyuncu1.silah = "tabanca";

//Oyuncu oyuncu2 = new Oyuncu("Mehmet", "bıçak", 99);

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
