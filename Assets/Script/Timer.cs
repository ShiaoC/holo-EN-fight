using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{
    [Header("Timer UI refences :")]
    [SerializeField]    private Image UIFullImage;//裡面那圈會轉ㄉ
    [SerializeField]    private Text UIText;//暫存次數
    [SerializeField]    private GameObject store;//暫存次數

    [SerializeField]    private int typeNum = 0;

    public int Duration { get; private set;} //Read-only variable

    private int remainingDuration ; // 0 <= remainingDuaring <= Duaring

    private void Awake() {
        ResetTimer(); 
    }

    private void ResetTimer(){
        //UIText.text = "00:00";
        UIFullImage.fillAmount = 0f;

        Duration = remainingDuration = 0;
    }

    public Timer SetDuration(int num){            //設置總共秒數
        Duration = remainingDuration = GameDataManager.usefulTime[ num ];
        return this;
    }

    public void UpdateUI( int type ){
        //UIText.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
        // " :D2 " 就算是個位數數字也會輸出兩個數字

        if( GameDataManager.storeMax[type] > 1 && GameDataManager.storeNow[type] != 0){
            store.SetActive(true);
            UIText.text = string.Format( "{0:D1}", GameDataManager.storeNow[type] );
        }
        else{
            store.SetActive(false);
        }

        UIFullImage.fillAmount = Mathf.InverseLerp(0, GameDataManager.usefulTime[type], GameDataManager.nowTime[type]);
    }

    public void End(){
        ResetTimer();
    }

    private void OnDestroy() {
        StopAllCoroutines();
    }

}