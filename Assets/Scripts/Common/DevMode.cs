using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class DevMode : Singleton<DevMode>
    {
        public bool isDevMode = false;
        public void Log(object content)
        {
            if (isDevMode)
            {
                Debug.Log("DEBUG-LOG: " + content.ToString());

            }
        }
        public void Log(string content)
        {
            if (isDevMode)
            {
                Debug.Log("DEBUG-LOG: " + content.ToString());

            }
        }
    }
}