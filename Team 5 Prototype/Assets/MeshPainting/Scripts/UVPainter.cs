using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVPainter : MonoBehaviour
{
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        if (camera == null)
        {
            throw new MissingComponentException();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)|| Input.GetMouseButton(1))
        {
            Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit info;
            Debug.DrawRay(mouseRay.origin,mouseRay.direction,Color.red, 500);
            if(Physics.Raycast(mouseRay, out info))
            {
                var paintController = info.transform.GetComponent<UVPaintController>();
                if (paintController != null)
                {
                    Vector3 dir = mouseRay.direction.normalized;
                    Vector3 mask = Input.GetMouseButton(0) ? new Vector3(1, 0, 0) : new Vector3(0, 1, 0);
                    paintController.PaintOnGO(info.point, dir, mask, 1);
                }

            }

        }
    }
}
