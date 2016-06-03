using UnityEngine;
using System;
using System.IO;
namespace Manager.File
{
    public class TextFileUtil
    {
        public TextFileUtil()
        {
        }

        public void writeFile(string path, string name, string info)
        {
            StreamWriter sw;

            FileInfo t = new FileInfo(path + "//" + name);
            if (t.Exists)
            {
                sw = t.CreateText();
            }
            else
            {
                sw = t.AppendText();
            }

            sw.Write(info);

            sw.Close();
            sw.Dispose();
        }

        public string readFile(string path, string name)
        {
            StreamReader sr = null;
            try
            {
                sr = System.IO.File.OpenText(path + "//" + name);
            }
            catch (Exception e)
            {
                Manager.Debug.DebugManager.getInstance().error(e);
                //路径与名称未找到文件则直接返回空
                return null;
            }
            string line = sr.ReadToEnd();

            sr.Close();
            sr.Dispose();
            return line;
        }

        public void deleteFile(string path, string name)
        {
            System.IO.File.Delete(path + "//" + "name");
        }
    }
}