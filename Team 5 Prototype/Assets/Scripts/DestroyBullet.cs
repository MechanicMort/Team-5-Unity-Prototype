using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public GameObject obj;
    public GameObject paintSplatter;
    public ParticleSystem particle;


    private void OnTriggerEnter(Collider collision)
    {
        particle.Play();
        Destroy(obj);
        
    }
}
