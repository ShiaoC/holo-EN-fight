using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFollow : Enemy
{

    private Transform target;
    private float FaceSide = 0;

    // Start is called before the first frame update
    void Start(){
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update(){
        
        if(gotHurt){
            if(hurtTime>0){
                hurtTime -= Time.deltaTime;
                transform.position = new Vector2(transform.position.x + FaceSide/40 , transform.position.y + movelength);
            }
            else{
                coll.enabled = true;
                gotHurt = false;
            }
            
        }
        else{
           if(Vector2.Distance(transform.position, target.position) > 0.2f){
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                FaceSide = (transform.position.x - target.position.x);
                if(FaceSide >=0 )   FaceSide = 1.0f;
                else                FaceSide = -1.0f;
                transform.localScale =new Vector3(FaceSide , 1, 1);
            } 
        }
        
    }
}
