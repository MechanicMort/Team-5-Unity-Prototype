using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
using System;

public class MaterialExporter
{
    public static bool StandardMaterialToJson(Material mat, out JsonData root)
    {
        root = new JsonData();
        root["name"] = mat.name;

        root["shader"] = "pbr";//mat.shader.name;

        root["params"] = new JsonData();

        float metallic = 0.0f;
        float smoothness = 0.0f;
        float aoStrength = 0.0f;

        //s_texColor
        if (mat.mainTexture) 
        {
            JsonData s_texColor = new JsonData();
            s_texColor["name"] = "s_texColor";
            s_texColor["type"] = "texture";
            s_texColor["value"] = mat.mainTexture.name;
            root["params"].Add(s_texColor);
        }

        //s_texNormal
        try
        {
            var texNormalMap = mat.GetTexture("_BumpMap");
            if (texNormalMap)
            {
                JsonData s_texNormal = new JsonData();
                s_texNormal["name"] = "s_texNormal";
                s_texNormal["type"] = "texture";
                s_texNormal["value"] = texNormalMap.name;
                root["params"].Add(s_texNormal);
            }
        }
        catch(Exception)
        {

        }


        //s_texMetallic
        try
        {
            var texMetallic = mat.GetTexture("_MetallicGlossMap");
            if (texMetallic)
            {
                JsonData s_texMetallic = new JsonData();
                s_texMetallic["name"] = "s_texMetallic";
                s_texMetallic["type"] = "texture";
                s_texMetallic["value"] = texMetallic.name;
                root["params"].Add(s_texMetallic);

                metallic = mat.GetFloat("_GlossMapScale");
            }
        }
        catch (Exception)
        {

        }

        //s_texSmoothness
        try
        {
            var texSmoothness = mat.GetTexture("_SpecGlossMap");
            if (texSmoothness)
            {
                JsonData s_texSmoothness = new JsonData();
                s_texSmoothness["name"] = "s_texSmoothness";
                s_texSmoothness["type"] = "texture";
                s_texSmoothness["value"] = texSmoothness.name;
                root["params"].Add(s_texSmoothness);

                smoothness = mat.GetFloat("_GlossMapScale");
            }
        }
        catch (Exception)
        {

        }

        //s_texAO
        try
        {
            var texAO = mat.GetTexture("_OcclusionMap");
            if (texAO)
            {
                JsonData s_texAO = new JsonData();
                s_texAO["name"] = "s_texAO";
                s_texAO["type"] = "texture";
                s_texAO["value"] = texAO.name;
                root["params"].Add(s_texAO);

                aoStrength = mat.GetFloat("_OcclusionStrength");
            }
        }
        catch(Exception)
        {

        }

        JsonData u_pbrParam = new JsonData();
        u_pbrParam["name"] = "u_pbrParam";
        u_pbrParam["type"] = "vector";
        JsonData u_pbrParamData = new JsonData();
        float ambientComponent = 0.05f;
        u_pbrParamData.Add(0);
        u_pbrParamData[0] = metallic;
        u_pbrParamData.Add(0);
        u_pbrParamData[1] = smoothness;
        u_pbrParamData.Add(0);
        u_pbrParamData[2] = ambientComponent;
        u_pbrParamData.Add(0);
        u_pbrParamData[3] = aoStrength;
        u_pbrParam["value"] = u_pbrParamData;
        root["params"].Add(u_pbrParam);

        return true;
    }
}
