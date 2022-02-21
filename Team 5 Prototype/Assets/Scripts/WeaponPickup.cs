using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponStats weapon;


    private void Start()
    {
        this.GetComponent<MeshFilter>().mesh = weapon.weaponMesh;
    }

    private void Update()
    {
        transform.RotateAround(transform.position, new Vector3(0,1,0), 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().heldWeapon = weapon;
        }
    }
}
