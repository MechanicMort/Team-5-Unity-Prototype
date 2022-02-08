using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public float ammoCount;
    public float magazineSize;
    public Transform origin;
    public GameObject bullet;
    public float speed = 50f;
    public Text ammoCountText;
    public float fireRate;
    public float fireRateCounter;
    public float damage;

    private void Start()
    {
        ammoCount = magazineSize;
        fireRateCounter = fireRate;
        StartCoroutine(fireRateController());
    }
    private void Update()
    {
        ammoCountText.text = ammoCount + "/" + magazineSize;
        if (Input.GetButton("Fire1") && fireRateCounter <= 0 && ammoCount !=0)
        {
            ammoCount -= 1;
            fireRateCounter = fireRate;
            ShootingBullet();
        }
    }

    private IEnumerator fireRateController()
    {
        yield return new WaitForSeconds(0.01f);
        fireRateCounter -= 0.01f;
        StartCoroutine(fireRateController());
    }

    void ShootingBullet()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject proj = Instantiate(bullet, origin.position, bullet.transform.rotation);
            proj.GetComponent<DamagePlayer>().damageDelt = damage;
            proj.GetComponent<DamagePlayer>().whatTeam =gameObject.layer;

            Rigidbody rig = proj.GetComponent<Rigidbody>();

            rig.AddForce(origin.forward * speed, ForceMode.Impulse);
        }
        
    }
}
