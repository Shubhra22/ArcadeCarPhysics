/*
Author: Shubhra Sarker
Company: JoystickLab
Email: ssuvro22@outlook.com
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SocialNinja
{
    public class Manager<T> : MonoBehaviour where T:MonoBehaviour
    {
        #region Variables
        private static T _instance;

        public static T Instance
        {
            get => _instance;
            set
            {
                if (_instance == null)
                {
                    _instance = value;
                    DontDestroyOnLoad(_instance.gameObject);
                }
                else if (_instance != value)
                {
                    Destroy(value.gameObject);
                }
            
            }
        }
        #endregion

        #region MainMethods
        // Start is called before the first frame update
        void Awake()
        {
            Instance = this as T;
        }
        #endregion
        
    }
}