using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public Transform origin;
    public GameObject bullet;
    public float speed = 50f;
    public Text ammoCountText;

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
