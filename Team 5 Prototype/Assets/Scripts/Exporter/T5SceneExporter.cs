using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
using System;
using System.Linq;
using System.IO;

public class T5SceneExporter : MonoBehaviour
{
    public string pathOut = "./";
    public bool invertHandedness = true;

    // Start is called before the first frame update
    void Start()
    {
        JsonData jsonScene = new JsonData();
        jsonScene["gameobjects"] = new JsonData();

        var gameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects().ToList();
        for(int i=0;i< gameObjects.Count; ++i)
        {
            GameObject go = gameObjects[i];
            if (go == gameObject)
            {
                continue;
            }
            if (!go.activeSelf)
            {
                continue;
            }
            foreach (Transform child in go.transform)
            {
                gameObjects.Add(child.gameObject);
            }

            JsonData jsonGO;
            print(go.name);
            if(!GoExporter.GameObjectToJson(go, out jsonGO, invertHandedness))
            {
                Debug.LogWarning("Parsing GO Failed: " + go.name);
            }
            else
            {
                jsonScene["gameobjects"].Add(jsonGO);
            }
        }

        string filename = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + ".scn";
        string outputPath = Path.Combine(pathOut, filename);
        string str;
        ExporterHelper.JsonToString(jsonScene, out str);
        FileIO.WriteTextToFile(outputPath, str);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
