using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ProjectilePaint : MonoBehaviour
{
    public float team;

    // Update is called once per frame
    void Update()
    {
        Vector3 mask = new Vector3(0, 0, 0); //: new Vector3(0, 1, 0);

        RaycastHit forwardHit;
        // Does the ray intersect any objects excluding the player layer
        var colliders = Physics.OverlapSphere(transform.position, 1.5f);
        if (colliders.Length>0)
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out forwardHit, 1))
        {
            foreach(var cld in colliders)
            {
                Vector3 dir =GetComponent<Rigidbody>().velocity;
                var paintController = cld.transform.GetComponent<UVPaintController>();
                if (paintController != null)
                {
                    //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * forwardHit.distance, Color.yellow);
                    if (team == 10)
                    {
                        mask = new Vector3(1, 0, 0); //: new Vector3(0, 1, 0);
                    }
                    else
                    {
                        mask = new Vector3(0, 1, 0); //: new Vector3(0, 1, 0);
                    }
                    paintController.PaintOnGO(transform.position, dir, mask, 1);
                }
            }
            
        }


    }
}
