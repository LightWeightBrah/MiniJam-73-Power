using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damageAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damageable = other.GetComponent<IDamagable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }

        Destroy(gameObject);
    }
}

