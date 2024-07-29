using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace _Bootcamp.Scripts.UI
{
    public class HoverElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TextMeshProUGUI text;
        public string explain;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("enter");
            text.text = explain;
            text.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            text.gameObject.SetActive(false);
        }
    }
}