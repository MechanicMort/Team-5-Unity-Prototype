using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
using System;
using UnityEditor;
using System.IO;
using UnityEngine.Rendering;

public class MaterialExporter
{
    public static bool StandardMaterialToJson(Material mat, out JsonData root, bool isURP,
                                                bool toCopyTextures = false, string dirDst = "./")
    {
        string texNameBump, texNameMetallic, texNameSmoothness, texNameAO;
        string floatNameMetallic0, floatNameMetallic1, floatNameSmoothness0, floatNameSmoothness1, floatNameOcclusion;
        
        texNameBump = "_BumpMap";
        texNameMetallic = "_MetallicGlossMap";
        texNameSmoothness = "_SpecGlossMap";
        texNameAO = "_OcclusionMap";
        if (!isURP)
        {
            floatNameMetallic0 = "_GlossMapScale";
            floatNameMetallic1 = "_Metallic";
            floatNameSmoothness0 = "_GlossMapScale";
            floatNameSmoothness1 = "_Glossiness";
            floatNameOcclusion = "_OcclusionStrength";
        }
        else
        {
            floatNameMetallic0 = "_Smoothness";
            floatNameMetallic1 = "_Metallic";
            floatNameSmoothness0 = "_Smoothness";
            floatNameSmoothness1 = "_Smoothness";
            floatNameOcclusion = "_OcclusionStrength";
        }

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

            if (toCopyTextures)
            {
                CopyDependencyTexture(mat.mainTexture, dirDst);
            }
        }

        //s_texNormal
        try
        {
            var texNormalMap = mat.GetTexture(texNameBump);
            if (texNormalMap)
            {
                JsonData s_texNormal = new JsonData();
                s_texNormal["name"] = "s_texNormal";
                s_texNormal["type"] = "texture";
                s_texNormal["value"] = texNormalMap.name;
                root["params"].Add(s_texNormal);

                if (toCopyTextures)
                {
                    CopyDependencyTexture(texNormalMap, dirDst);
                }
            }
        }
        catch(Exception)
        {

        }


        //s_texMetallic
        try
        {
            var texMetallic = mat.GetTexture(texNameMetallic);
            if (texMetallic)
            {
                JsonData s_texMetallic = new JsonData();
                s_texMetallic["name"] = "s_texMetallic";
                s_texMetallic["type"] = "texture";
                s_texMetallic["value"] = texMetallic.name;
                root["params"].Add(s_texMetallic);

                metallic = mat.GetFloat(floatNameMetallic0);

                if (toCopyTextures)
                {
                    CopyDependencyTexture(texMetallic, dirDst);
                }
            }
            else
            {
                metallic = mat.GetFloat(floatNameMetallic1);
            }
        }
        catch (Exception)
        {

        }

        //s_texSmoothness
        try
        {
            var texSmoothness = mat.GetTexture(texNameSmoothness);
            if (texSmoothness)
            {
                JsonData s_texSmoothness = new JsonData();
                s_texSmoothness["name"] = "s_texSmoothness";
                s_texSmoothness["type"] = "texture";
                s_texSmoothness["value"] = texSmoothness.name;
                root["params"].Add(s_texSmoothness);

                smoothness = mat.GetFloat(floatNameSmoothness0);

                if (toCopyTextures)
                {
                    CopyDependencyTexture(texSmoothness, dirDst);
                }
            }
            else
            {
                metallic = mat.GetFloat(floatNameSmoothness1);
            }
        }
        catch (Exception)
        {

        }

        //s_texAO
        try
        {
            var texAO = mat.GetTexture(texNameAO);
            if (texAO)
            {
                JsonData s_texAO = new JsonData();
                s_texAO["name"] = "s_texAO";
                s_texAO["type"] = "texture";
                s_texAO["value"] = texAO.name;
                root["params"].Add(s_texAO);

                aoStrength = mat.GetFloat(floatNameOcclusion);

                if (toCopyTextures)
                {
                    CopyDependencyTexture(texAO, dirDst);
                }
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

    protected static void CopyDependencyTexture(Texture tex, string dirDst)
    {
        string pathOrg = AssetDatabase.GetAssetPath(tex);
        string dirOrg = Path.GetDirectoryName(pathOrg);
        string filename = Path.GetFileName(pathOrg);
        if (!FileHelper.CopyFileFromTo(dirOrg, dirDst, filename))
        {
            throw new FileNotFoundException();
        }
    }
}
