using UnityEngine;
using System;
using System.Collections.Generic;

namespace Manager.File
{
    public class FileManager
    {
        private static FileManager _instance;
        public static FileManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new FileManager();
            }
            return _instance;
        }

        //Application.persistentDataPath
        public TextFileUtil textFileUtil = new TextFileUtil();
    }
}