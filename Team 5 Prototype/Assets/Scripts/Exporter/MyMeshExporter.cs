using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Note: You need to turn on read/write of meshes
 */
public class MyMeshExporter
{
    enum GeometryChunkTypes
    {
        VPositions = 1 << 0,
        VNormals = 1 << 1,
        VTangents = 1 << 2,
        VColors = 1 << 3,
        VTex0 = 1 << 4,
        VTex1 = 1 << 5,
        VWeightValues = 1 << 6,
        VWeightIndices = 1 << 7,
        Indices = 1 << 8,
        JointNames = 1 << 9,
        JointParents = 1 << 10,
        BindPose = 1 << 11,
        BindPoseInv = 1 << 12,
        Material = 1 << 13,
        SubMeshes = 1 << 14,
        SubMeshNames = 1 << 15,
    };

    public static string ProcMeshName(string name)
    {
        return name.Replace(':', '_');
    }

    //Note! This function will not export animation(including skeleton) or material unlike Rich's. 
    public static void ExportMeshToFile(Mesh m, string pathOut, bool invertZ)
    {

        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(pathOut, false))
        {

            List<Vector3> allVertPos = new List<Vector3>();
            List<Vector4> allTangents = new List<Vector4>();
            List<Vector3> allNormals = new List<Vector3>();
            List<Vector2> allUVs = new List<Vector2>();
            List<int> allIndices = new List<int>();
            List<int> allWeightIndices = new List<int>();
            List<float> allWeightValues = new List<float>();
            List<int> indexStarts = new List<int>();
            List<int> indexEnds = new List<int>();
            List<string> meshNames = new List<string>();
            int meshCount = 0;

            Vector3[] vertPos = m.vertices;
            Vector3[] vertNormal = m.normals;
            Vector4[] vertTangent = m.tangents;
            Vector2[] vertUV = m.uv;
            Color[] vertColors = m.colors;
            BoneWeight[] weights = m.boneWeights;

            Debug.Log("Mesh info: " + m.name);
            Debug.Log("Vert count: " + vertPos.Length);

            int startIndex = allVertPos.Count;

            Matrix4x4 mat = Matrix4x4.identity;
            if (invertZ)
            {
                mat = Matrix4x4.Scale(new Vector3(1, 1, -1));
            }

            for (int i = 0; i < vertPos.Length; ++i)
            {
                //Vector4 localVert = new Vector4(vertPos[i].x, vertPos[i].y, vertPos[i].z, 1.0f);
                Vector4 localVert = mat.MultiplyPoint(new Vector3(vertPos[i].x, vertPos[i].y, vertPos[i].z));
                localVert.w = 1.0f;
                allVertPos.Add(localVert);
            }

            for (int i = 0; i < vertPos.Length; ++i)
            {
                //int p = -1;

                //bones.TryGetValue(r.transform.name, out p);

                allWeightValues.Add(1.0f);
                allWeightValues.Add(0.0f);
                allWeightValues.Add(0.0f);
                allWeightValues.Add(0.0f);

                allWeightIndices.Add(-1);
                allWeightIndices.Add(0);
                allWeightIndices.Add(0);
                allWeightIndices.Add(0);
            }

            if (vertNormal.Length > 0)
            {
                allNormals.AddRange(vertNormal);
            }
            if (vertTangent.Length > 0)
            {
                allTangents.AddRange(vertTangent);
            }
            if (vertUV.Length > 0)
            {
                allUVs.AddRange(vertUV);
            }

            for (int i = 0; i < m.subMeshCount; ++i)
            {
                meshNames.Add(m.name + "_" + i);

                int[] indices = m.GetIndices(i);
                Debug.Log("Index count: " + indices.Length);
                int start = allIndices.Count;
                indexStarts.Add(start);
                foreach (int index in indices)
                {
                    allIndices.Add(startIndex + index);
                }
                indexEnds.Add(allIndices.Count - start);

                meshCount++;
            }



            int expectedAttribCount = 0;
            int exportedAttribCount = 0;

            expectedAttribCount += allVertPos.Count > 0 ? 1 : 0;
            expectedAttribCount += allNormals.Count > 0 ? 1 : 0;
            expectedAttribCount += allTangents.Count > 0 ? 1 : 0;
            expectedAttribCount += allUVs.Count > 0 ? 1 : 0;
            expectedAttribCount += allIndices.Count > 0 ? 1 : 0;
            expectedAttribCount += indexStarts.Count > 0 ? 1 : 0;
            expectedAttribCount += meshNames.Count > 0 ? 1 : 0;

            file.WriteLine("MeshGeometry");
            file.WriteLine(1);
            file.WriteLine(meshCount);
            file.WriteLine(allVertPos.Count);
            file.WriteLine(allIndices.Count);
            file.WriteLine(expectedAttribCount);

            if (allVertPos.Count > 0)
            {
                file.WriteLine((int)GeometryChunkTypes.VPositions); exportedAttribCount++;
                foreach (Vector3 v in allVertPos)
                {
                    file.WriteLine(v.x + " " + v.y + " " + v.z);
                }
            }
            if (allNormals.Count > 0)
            {
                file.WriteLine((int)GeometryChunkTypes.VNormals); exportedAttribCount++;
                foreach (Vector3 v in allNormals)
                {
                    file.WriteLine(v.x + " " + v.y + " " + v.z);
                }
            }

            if (allTangents.Count > 0)
            {
                file.WriteLine((int)GeometryChunkTypes.VTangents); exportedAttribCount++;
                foreach (Vector4 v in allTangents)
                {
                    file.WriteLine(v.x + " " + v.y + " " + v.z + " " + v.w);
                }
            }

            if (allUVs.Count > 0)
            {
                file.WriteLine((int)GeometryChunkTypes.VTex0); exportedAttribCount++;
                foreach (Vector2 v in allUVs)
                {
                    file.WriteLine(v.x + " " + v.y);
                }
            }
            if (allIndices.Count > 0)
            {
                file.WriteLine((int)GeometryChunkTypes.Indices); exportedAttribCount++;
                for (int i = 0; i < allIndices.Count; i += 3)
                {
                    if (invertZ)
                    {
                        file.WriteLine(allIndices[i] + " " + allIndices[i + 2] + " " + allIndices[i + 1]);
                    }
                    else
                    {
                        file.WriteLine(allIndices[i] + " " + allIndices[i + 1] + " " + allIndices[i + 2]);
                    }
                }
            }

            if (indexStarts.Count > 0)
            {
                file.WriteLine((int)GeometryChunkTypes.SubMeshes); exportedAttribCount++;
                for (int i = 0; i < indexStarts.Count; ++i)
                {
                    file.WriteLine(indexStarts[i] + " " + indexEnds[i]);
                }
            }
            if (meshNames.Count > 0)
            {
                file.WriteLine((int)GeometryChunkTypes.SubMeshNames); exportedAttribCount++;
                foreach (string s in meshNames)
                {
                    file.WriteLine(s);
                }
            }

            if (exportedAttribCount != expectedAttribCount)
            {
                Debug.LogWarning("Mesh exported incorrect attribute count?");
            }

        }
    }
}
