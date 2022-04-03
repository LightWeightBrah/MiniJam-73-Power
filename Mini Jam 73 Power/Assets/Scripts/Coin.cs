using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float speed;

    GameObject player;

    CoinManager coinMan;

    bool shouldMove;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        coinMan = player.GetComponent<CoinManager>();
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 10f)
        {
            shouldMove = true;
        }

        if(shouldMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, player.transform.position) < 0.5f)
        {
            AudioManager.instance.PLayCoinSound();
            coinMan.AddCoins(Random.Range(1, 6));
            Destroy(gameObject);
        }

    }
}
