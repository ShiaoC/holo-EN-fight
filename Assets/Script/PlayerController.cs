using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //public static float GameDataManager.PlayerSpeed;
    public Collider2D idleColl;
    public Collider2D attackColl;
    public Collider2D GPColl;

    public HealthBar healthbar;

    private Rigidbody2D rb; 
    private Vector2 moveVelocity;

    private Animator animator;
    private bool isAttack = false;
    //private bool gotHurt = false;
    private float hurtTime = 0,  hurtTimeMax = 0.5f;
    private bool GPAttack = false;
    private int GPTime = 0,  GPTimeMax = 1000; //ground pound
    private bool sprint = false;
    public GameObject bullet;
    public Transform shotPoint;

    bool checkClick = true;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameDataManager.PlayerHealth = GameDataManager.PlayerMaxHealth;

    }

    // Update is called once per frame
    void Update(){
        Movement();
        //SwitchAnime();
        Attack();
        
        if(GameDataManager.PlayerHealth <= 0){
            Menu.instance.EndGame();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);   
    }

    void Movement(){

        //if(!gotHurt){
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * GameDataManager.PlayerSpeed;

            if(Input.GetAxisRaw("Horizontal") != 0 ){
                if(Input.GetAxisRaw("Horizontal") > 0){
                    rb.transform.rotation = Quaternion.Euler(0.0f,180.0f,0.0f);
                }
                else if(Input.GetAxisRaw("Horizontal") < 0){
                    rb.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
                }
                animator.SetBool ("isMoving", true);
            }
            else if(Input.GetAxisRaw("Vertical") != 0){
                animator.SetBool ("isMoving", true);
            }
            else{
                animator.SetBool ("isMoving", false);
            }
        //}
        
    }

    void Attack(){
        //------------------打擊攻擊------------------
        if(Input.GetAxisRaw("Fire1") != 0 && GameDataManager.storeNow[0] != 0 && checkClick){
            GameDataManager.storeNow[0] -= 1;
            checkClick = false;

            animator.SetBool ("isAttack", true);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ame_attack2")){
            isAttack = true;
            attackColl.enabled = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ame_attack3") && !checkClick){
            animator.SetBool ("isAttack", false);
            isAttack = false;
            checkClick = true;

            attackColl.enabled = false;
            
            //Debug.Log("attack over");
        }

        //------------------ground pound攻擊------------------
        if( Input.GetAxisRaw("Fire2")!=0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Ame_GroundPound") && GameDataManager.storeNow[1] != 0 && checkClick){
            GameDataManager.storeNow[1] -= 1;
            checkClick = false;

            animator.SetBool ("isGP", true);
            GPTime = GPTimeMax;
            idleColl.enabled = false;
            //Input.GetAxisRaw("Fire2") = 0f;
        }
        if( animator.GetCurrentAnimatorStateInfo(0).IsName("Ame_GroundPound") ){
            if( GPTime >0 )  GPTime --;
            if( GPTime == 0 || Input.GetAxisRaw("Fire2")!=0 ){
                GPTime = 0;
                GPAttack = true;
                animator.SetBool ("isGP", false);
                isAttack = true;
                GPColl.enabled = true;
                
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack over")){
            animator.SetBool ("isAttack", false);
            isAttack = false;
            checkClick = true;

            idleColl.enabled = true;
            GPColl.enabled = false;
            
        }
        
        //------------------射擊攻擊------------------
        if(Input.GetAxisRaw("Fire3")!=0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Ame_Shoot") && GameDataManager.storeNow[2] != 0 && checkClick){
            GameDataManager.storeNow[2] -= 1;
            checkClick = false;

            animator.SetBool("isShoot", true);   //用trigger會連射
            Instantiate(bullet, shotPoint.position, transform.rotation);

        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Ame_Shoot")){
            animator.SetBool("isShoot", false);   //用trigger會連射
            checkClick = true;

            //Debug.Log("end shoot!");
        }

        //------------------衝撞攻擊------------------
        if(Input.GetAxisRaw("Jump")!=0 && GameDataManager.storeNow[3] != 0 && checkClick){
            GameDataManager.storeNow[3] -= 1;
            checkClick = false;

            isAttack = true;
            attackColl.enabled = true;
            sprint = true;

            GameDataManager.PlayerSpeed *= 3f;

            animator.SetBool("isSprint", true);
        }
        else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Ame_sprint") && sprint){
            animator.SetBool("isSprint", false);
            checkClick = true;

            GameDataManager.PlayerSpeed /= 3f;

            isAttack = false;
            attackColl.enabled = false;
            sprint = false;
        }
    }
    
    public void Hit(){
        healthbar.SetHealth( GameDataManager.PlayerMaxHealth, GameDataManager.PlayerHealth );
    }

    /*void SwitchAnime(){
        if(gotHurt){
            if(hurtTime == 0){
                gotHurt = false;
                //animator.SetBool ("hit", gotHurt);
            }
            else{
                hurtTime -= Time.deltaTime;
            }
        }
    }*/

    private void OnTriggerEnter2D ( Collider2D collision ){
        if(collision.gameObject.tag == "Enemy"){
            //Debug.Log("enemy");
            //-----------被敵人撞到=受傷------------
            if(!isAttack){
                if(transform.position.x > collision.gameObject.transform.position.x){
                    rb.velocity = new Vector2(+10, 0 );
                    //gotHurt = true;

                    GameDataManager.PlayerHealth -=  (int)Random.Range(90, 150);

                    healthbar.SetHealth( GameDataManager.PlayerMaxHealth, GameDataManager.PlayerHealth );
                    //Debug.Log(GameDataManager.PlayerHealth);
                    //hurtTime = hurtTimeMax;
                }
                else{
                    rb.velocity = new Vector2(-10, 0 );
                    //Debug.Log("hurt");
                    //hurtTime = hurtTimeMax;
                }

            }
            //-----------打到敵人------------
            else{
                ///////////////////////加分還沒寫
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.Hit();
                //Debug.Log("attack success!!!!!!!!!!!!!");
            }
        }
    }

}
