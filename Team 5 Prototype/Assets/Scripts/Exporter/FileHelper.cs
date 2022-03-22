using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class FileHelper
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

    public static bool CopyFileFromTo(string dirFrom, string dorTo, string fileName)
    {
        string pathFrom = Path.Combine(dirFrom, fileName);
        string pathTo = Path.Combine(dorTo, fileName);
        if (!File.Exists(pathFrom))
        {
            return false;
        }
        if (File.Exists(dorTo))
        {
            return false;
        }
        File.Copy(pathFrom, pathTo, true);
        return true;
    }
}
