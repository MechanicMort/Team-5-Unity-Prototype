using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public GameObject obj;
    public GameObject paintSplatter;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            GameObject temp = Instantiate(paintSplatter, collision.GetContact(0).point, Quaternion.FromToRotation(-Vector3.forward, collision.GetContact(0).point));

            //temp.transform.Rotate(Vector3.forward * 90);
            //temp.transform.LookAt(obj.transform.position);
            //temp.transform.Translate(-Vector3.forward * 0.1f);
            
            Destroy(obj);


        }
    }
}
