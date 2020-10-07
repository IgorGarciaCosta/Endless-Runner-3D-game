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
    void Start()
    {
        //pega o capsule collider do plyer
        controller = GetComponent<CharacterController>();
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
        }

        else{//se está no ar, decresce a gravidade da vel. do pulo
            jumpVelocity-=gravity;
        }

        direction.y = jumpVelocity;

        controller.Move(direction*Time.deltaTime);
    }
}
