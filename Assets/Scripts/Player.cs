using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    
    public float speed;
    public float jumpHeigth;
    public float gravity;
    private float jumpVelocity;

    public float rayRadius;
    public LayerMask layer;
    public LayerMask coinLayer;

    public float horizontalSpeed;
    private bool isMovingLeft;
    private bool isMovingRight;

    public Animator anim;
    public bool isDead;
    private GameController gc;
    void Start()
    {
        //pega o capsule collider do plyer
        controller = GetComponent<CharacterController>();
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //add eixo z (a frente do personagem)
        Vector3 direction = Vector3.forward * speed;

        if(controller.isGrounded){//saber se está tocando do não
            //confere se pressionou espaco
            if(Input.GetKeyDown(KeyCode.Space)){
                jumpVelocity = jumpHeigth;
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -0.5f && !isMovingLeft){
                isMovingLeft = true;
                StartCoroutine(LeftMove());
            }

            //confere se a tecla foi apertada e se o personagem não chegou ainda ao limite de distância pra não cair
            if(Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 0.5f && !isMovingRight){
                isMovingRight = true;
                StartCoroutine(RightMove());
            }
        }

        else{//se está no ar, decresce a gravidade da vel. do pulo
            jumpVelocity-=gravity;
        }

        OnCollision();

        direction.y = jumpVelocity;

        controller.Move(direction*Time.deltaTime);

       
    }

    //corrotinas para movimentação
    IEnumerator LeftMove(){
        
        for(float i = 0; i<10; i+=0.1f){//100 iterações

            //val de -1 no eixo x
            controller.Move(Vector3.left*Time.deltaTime*horizontalSpeed);
            yield return null;
        }
        isMovingLeft = false;
    }

    IEnumerator RightMove(){
        for(float i=0; i<10; i+=0.1f){
            //val de 1 no eixo x
            controller.Move(Vector3.right*Time.deltaTime*horizontalSpeed);
            yield return null;
        }
        isMovingRight = false;
    }


    void OnCollision(){
        RaycastHit hit;
        //mandando uma direção de origem e uma de destino pro Raycast(), retorna oobj no "hit"
        //manda dps o tamanho do raio e a layer onde deve estar
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRadius, layer) && !isDead){
            anim.SetTrigger("die");//executa animação de morte
            speed = 0;
            jumpHeigth = 0;
            horizontalSpeed = 0;
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