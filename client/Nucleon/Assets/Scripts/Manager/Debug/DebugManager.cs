using UnityEngine;
using System.Collections;

namespace Manager.Debug
{
    public class DebugManager
    {
        private static DebugManager _instance;
        public static DebugManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new DebugManager();
            }
            return _instance;
        }

        public void trace(object value)
        {
            UnityEngine.Debug.Log("[trace] " + value);
        }

        public void warn(object value)
        {
            UnityEngine.Debug.Log("[warn] " + value);
        }

        public void error(object value)
        {
            UnityEngine.Debug.Log("[error] " + value);
        }

    }

}