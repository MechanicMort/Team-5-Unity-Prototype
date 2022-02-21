using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightDilation : MonoBehaviour
{

    private Light light;
    public   float accuracy;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        light.spotAngle = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime;
        if (light.spotAngle > accuracy)
        {
            light.spotAngle -= 6f * Time.deltaTime;
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity.magnitude <= 4 && light.spotAngle > accuracy)
        {
            light.spotAngle -= 12f * Time.deltaTime;
        }

    }
    public void Recoil(float recoil)
    {
        light.spotAngle +=recoil;
    }
}
