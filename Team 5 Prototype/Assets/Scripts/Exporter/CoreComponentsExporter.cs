using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
using System;

using System.Diagnostics;
using System.Reflection;

public class CoreComponentsExporter
{
    public static bool MeshRendererToJson(MeshRenderer renderer, out JsonData json)
    {
        json = new JsonData();
        if (!renderer.GetComponent<MeshFilter>())
        {
            return false;
        }
        json["mesh"] = renderer.GetComponent<MeshFilter>().sharedMesh.name;
        json["material"] = renderer.sharedMaterial.name;
        return true;
    }

    public static bool CameraToJson(Camera camera, out JsonData json)
    {
        json = new JsonData();
        if (camera.orthographic)
        {
            throw new NotImplementedException();
        }
        json["type"] = 1; //0: Orth; 1: Perspective
        json["fovX"] = camera.fieldOfView;
        json["aspectRatio"] = 1.0f/camera.aspect;
        return true;
    }

    public static bool LightToJson(Light light, out JsonData json)
    {
        json = new JsonData();
        int lightType = -1;
        switch (light.type)
        {
            case LightType.Directional:
                {
                    lightType = 0;
                    json["type"] = lightType;
                }
                break;
            case LightType.Point:
                {
                    lightType = 1;
                    json["type"] = lightType;
                    json["radius"] = light.range;
                    json["innerRadius"] = 0.1f;
                }
                break;
            case LightType.Spot:
                {
                    lightType = 2;
                    json["type"] = lightType;
                    json["radius"] = light.range;
                    json["angle"] = light.spotAngle;
                }
                break;
            default:
                throw new NotImplementedException();
        }
        json["colourAndIntensity"] = new JsonData();
        json["colourAndIntensity"].Add(0);
        json["colourAndIntensity"][0] = light.color.r;
        json["colourAndIntensity"].Add(0);
        json["colourAndIntensity"][1] = light.color.g;
        json["colourAndIntensity"].Add(0);
        json["colourAndIntensity"][2] = light.color.b;
        json["colourAndIntensity"].Add(0);
        json["colourAndIntensity"][3] = light.intensity;
        
        return true;
    }
}
