using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class FileIO
{
    public static bool WriteTextToFile(string path, string content)
    {
        StreamWriter sw;
        FileInfo fi = new FileInfo(path);
        sw = fi.CreateText();
        
        sw.Write(content);

        sw.Close();
        sw.Dispose();

        return true;
    }
}
