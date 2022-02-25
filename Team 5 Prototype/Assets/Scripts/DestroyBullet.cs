using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public GameObject obj;
    public GameObject paintSplatter;
    //public GameObject particles;

    private void Awake()
    {
        Destroy(obj, 3f);
    }
    private void OnTriggerEnter(Collider collision)
    {
        //Instantiate(particles, this.transform.position, Quaternion.identity);
        Destroy(obj);        
    }
}
