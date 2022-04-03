using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DistanceWeapon : MonoBehaviour //abstract means we can't instantiate it
{
    protected Shooting shooting;

    protected void Awake()
    {
        shooting = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
    }

    protected void OnEnable()
    {
        Shooting.OnShoot += Shoot;
    }

    protected void OnDisable()
    {
        Shooting.OnShoot -= Shoot;
    }

    public virtual void Shoot()
    {
        GameObject bullet = Instantiate(shooting.bulletPrefab, shooting.firePoint.position, shooting.firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(shooting.firePoint.transform.right * shooting.bulletForce, ForceMode2D.Impulse);
    }
}
