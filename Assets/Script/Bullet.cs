using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 7;
    public float lifeTime = 1.5f;

    public GameObject destoryEffect;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * speed;
    }

    void DestroyBullet(){
        Instantiate(destoryEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D ( Collider2D collision ){
        if(collision.gameObject.tag == "Enemy"){
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Hit();
            DestroyBullet();
        }
    }
}
