using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
using System;

public class GoExporter
{
    public static bool GameObjectToJson(GameObject go, out JsonData root, bool invertHandedness=false)
    {
        root = new JsonData();
        root["id"] = go.GetInstanceID();
        if (go.transform.parent)
        {
            int parentID = 0;
            parentID = go.transform.parent.gameObject.GetInstanceID();
            root["parentId"] = parentID;
        }
        root["name"] = go.name;

        root["transform"] = new JsonData();
        root["transform"]["position"] = new JsonData();
        root["transform"]["position"].Add(0);
        root["transform"]["position"][0] = go.transform.localPosition.x;
        root["transform"]["position"].Add(0);
        root["transform"]["position"][1] = go.transform.localPosition.y;
        root["transform"]["position"].Add(0);
        root["transform"]["position"][2] = invertHandedness ? -go.transform.localPosition.z : go.transform.localPosition.z;
        root["transform"]["orientation"] = new JsonData();
        var rot = go.transform.localRotation;
        if (invertHandedness) { rot.x = -rot.x; rot.y = -rot.y; rot.z = rot.z; }
        root["transform"]["orientation"].Add(0);
        root["transform"]["orientation"][0] = rot.x;
        root["transform"]["orientation"].Add(0);
        root["transform"]["orientation"][1] = rot.y;
        root["transform"]["orientation"].Add(0);
        root["transform"]["orientation"][2] = rot.z;
        root["transform"]["orientation"].Add(0);
        root["transform"]["orientation"][3] = rot.w;
        root["transform"]["scale"] = new JsonData();
        root["transform"]["scale"].Add(0);
        root["transform"]["scale"][0] = go.transform.localScale.x;
        root["transform"]["scale"].Add(0);
        root["transform"]["scale"][1] = go.transform.localScale.y;
        root["transform"]["scale"].Add(0);
        root["transform"]["scale"][2] = go.transform.localScale.z;
        root["transform"]["scale"][2] = go.transform.localScale.z;

        //
        // Components 
        //
            
        JsonData componentsJson = new JsonData();
        bool hasComponent = false;

        //
        //Core Components
        //
        //MeshRenderer
        var mr = go.GetComponent<MeshRenderer>();
        if (mr)
        {
            var componentRoot = new JsonData();
            componentRoot["type"] = "T5PrimitiveObjectComponent"; //renderer.GetType().Name;
            JsonData data;
            if(CoreComponentsExporter.MeshRendererToJson(mr, out data))
            {
                componentRoot["data"] = data;
                componentsJson.Add(componentRoot);
                hasComponent = true;
            }
        }
        //Camera
        var cam = go.GetComponent<Camera>();
        if (cam)
        {
            var componentRoot = new JsonData();
            componentRoot["type"] = "T5CameraComponent";
            JsonData data;
            CoreComponentsExporter.CameraToJson(cam, out data);
            componentRoot["data"] = data;
            componentsJson.Add(componentRoot);
            hasComponent = true;
        }
        //Light
        var light = go.GetComponent<Light>();
        if (light)
        {
            var componentRoot = new JsonData();
            componentRoot["type"] = "T5LightComponent";
            JsonData data;
            CoreComponentsExporter.LightToJson(light, out data);
            componentRoot["data"] = data;
            componentsJson.Add(componentRoot);
            hasComponent = true;
        }
        //Audio Listener
        var listener = go.GetComponent<AudioListener>();
        if (listener)
        {
            var componentRoot = new JsonData();
            componentRoot["type"] = "T5AudioListener";
            componentsJson.Add(componentRoot);
            hasComponent = true;
        }
        //Customed Monos
        var components = go.GetComponents<MonoBehaviour>();
        foreach(var component in components)
        {
            var componentRoot = new JsonData();
            componentRoot["type"] = component.GetType().Name;
            JsonData data;
            if(ExporterHelper.ComponentToJson(component, out data)) //In case there's no param at all
            {
                componentRoot["data"] = data;
            }
            componentsJson.Add(componentRoot);
            hasComponent = true;
        }
        if (!hasComponent)
        {
            componentsJson.SetJsonType(JsonType.Array);
        }

        root["components"] = componentsJson;

        return true;
    }
}
