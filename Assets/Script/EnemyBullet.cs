using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7;
    //public float lifeTime = 2.0f;

    private Transform player;
    private Vector2 target;

    public GameObject destoryEffect;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2( player.position.x, player.position.y);

        //Invoke("DestroyBullet", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyBullet();
        }
        //transform.position = -transform.right * Time.deltaTime * speed;
    }

    void DestroyBullet(){
        Instantiate(destoryEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D ( Collision2D collision ){
        if(collision.gameObject.tag == "Player"){
            //Debug.Log("Hit");
            PlayerController pp = collision.gameObject.GetComponent<PlayerController>();
            GameDataManager.PlayerHealth -= (int)Random.Range(30, 50);
            pp.Hit();
            DestroyBullet();
        }
    }
}
