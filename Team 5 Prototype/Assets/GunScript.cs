using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform origin;
    public GameObject bullet;
    public float speed = 50f;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            ShootingBullet();
        }
    }

    void ShootingBullet()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject proj = Instantiate(bullet, origin.position, bullet.transform.rotation);
            Rigidbody rig = proj.GetComponent<Rigidbody>();

            rig.AddForce(origin.forward * speed, ForceMode.Impulse);
        }
        
    }
}
