using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVPaintManager : MonoBehaviour
{
    private static UVPaintManager __instance = null;
    static public UVPaintManager instance
    {
        get
        {
            return __instance;
        }
    }

    private void Awake()
    {
        __instance = this;
    }

    public Material matPaint;
    public RenderTexture rtTemp;
    
}
