using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject EndMenu;

    public static Menu instance;

    private void Awake(){
        instance = this;
    }
    

    public void PauseGame(){
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueGame(){
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EndGame(){
        EndMenu.SetActive(true);
        /*bestScoreObject.SetActive(true);
        nowScoreObject.SetActive(false);*/
        Time.timeScale = 0f;

        if(GameDataManager.Score > GameDataManager.BestScore){
            GameDataManager.BestScore = GameDataManager.Score;
        }

    }
}
