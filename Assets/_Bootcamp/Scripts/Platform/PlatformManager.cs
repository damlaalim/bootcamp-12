using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Bootcamp.Scripts.Platform
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private List<PlatformList> _platformsList;

        private void Start()
        {
            // if (_platformsList[0].platforms.Count > 0)
            //     _platformsList[0].platforms[0].ShowPlatform();
        }

        public void PlatformTriggered(PlatformController platform)
        {
            // var index = -1;
            // PlatformList platformList = null;
            //
            // foreach (var a in _platformsList)
            // {
            //     index = a.platforms.FindIndex(current => current == platform);
            //     
            //     if (index == -1)
            //         continue;
            //
            //     platformList = a;
            //     break;
            // }
            //
            // if (platformList is null || ++index >= platformList.platforms.Count)
            //     return;
            //
            // var newPlatform = platformList.platforms[index];
            // newPlatform.ShowPlatform();
            //
            // if (newPlatform.isTrap)
            //     PlatformTriggered(newPlatform);
        }
    }

    [Serializable]
    public class PlatformList
    {
        public List<PlatformController> platforms;
    }
}