using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class T5MeshExporter : MonoBehaviour
{
    public string path = "./";
    public List<MeshExportEntry> meshes = new List<MeshExportEntry>();

    private void Start()
    {
        foreach(var entry in meshes)
        {
            string pathOut = Path.Combine(path, MyMeshExporter.ProcMeshName(entry.mesh.name) + ".msh");
            MyMeshExporter.ExportMeshToFile(entry.mesh, pathOut, entry.invertZ);
        }
    }
}

[Serializable]
public struct MeshExportEntry
{
    public Mesh mesh;
    //public string name;
    public bool invertZ;
}