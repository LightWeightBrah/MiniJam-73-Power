using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    string gunName = "Weapon";

    public GameObject gun;
    public GameObject bulletPrefab;

    public Transform firePoint;

    public float timeBetweenBullets;

    public float bulletForce = 20f;

    Shooting shooting;


    public void Spawn(Transform handTransform)
    {
        DisableOldWeapon(handTransform);

        GameObject weapon = Instantiate(gun, handTransform);
        weapon.name = gunName;
        firePoint = weapon.GetComponentInChildren<Transform>();

        SetShooting();

        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.gunSr = weapon.GetComponent<SpriteRenderer>();
    }

    private void SetShooting()
    {
        shooting = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();

        shooting.bulletPrefab = bulletPrefab;
        shooting.firePoint = firePoint;
        shooting.timeBetweenBullets = timeBetweenBullets;
        shooting.bulletForce = bulletForce;
    }

    private void DisableOldWeapon(Transform handTransform)
    {
        Transform oldWeapon = handTransform.Find(gunName);

        foreach(Transform t in handTransform)
        {
            t.gameObject.SetActive(false);
        }
    }
}
