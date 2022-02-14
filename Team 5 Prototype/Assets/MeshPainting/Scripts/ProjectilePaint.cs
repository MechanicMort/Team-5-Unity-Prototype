using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ProjectilePaint : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Vector3 dir = transform.TransformDirection(Vector3.down).normalized;
            var paintController = hit.transform.GetComponent<UVPaintController>();
            if (paintController != null)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Vector3 mask = Input.GetMouseButton(0) ? new Vector3(1, 0, 0) : new Vector3(0, 1, 0);
                Debug.Log("Did Hit");
                paintController.PaintOnGO(hit.point, dir, mask, 1);
            }
        }



    }
}
