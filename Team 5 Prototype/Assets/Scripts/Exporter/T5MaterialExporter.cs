using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

using LitJson;

public class T5MaterialExporter : MonoBehaviour
{
    public string pathOut = "./";

    public List<Material> materials;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i< materials.Count; ++i)
        {
            string filename = materials[i].name+".mat";
            string outputPath = Path.Combine(pathOut, filename);
            JsonData json = "";
            if(MaterialExporter.StandardMaterialToJson(materials[i], out json))
            {
                string str;
                ExporterHelper.JsonToString(json, out str);
                FileIO.WriteTextToFile(outputPath, str);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}