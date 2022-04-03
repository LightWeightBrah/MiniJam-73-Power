using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] float speed;
    [SerializeField] float health;
    [SerializeField] float damage;

    [SerializeField] float minTimeBetweenAttacks;
    [SerializeField] float maxTimeBetweenAttacks;

    float counter;

    GameObject tower;

    [SerializeField] GameObject coinPrefab;

    private void Start()
    {
        tower = GameObject.FindGameObjectWithTag("Tower");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            AudioManager.instance.PlayDieSound();
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (tower.gameObject == null) return;

        transform.position = Vector2.MoveTowards(transform.position, tower.transform.position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position , tower.transform.position) <= 3f)
        {
            if(counter > 0)
            {
                counter -= Time.deltaTime;
            }
            else
            {
                AudioManager.instance.PlayHurtSound();
                Debug.Log("Attacking");
                tower.GetComponent<IDamagable>().TakeDamage(damage);
                counter = Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks);
                Debug.Log("Counter = " + counter);
            }
        }
    }
}
