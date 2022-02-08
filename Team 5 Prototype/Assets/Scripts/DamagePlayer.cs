using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float whatTeam;
    public float damageDelt;

    private void Start()
    {
       
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if ( collision.transform.tag == "Player")
        {
            if (collision.gameObject.layer != whatTeam )
            {
                collision.transform.GetComponent<PlayerController>().TakeDamage(damageDelt);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (collision.gameObject.layer != whatTeam)
            {
                collision.transform.GetComponent<PlayerController>().TakeDamage(damageDelt);
            }
        }
    }
}
