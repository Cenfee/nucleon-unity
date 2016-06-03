using UnityEngine;
using System.Collections;

namespace Model.User
{
    public class UserModel : MonoBehaviour
    {

        private static UserModel _instance;
        public static UserModel getInstance()
        {
            if (_instance == null)
            {
                _instance = new UserModel();
            }
            return _instance;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
