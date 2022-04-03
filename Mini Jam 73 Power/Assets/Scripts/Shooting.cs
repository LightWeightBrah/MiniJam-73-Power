using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float timeBetweenBullets = 0.1f;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    float bulletsInerval;

    public static event Action OnShoot;

    PlayerMovement player;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (player.isPaused) return;
        if (bulletsInerval > 0) { bulletsInerval -= Time.deltaTime; return; }
        if(Input.GetButton("Fire1"))
        {
            Shoot();
            bulletsInerval = timeBetweenBullets;
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(firePoint.transform.position);
        }
    }

    void Shoot()
    {
        AudioManager.instance.PlayShootingSound();
        OnShoot?.Invoke();
    }
}
