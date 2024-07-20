using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Bootcamp.Scripts.Platform
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private List<PlatformController> _platformList;

        public void PlatformTriggered(PlatformController platform)
        {
            var index = _platformList.FindIndex(controller => controller == platform);
            
            if (++index >= _platformList.Count)
                return;

            var newPlatform = _platformList[index];
            newPlatform.ShowPlatform();
            
            if (newPlatform.isTrap)
                PlatformTriggered(newPlatform);
        }
    }
}