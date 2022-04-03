using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject gameOverScreen;
    public float health;
    public float maxHealth;

    [SerializeField] Image healthFill;

    private void Awake()
    {
        gameOverScreen.gameObject.SetActive(false);

        maxHealth = health;
        UpdateHealthUI();
        Debug.Log("Health is " + health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        Debug.Log("Tower's current health  is " + health);

        UpdateHealthUI();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealthUI()
    {
        if (health < 0)
        {
            health = 0;
            healthFill.fillAmount = 0f;

            gameOverScreen.gameObject.SetActive(true);
        }
        else
        {
            healthFill.fillAmount = health / maxHealth;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
