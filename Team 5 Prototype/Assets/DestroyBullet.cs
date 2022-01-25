using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public GameObject obj;

    // Start is called before the first frame update
    private void Update()
    {
        Destroy(obj, 2);
    }    
}
