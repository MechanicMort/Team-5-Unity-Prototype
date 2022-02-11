using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nades : MonoBehaviour
{
    public Bombs bombs;

    private void Start()
    {
        bombs.hasExploded = false;
        bombs.countDown = bombs.delay;
    }

    private void Update()
    {
        bombs.countDown -= Time.deltaTime;
        if(bombs.countDown <= 0f && !bombs.hasExploded)
        {
            Explode();
            bombs.hasExploded = true;
        }
    }

    void Explode()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, bombs.radius);
        foreach(Collider nearBy in collider)
        {
            Rigidbody rb = nearBy.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(bombs.force, transform.position, bombs.radius);
            }
        }

        Destroy(this.gameObject);
    }
}
