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
    public GameObject waterNae;
    public GameObject paintNade;
    public float speed;
    public float shots;
    public Light spotLight;
    public Text ammoCountText;
    public float fireRate;
    public float fireRateMod = 1.0f;
    public float fireRateCounter;
    public float damage;
    public float accuracy;
    public float recoilAmount;
    public bool isSquid;

    private void Start()
    {
        ammoCount = magazineSize;
        fireRateCounter = fireRate;
        StartCoroutine(fireRateController());
    }
    private void Update()
    {
        //ammoCountText.text = ammoCount + "/" + magazineSize;
        spotLight.GetComponent<SpotlightDilation>().accuracy = accuracy;

    }

    private IEnumerator fireRateController()
    {
        yield return new WaitForSeconds(0.01f);
        fireRateCounter -= 0.01f;
        StartCoroutine(fireRateController());
    }

    public  void Reload()
    {
        ammoCount = magazineSize;

    }
    public void ShootBullet()
    {
        if (fireRateCounter <= 0 && ammoCount != 0 && isSquid == false)
        {
            ammoCount -= 1;
            fireRateCounter = fireRate / fireRateMod;
            for (int i = 0; i < shots; i++)
            {
                float radius = Mathf.Tan(Mathf.Deg2Rad * spotLight.spotAngle / 2) * spotLight.range;
                Vector2 circle = Random.insideUnitCircle * radius;
                Vector3 target = spotLight.transform.position + spotLight.transform.forward * spotLight.range + spotLight.transform.rotation * new Vector3(circle.x, circle.y);


                GameObject proj = Instantiate(bullet, origin.position, bullet.transform.rotation);

                proj.GetComponent<DamagePlayer>().damageDelt = damage;
                proj.GetComponent<DamagePlayer>().whatTeam = gameObject.layer;
                proj.GetComponent<ProjectilePaint>().team = gameObject.layer;
                proj.transform.LookAt(target);
                Rigidbody rig = proj.GetComponent<Rigidbody>();
                rig.AddForce(proj.transform.forward * speed, ForceMode.VelocityChange);


            }
            spotLight.GetComponent<SpotlightDilation>().Recoil(recoilAmount);
        }
        
        
    }

    public void ThrowNade(string nadeType)
    {

                float radius = Mathf.Tan(Mathf.Deg2Rad * spotLight.spotAngle / 2) * spotLight.range;
                Vector2 circle = Random.insideUnitCircle * radius;
                Vector3 target = spotLight.transform.position + spotLight.transform.forward * spotLight.range + spotLight.transform.rotation * new Vector3(circle.x, circle.y);
                GameObject proj;
                if (nadeType == "Water")
                {
                    proj = Instantiate(waterNae, origin.position, bullet.transform.rotation);
            proj.GetComponent<Nades>().team = 0;
        }
                else
                {
                    proj = Instantiate(paintNade, origin.position, bullet.transform.rotation);
            proj.GetComponent<Nades>().team = gameObject.layer;
        }


                proj.transform.LookAt(target);
                Rigidbody rig = proj.GetComponent<Rigidbody>();
                rig.AddForce(proj.transform.forward * speed, ForceMode.VelocityChange);

            
    }

    }

