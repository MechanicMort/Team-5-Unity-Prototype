using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVPaintShow : MonoBehaviour
{
    [SerializeField]
    private UVPaintController paintController;

    void Awake()
    {
        if (!paintController)
        {
            throw new MissingComponentException();
        }
        paintController.onUVPaintControllerInit += Init;
    }

    void Init()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (!renderer)
        {
            throw new MissingComponentException();
        }

        var rendererPC = paintController.GetComponent<MeshRenderer>();

        renderer.sharedMaterial = rendererPC.sharedMaterial;

        print("Init " + gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
