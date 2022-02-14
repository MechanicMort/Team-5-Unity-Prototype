using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public GameObject obj;
    public GameObject paintSplatter;


    private void OnTriggerEnter(Collider collision)
    {
        Destroy(obj);
        
    }
}
