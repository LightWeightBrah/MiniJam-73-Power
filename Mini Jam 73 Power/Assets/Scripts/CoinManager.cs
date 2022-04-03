using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    public int coins;
    public Text coinText;

    bool isPaused;

    PlayerMovement player;

    [SerializeField] Shop shop;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        coinText.text = coins.ToString();
    }

    private void Update()
    {
        if(shop.isBuying == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinsUI();
    }

    public void SubstractCoins(int amount)
    {
        coins -= amount;
        UpdateCoinsUI();
    }

    public void UpdateCoinsUI()
    {
        coinText.text = coins.ToString();
    }

    public void Pause()
    {
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;

        player.isPaused = true;
        isPaused = true;

    }
    public void Resume()
    {
        AudioManager.instance.PlayButtonSound();
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;

        player.isPaused = false;
        isPaused = false;
    }
}
