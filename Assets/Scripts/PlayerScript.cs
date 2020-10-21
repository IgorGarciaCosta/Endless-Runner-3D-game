using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody rb;
    public float speed;
    public float laneSpeed;
    private int currentLane = 1;

    public float rayRadius;
    public LayerMask coinLayer;
    public LayerMask layer;
    public bool isDead;
    public Animator anim;
    private GameController gc;
    private Vector3 verticalTargetPosition = new Vector3(0, 1, 0);
    // Start is called before the first frame update
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

        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed*Time.deltaTime);

        
    
    }

    private void FixedUpdate(){
        rb.velocity = Vector3.forward*speed;
    }

    void ChangeLane(int direction){
        int targetLane = currentLane+direction;
        if(targetLane<-4||targetLane>6){
            return;
        }
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane-1), 1, 0);
    }

    void OnCollision(){
        RaycastHit hit;
        //mandando uma direção de origem e uma de destino pro Raycast(), retorna o obj no "hit"
        //manda dps o tamanho do raio e a layer onde deve estar
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRadius, layer) && !isDead){
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.green, 20, true);
            anim.SetTrigger("die");//executa animação de morte
            speed = 0;
            //jumpHeigth = 0;
            //horizontalSpeed = 0;
            Invoke("GameOver", 3f);
            isDead = true;
        }

        RaycastHit coinHit;
        
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out coinHit, rayRadius, coinLayer)){
            gc.AddCoin();
            Destroy(coinHit.transform.gameObject);
        }


    }
    void GameOver(){
        gc.ShowGameOver();
    }
}
