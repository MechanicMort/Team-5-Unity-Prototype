using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityLifts : MonoBehaviour
{
    Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        rb = GetComponent<Rigidbody>();

        if (collision.gameObject.CompareTag("BoostPad"))
        {
            rb.AddForce(Vector3.up * 45f, ForceMode.Impulse);
        }
    }
}
