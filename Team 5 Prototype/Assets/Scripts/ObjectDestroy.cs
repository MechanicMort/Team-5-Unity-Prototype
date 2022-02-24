using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this.gameObject, 5f);
    }
}
