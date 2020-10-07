using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    //lista que vai guardar as plataformas pra serem carregadas à frente
    public List<GameObject> platforms = new List<GameObject>();//prefabs

    public List<Transform> currentPlatforms = new List<Transform>();//objetos instanciados
    public int offset;//distância entre as plataformas

    private Transform player;
    private Transform currentPlatformPoint;
    private int platformIndex;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i=0; i<platforms.Count; i++){
            //obj, posição e rotação
            Transform p = Instantiate(platforms[i], new Vector3(0, 0, i*86), transform.rotation).transform;//instancia plataforma
            currentPlatforms.Add(p);
            offset +=86;//86 é o tamanho de uma plataforma
        }

        currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
    }

    // Update is called once per frame
    void Update()
    {
        //quando o player passar por cima do ponto, o distance vira 0 e passa a ser positivo
        float distance = player.position.z - currentPlatformPoint.position.z;

        if(distance>=5){//passou da plataforma
            //recebe a plataforma que vai ser reposicionada
            Recycle(currentPlatforms[platformIndex].gameObject);
            platformIndex++;

            //zera o index antes de chegar em 3, senão dá erro
            if(platformIndex>currentPlatforms.Count-1){
                platformIndex = 0;
            }

            currentPlatformPoint = currentPlatforms[platformIndex].GetComponent<Platform>().point;
        }
    }

    public void Recycle(GameObject platform){
        platform.transform.position = new Vector3(0, 0, offset);
        offset+=86;
    }
}
