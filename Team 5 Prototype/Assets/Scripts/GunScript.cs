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
    public float shots;
    public Light spotLight;
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
        for (int i = 0; i < shots; i++)
        {
            float radius = Mathf.Tan(Mathf.Deg2Rad * spotLight.spotAngle / 2) * spotLight.range;
            Vector2 circle = Random.insideUnitCircle * radius;
            Vector3 target = spotLight.transform.position + spotLight.transform.forward * spotLight.range + spotLight.transform.rotation * new Vector3(circle.x, circle.y);

            
            GameObject proj = Instantiate(bullet, origin.position, bullet.transform.rotation);

            proj.GetComponent<DamagePlayer>().damageDelt = damage;
            proj.GetComponent<DamagePlayer>().whatTeam =gameObject.layer;
            proj.GetComponent<ProjectilePaint>().team = gameObject.layer;
            Rigidbody rig = proj.GetComponent<Rigidbody>();
            proj.transform.LookAt( target);
            rig.AddForce(Vector3.MoveTowards(transform.position,target,speed) ,ForceMode.VelocityChange);

        }
        
    }
}
