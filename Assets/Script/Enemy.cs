using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //protected Animator animator;
    protected AudioSource deathAudio;
    public Collider2D coll;
    protected bool gotHurt = false;
    protected float hurtTime = 0 , hurtTimeMax = 0.3f;
    public float movelength = 0;
    public float lengthWithPlayer = 0.0f;

    public float speed;
    
    public HealthBar healthbar;
    private int health = GameDataManager.EnemyMaxHealth[ GameDataManager.nextEnemyStyle ];
    private int maxHealth = GameDataManager.EnemyMaxHealth[ GameDataManager.nextEnemyStyle ];
    public GameObject destoryEffect;
    //public GameObject healthBar;

    protected virtual void Start(){
        //animator = GetComponent<Animator>();
        //deathAudio = GetComponent<AudioSource>();
        coll = GetComponent<Collider2D>();
        healthbar.SetMaxHealth(health);
    }

    public void Hit(){
        coll.enabled = false;
        
        //deathAudio.Play();
        gotHurt = true;
        hurtTime = hurtTimeMax;
        movelength = Random.Range( -0.05f, 0.05f);
        health -=  (int)Random.Range(30, 80);
        healthbar.SetHealth( maxHealth, health );
        //Debug.Log(health);
        /*healthbar.SetHealth(health);*/

        if(health <= 0){
            Death();
        }
    }

    private void Death(){
        Instantiate(destoryEffect, transform.position, Quaternion.identity);
        //animator.SetTrigger ("death");
        healthbar.Death();
        Destroy(gameObject);
    }

    public void ChangeHealthBar(HealthBar H){
        healthbar = H;
    }
}
