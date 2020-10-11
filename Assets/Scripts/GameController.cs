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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isDead){
            score+= Time.deltaTime *5f;//quanto maior o num, mais rápido cresce o score
            scoreText.text = Mathf.Round(score).ToString() + "m";
        }
        
    }

    public void ShowGameOver(){
        gameOver.SetActive(true);//ativa e torna visivel a tela de game over
    }

    public void AddCoin(){
        scoreCoins++;
        scoreCoinText.text = "Coin: "+scoreCoins.ToString();
    }
}
