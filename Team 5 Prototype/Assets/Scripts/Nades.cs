using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nades : MonoBehaviour
{
    public Bombs bombs;
    public float team;
    private void Start()
    {
        bombs.hasExploded = false;
        bombs.countDown = bombs.delay;
        StartCoroutine(Timer());
    }



    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.01f);
        bombs.countDown -= 0.01f;
        StartCoroutine(Timer());
        if (bombs.countDown <= 0f && !bombs.hasExploded)
        {
            Explode();
            bombs.hasExploded = true;
        }



    }
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
    void Explode()
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, bombs.radius);
            foreach (Collider nearBy in collider)
            {
                Rigidbody rb = nearBy.GetComponent<Rigidbody>();
                if (rb != null)
                {
                if (nearBy.tag == "Player")
                {
                    nearBy.GetComponent<PlayerInputController>().TakeDamage(bombs.damage);
                }
                    rb.AddExplosionForce(bombs.force, transform.position, bombs.radius);
                }
            }
            Vector3 mask = new Vector3(0, 0, 0); //: new Vector3(0, 1, 0);
            if (team == 10)
            {
                mask = new Vector3(1, 0, 0); //: new Vector3(0, 1, 0);
            }
            else if (team == 11)
            {
                mask = new Vector3(0, 1, 0); //: new Vector3(0, 1, 0);
            }
            // Does the ray intersect any objects excluding the player layer
            var colliders = Physics.OverlapSphere(transform.position, 1.5f);
            if (colliders.Length > 0)
            {
                foreach (var cld in colliders)
                {
                    Vector3 dir = GetComponent<Rigidbody>().velocity;
                    var paintController = cld.transform.GetComponent<UVPaintController>();
                    if (paintController != null)
                    {
                        paintController.PaintOnGO(transform.position, dir, mask, 1);
                        paintController.PaintOnGO(transform.position + new Vector3(2.5f, 0, 0), dir, mask, 1);
                        paintController.PaintOnGO(transform.position + new Vector3(0, 0, 2.5f), dir, mask, 1);
                        paintController.PaintOnGO(transform.position + new Vector3(0, 0, -2.5f), dir, mask, 1);
                        paintController.PaintOnGO(transform.position + new Vector3(-2.5f, 0, 0), dir, mask, 1);
                        paintController.PaintOnGO(transform.position + new Vector3(5f, 0, 0), dir, mask, 1);
                        paintController.PaintOnGO(transform.position + new Vector3(0, 0, 5f), dir, mask, 1);
                        paintController.PaintOnGO(transform.position + new Vector3(0, 0, -5f), dir, mask, 1);
                        paintController.PaintOnGO(transform.position + new Vector3(-5f, 0, 0), dir, mask, 1);
                    }
                    print("Exploded");
                }
            }
            Destroy(this.gameObject);
       
    }
}
