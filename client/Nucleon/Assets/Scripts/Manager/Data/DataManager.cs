using UnityEngine;
using System;
using System.Collections.Generic;

namespace Manager.Debug
{
    public class DataManager
    {
        private static DataManager _instance;
        public static DataManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }

        public void writeData(string key, object data, Type type)
        {
            if (type == typeof(int))
                PlayerPrefs.SetInt(key, (int)data);
            else if (type == typeof(string))
                PlayerPrefs.SetString(key, (string)data);
            else if (type == typeof(float))
                PlayerPrefs.SetFloat(key, (float)data);
        }
        public object readData(string key, Type type)
        {
            if (PlayerPrefs.HasKey(key))
            {

                if (type == typeof(int))
                    return PlayerPrefs.GetInt(key);
                else if (type == typeof(string))
                    return PlayerPrefs.GetString(key);
                else if (type == typeof(float))
                    return PlayerPrefs.GetFloat(key);
            }

            return null;

        }

        public void removeData(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {

                PlayerPrefs.DeleteKey(key);
            }
        }
    }
}