using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour{
    [SerializeField]    private GameObject[] enemy;
    [SerializeField]    private GameObject enemyHealthBar;
    [SerializeField]    private Transform[] bornPoint;
    /*public HealthBarFollow barScript;
    public Camera camera;
    public Canvas canvas;*/


    private int nextStyle;
    private int nextPosition;
    private float waitTime=0;



    void Start(){
        findNext();

    }

    void Update()
    {
        waitTime -= Time.deltaTime;
        if(waitTime < 0){
            waitTime = GameDataManager.CreateTime[ nextStyle ] + Random.Range(0, GameDataManager.CreateRandom);
            //創造新敵人在特定點
            GameObject newEnemy = Instantiate( enemy[ nextStyle ] , bornPoint [ nextPosition ].position, transform.rotation );
            
            
            GameObject bar = Instantiate( enemyHealthBar );

            bar.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform, false);
            bar.gameObject.GetComponent<HealthBarFollow>().enemy = newEnemy;
            bar.gameObject.GetComponent<HealthBarFollow>().camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            
            newEnemy.gameObject.GetComponent<EnemyFollow>().healthbar = bar.gameObject.GetComponent<HealthBarFollow>();
            newEnemy.gameObject.GetComponent<EnemyFollow>().lengthWithPlayer = GameDataManager.EnemyLengthWithPlayer[ nextStyle ];
            newEnemy.gameObject.GetComponent<EnemyFollow>().speed = GameDataManager.EnemySpeed[ nextStyle ];
            

            findNext();
        }
        //AddSpeed();

    }

    void findNext(){
        //nextStyle = 4;
        nextStyle = (int)Random.Range( 0, 67) * 7 % GameDataManager.enemyStyleNum;
        nextPosition = (int)Random.Range( 0, 41) *13 % GameDataManager.newBornPointNum;
        GameDataManager.nextEnemyStyle = nextStyle;
    }
    











    /*void AddSpeed(){
        if(nextSpeed <= 0){
            nextSpeed = (int)Random.Range(5, 7);
            GamedataManager.Speed += 0.1f;
            menu.instance.SpeedUp( GamedataManager.Speed );
            Debug.Log( GamedataManager.Speed );
        }
    }*/


}