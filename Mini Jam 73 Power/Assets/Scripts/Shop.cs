using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject buyInteraction;
    [SerializeField] GameObject buyPage;

    bool canBuy;

    public bool isBuying;

    PlayerMovement player;

    void Start()
    {
        buyInteraction.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (canBuy && Input.GetKeyDown(KeyCode.E))
        {
            if(isBuying == false)
            {
                OpenBuyingPage();
            }
            else
            {
                CloseBuyingPage();
            }
        }

    }

    public void OpenBuyingPage()
    {
        Cursor.visible = true;
        buyPage.gameObject.SetActive(true);
        isBuying = true;
        Time.timeScale = 0f;
        player.isPaused = true;
    }

    public void CloseBuyingPage()
    {
        AudioManager.instance.PlayButtonSound();
        buyPage.gameObject.SetActive(false);
        isBuying = false;
        Time.timeScale = 1f;
        player.isPaused = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            buyInteraction.gameObject.SetActive(true);

            canBuy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            buyInteraction.gameObject.SetActive(false);

            canBuy = false;
        }
    }
}
