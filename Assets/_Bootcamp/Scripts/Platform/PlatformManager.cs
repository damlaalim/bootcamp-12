using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Bootcamp.Scripts.Platform
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private List<PlatformController> _platformList;

        private void Start()
        {
            if (_platformList.Count > 0)
                _platformList[0].ShowPlatform();
        }

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