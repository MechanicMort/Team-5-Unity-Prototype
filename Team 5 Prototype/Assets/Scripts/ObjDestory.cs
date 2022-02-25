using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestory : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this.gameObject, 1f);
    }
}
