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
        json["mesh"] = MyMeshExporter.ProcMeshName(renderer.GetComponent<MeshFilter>().sharedMesh.name);
        json["materials"] = new JsonData();
        for (int count = 0; count < renderer.sharedMaterials.Length; ++count) 
        {
            json["materials"].Add(0);
            json["materials"][count] = renderer.sharedMaterials[count].name;
        }
        
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
        json["colourAndIntensity"][3] = light.intensity * 3;
        json["castShadow"] = (light.shadows > 0);

        return true;
    }

    public static bool ColliderToJson(Collider collider, out JsonData json, bool invertZ = false)
    {
        json = new JsonData();
        int colliderType = -1;
        if      (collider.GetType() == typeof(SphereCollider))
        {
            colliderType = 0;
            json["typeCollider"] = colliderType;
            Vector3 center = ((SphereCollider)collider).center;
            if (invertZ)
                center.z = -center.z;
            ExporterHelper.AddVal("center", center, ref json);
            json["radius"] = ((SphereCollider)collider).radius;
        }
        else if (collider.GetType() == typeof(BoxCollider))
        {
            colliderType = 1;
            json["type"] = colliderType;
            Vector3 center = ((BoxCollider)collider).center;
            if (invertZ)
                center.z = -center.z;
            ExporterHelper.AddVal("center", center, ref json);
            ExporterHelper.AddVal("halfXYZ", ((BoxCollider)collider).size/2.0f, ref json);

        }
        else if (collider.GetType() == typeof(CapsuleCollider))
        {
            colliderType = 2;
            json["type"] = colliderType;
            Vector3 center = ((CapsuleCollider)collider).center;
            if (invertZ)
                center.z = -center.z;
            ExporterHelper.AddVal("center", center, ref json);
            json["radius"] = ((CapsuleCollider)collider).radius;
            json["halfHeight"] = ((CapsuleCollider)collider).height/2.0f - ((CapsuleCollider)collider).radius;
            //Note: Only support Y dir capsules. 
        }
        else
        {
            throw new NotImplementedException();
        }

        json["material"] = new JsonData();
        float staticFriction = 0.0f;
        float dynamicFriction = 0.0f;
        float restitution = 0.0f;
        if (collider.sharedMaterial)
        {
            staticFriction = collider.sharedMaterial.staticFriction;
            dynamicFriction = collider.sharedMaterial.dynamicFriction;
            restitution = collider.sharedMaterial.bounciness;
        }

        json["material"]["staticFriction"] = staticFriction;
        json["material"]["dynamicFriction"] = dynamicFriction;
        json["material"]["restitution"] = restitution;

        return true;
    }
}
