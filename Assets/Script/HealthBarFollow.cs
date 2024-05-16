using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : HealthBar{

    public Camera camera;
    public GameObject enemy;

    public Canvas canvas;

    void Start(){
        FollowObject();
        SetMaxHealth(1);
    }

    void Update(){
        FollowObject();
    }

    public void changeEnemy( GameObject E ){
        enemy = E;
    }

    private void FollowObject(){
        Vector2 pos = camera.WorldToScreenPoint(enemy.transform.position);
        //Debug.Log(pos.x);
        transform.position = new Vector3(pos.x, pos.y+54, 0);
    }
}
