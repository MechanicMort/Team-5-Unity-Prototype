using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;
using LitJson;
using System;

public class ExporterHelper
{
    public static bool AddVal(string name, int val, ref JsonData data)
    {
        data[name] = val;
        return true;
    }
    public static bool AddVal(string name, float val, ref JsonData data)
    {
        data[name] = val;
        return true;
    }
    public static bool AddVal(string name, string val, ref JsonData data)
    {
        data[name] = val;
        return true;
    }
    public static bool AddVal(string name, Vector2 val, ref JsonData data)
    {
        data[name] = new JsonData();
        data[name].Add(0);
        data[name][0] = val.x;
        data[name].Add(0);
        data[name][1] = val.y;
        return true;
    }
    public static bool AddVal(string name, Vector3 val, ref JsonData data)
    {
        data[name] = new JsonData();
        data[name].Add(0);
        data[name][0] = val.x;
        data[name].Add(0);
        data[name][1] = val.y;
        data[name].Add(0);
        data[name][2] = val.z;
        return true;
    }
    public static bool AddVal(string name, Vector4 val, ref JsonData data)
    {
        data[name] = new JsonData();
        data[name].Add(0);
        data[name][0] = val.x;
        data[name].Add(0);
        data[name][1] = val.y;
        data[name].Add(0);
        data[name][2] = val.z;
        data[name].Add(0);
        data[name][3] = val.w;
        return true;
    }
    public static bool AddVal(string name, Matrix4x4 val, ref JsonData data)
    {
        data[name] = new JsonData();
        
        var col = val.GetColumn(0);
        data[name].Add(0);
        data[name][0] = col.x;
        data[name].Add(0);
        data[name][1] = col.y;
        data[name].Add(0);
        data[name][2] = col.z;
        data[name].Add(0);
        data[name][3] = col.w;
        
        col = val.GetColumn(1);
        data[name].Add(0);
        data[name][0+4] = col.x;
        data[name].Add(0);
        data[name][1+4] = col.y;
        data[name].Add(0);
        data[name][2+4] = col.z;
        data[name].Add(0);
        data[name][3+4] = col.w;

        col = val.GetColumn(2);
        data[name].Add(0);
        data[name][0+8] = col.x;
        data[name].Add(0);
        data[name][1+8] = col.y;
        data[name].Add(0);
        data[name][2+8] = col.z;
        data[name].Add(0);
        data[name][3+8] = col.w;

        col = val.GetColumn(3);
        data[name].Add(0);
        data[name][0+12] = col.x;
        data[name].Add(0);
        data[name][1+12] = col.y;
        data[name].Add(0);
        data[name][2+12] = col.z;
        data[name].Add(0);
        data[name][3+12] = col.w;
        return true;
    }
    public static bool AddVal(string name, Texture2D val, ref JsonData data)
    {
        if (val)
        {
            data[name] = val.name;
            return true;
        }
        return false;
    }
    public static bool AddVal(string name, Material val, ref JsonData data)
    {
        if (val)
        {
            data[name] = val.name;
            return true;
        }
        return false;
    }
    public static bool AddVal(string name, Mesh val, ref JsonData data)
    {
        if (val)
        {
            data[name] = val.name;
            return true;
        }
        return false;
    }

    public static bool ComponentToJson(MonoBehaviour component, out JsonData root)
    {
        root = new JsonData();

        Type t = component.GetType();
        FieldInfo[] finfos = t.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        if (finfos.Length == 0)
        {
            return false;
        }

        bool hasField = false;

        foreach (FieldInfo finfo in finfos)
        {
            //Debug.Log("字段名称："+ finfo.Name+"  字段类型:"+ finfo.FieldType.ToString() + " rc中的值为:"+ finfo.GetValue(tester0));
            if (finfo.FieldType == typeof(int))
            {
                hasField = AddVal(finfo.Name, (int)finfo.GetValue(component), ref root);
            }
            else if (finfo.FieldType == typeof(float))
            {
                hasField = AddVal(finfo.Name, (float)finfo.GetValue(component), ref root);
            }
            else if (finfo.FieldType == typeof(string))
            {
                hasField = AddVal(finfo.Name, (string)finfo.GetValue(component), ref root);
            }
            else if (finfo.FieldType == typeof(Vector2))
            {
                hasField = AddVal(finfo.Name, (Vector2)finfo.GetValue(component), ref root);
            }
            else if (finfo.FieldType == typeof(Vector3))
            {
                hasField = AddVal(finfo.Name, (Vector3)finfo.GetValue(component), ref root);
            }
            else if (finfo.FieldType == typeof(Vector4))
            {
                hasField = AddVal(finfo.Name, (Vector4)finfo.GetValue(component), ref root);
            }
            else if (finfo.FieldType == typeof(Matrix4x4))
            {
                hasField = AddVal(finfo.Name, (Matrix4x4)finfo.GetValue(component), ref root);
            }
            else if (finfo.FieldType == typeof(Texture2D))
            {
                hasField = AddVal(finfo.Name, (Texture2D)finfo.GetValue(component), ref root);
            }
            else if (finfo.FieldType == typeof(Material))
            {
                hasField = AddVal(finfo.Name, (Material)finfo.GetValue(component), ref root);
            }
            else if (finfo.FieldType == typeof(Mesh))
            {
                hasField = AddVal(finfo.Name, (Mesh)finfo.GetValue(component), ref root);
            }
            else
            {
                
            }
        }

        return hasField;
    }
    public static bool JsonToString(JsonData json, out string str)
    {
        JsonWriter writer = new JsonWriter();
        writer.PrettyPrint = true;
        LitJson.JsonMapper.ToJson(json, writer);
        str = writer.ToString();
        return true;
    }
}
