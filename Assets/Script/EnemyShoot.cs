using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public float cooldownTime = 0;
    public float maxCooldownTime = 4;
    // Start is called before the first frame update
    void Start()
    {
        cooldownTime = maxCooldownTime*1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTime -= Time.deltaTime;
        while(cooldownTime <= 0){
            Shoot();
            cooldownTime = maxCooldownTime;
        }
    }

    void Shoot(){
        Instantiate( bullet, transform.position, Quaternion.identity);
    }
}
