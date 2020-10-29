using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;
    public float laneSpeed;
    private int currentLane = 1;
    private bool jumping = false;
    private float jumpStart;
    public float jumpLength;
    public float jumpHeight;

    public float rayRadius;
    public LayerMask coinLayer;
    public LayerMask layer;
    public bool isDead;
    public Animator anim;
    private GameController gc;
    private Vector3 verticalTargetPosition = new Vector3(0, 1, 0);
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            ChangeLane(-5);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            ChangeLane(5);
        }

        else if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

        if(jumping){
            float ratio = (transform.position.z-jumpStart)/jumpLength;
            if(ratio>=1f){
                jumping = false;

            }
            else{
                verticalTargetPosition.y = Mathf.Sin(ratio*Mathf.PI)*jumpHeight;
            }
        }

        else{//se estiver no chão
            verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 1, 5*Time.deltaTime);
        }

        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed*Time.deltaTime);

    }

    private void FixedUpdate(){
        rb.velocity = Vector3.forward*speed;//corre pra frente
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Death"){
            anim.SetTrigger("die");//executa animação de morte
            speed = 0;
            jumpHeight = 0;
            jumpLength = 0;
            Invoke("GameOver", 3f);
            isDead = true;
        }
    }

    void OnTriggerEnter(Collider moeda){
        if(moeda.tag == "Coin"){
            gc.AddCoin();
            //Destroy(moeda.transform.gameObject);
            
            
        }
    }

    void ChangeLane(int direction){//muda a trilha de corrida
        int targetLane = currentLane+direction;
        if(targetLane<-4||targetLane>6){
            return;
        }
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane-1), 1, 0);
    }

    void Jump(){
        if(!jumping){
            jumpStart = transform.position.z;
            jumping = true;
        }
    }


    
    void GameOver(){
        gc.ShowGameOver();
    }

    
}
