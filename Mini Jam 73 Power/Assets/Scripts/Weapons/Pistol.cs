using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : DistanceWeapon
{
    [SerializeField] Transform t;

    public override void Shoot()
    {
        GameObject bullet = Instantiate(shooting.bulletPrefab, t.position, t.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(t.transform.right * shooting.bulletForce, ForceMode2D.Impulse);
    }
}
