using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static float    PlayerSpeed = 7f;
    public static int    PlayerMaxHealth = 1000;
    public static int    PlayerHealth = 1000;

    public static float[]    EnemySpeed = { 3.5f, 2.0f, 2.2f, 3f, 1.0f};
    public static int[]   EnemyMaxHealth = { 200, 300 , 100 , 100 , 100};
    public static float[] EnemyLengthWithPlayer = { 0.2f, 0.2f, 0.2f, 0.2f, 2.5f};
    public static int[]    CreateTime = { 5, 4 , 3 , 4, 6};
    public static int    enemyStyleNum = 5; 
    public static int    nextEnemyStyle = 2;
    public static int    newBornPointNum = 4;//總共多少個

    public static int      CreateRandom = 5;

    public static int      Score = 0;
    public static int      BestScore = 0;

    public static int      hurtTime = 0,  hurtTimeMax = 200;
    public static int      GPTime = 0,  GPTimeMax = 1000; //ground pound

    public static int[]      usefulTime = {100, 500, 200, 1000};
    public static int[]      nowTime    = {100, 500, 200, 1000};
    public static int[]      storeMax   = {1, 2, 5, 2};
    public static int[]      storeNow   = {1, 1, 1, 1};
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(PlayerController.speed);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
