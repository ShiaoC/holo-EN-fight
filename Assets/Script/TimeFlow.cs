using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFlow : MonoBehaviour{
    public Timer[] t;

    void Start() {
        for(int i=0;i<1;i++){
            t[i].SetDuration(i);
        }
        
        InvokeRepeating("timer", 0.01f, 0.01f);
        //InokeRepeating 重複呼叫(“函式名”，第一次間隔幾秒呼叫，每幾秒呼叫一次)。
    }

    void timer(){
        for(int i=0 ; i<4 ; i++){
            if( GameDataManager.storeNow[i] < GameDataManager.storeMax[i] ){
                if( GameDataManager.nowTime[i] == 0 ){
                    GameDataManager.storeNow[i] += 1;
                    GameDataManager.nowTime[i] = GameDataManager.usefulTime[i];
                }
                else if( GameDataManager.nowTime[i] == -1 ){
                    GameDataManager.nowTime[i] = GameDataManager.usefulTime[i];
                }
                else{
                    GameDataManager.nowTime[i] --;
                    //Debug.Log(GameDataManager.nowTime[2]);
                }
            }
            else{
                GameDataManager.nowTime[i] = -1;
                //Debug.Log(GameDataManager.nowTime[i]);
            }
            t[i].UpdateUI( i );
/*

            if( GameDataManager.nowTime[i] == 0 ){
                GameDataManager.storeNow[i] += 1 ;
                if( GameDataManager.storeNow[i] < GameDataManager.storeMax[i] ){
                    GameDataManager.nowTime[i] = GameDataManager.usefulTime[i];
                }
            }*/
            /*else{
                GameDataManager.nowTime[i] -= 1;
                
            }*/


        }
    }

    /*public static void UsedAttack(int num){
        if()
    }*/


}
