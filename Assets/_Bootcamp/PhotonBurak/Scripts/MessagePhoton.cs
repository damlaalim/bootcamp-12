using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class MessagePhoton : MonoBehaviour
    {
        public Text MyMessage;

         void Start()
        {
            GetComponent<RectTransform>().SetAsFirstSibling();
        }
    }
}