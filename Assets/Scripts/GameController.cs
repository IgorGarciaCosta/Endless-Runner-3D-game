using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float score;
    public int scoreCoins;
    public Text scoreCoinText;
    private Player player;

    public Text scoreText;
    public GameObject gameOver;
    
    
    void Start()
    {
        Time.timeScale = 1;//despausa jogo
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    
    
    void Update()
    {
    //if(!player.isDead){
        score+= Time.deltaTime *5f;//quanto maior o num, mais rápido cresce o score
        scoreText.text = Mathf.Round(score).ToString() + "m";
    //}
        
    }


    public void ShowGameOver(){
        gameOver.SetActive(true);//ativa e torna visivel a tela de game over
        Time.timeScale = 0;//pausa jogo
    }

    public void AddCoin(){
        scoreCoins++;
        scoreCoinText.text = "Coin: "+scoreCoins.ToString();
    }
}
