using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGun : MonoBehaviour
{
    [SerializeField] bool isHealth;
    [SerializeField] Animator animator;

    [SerializeField] int moneyToBuy;

    CoinManager coinMan;

    [SerializeField] Gun gun;

    PlayerMovement player;

    [SerializeField] Text purchaseCostText;

    bool hasBeenBought;

    Tower tower;

    void Start()
    {
        tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        purchaseCostText.text = $"{moneyToBuy} $";
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        coinMan = player.GetComponent<CoinManager>();
    }

    public void PurchaseGun()
    {
        if (hasBeenBought) return;

        if(isHealth)
        {
            if (coinMan.coins >= moneyToBuy)
            {
                
                if (tower.health != tower.maxHealth)
                {
                    tower.health += 25;
                    if(tower.health > tower.maxHealth)
                    {
                        tower.health = tower.maxHealth;
                    }

                    tower.UpdateHealthUI();
                    coinMan.SubstractCoins(moneyToBuy);


                    moneyToBuy *= 3;
                    purchaseCostText.text = $"{moneyToBuy} $";

                    AudioManager.instance.PlayBuySound();
                    animator.Play("PressedButtonAnimation");
                }

            }

            return;
        }

        if(coinMan.coins >= moneyToBuy)
        {
            AudioManager.instance.PlayBuySound();
            animator.Play("PressedButtonAnimation");

            player.EquipGun(gun);
            coinMan.SubstractCoins(moneyToBuy);
            hasBeenBought = true;
        }

    }

}
