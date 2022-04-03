using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : DistanceWeapon
{
    public Transform[] firePoints;

    public override void Shoot()
    {
        foreach(Transform t in firePoints)
        {
            GameObject bullet = Instantiate(shooting.bulletPrefab, t.position, t.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(t.transform.right * shooting.bulletForce, ForceMode2D.Impulse);
        }

        
    }
}