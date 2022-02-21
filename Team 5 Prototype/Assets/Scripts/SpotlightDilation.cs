using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightDilation : MonoBehaviour
{

    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        light.spotAngle = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity.magnitude;
    }
}
