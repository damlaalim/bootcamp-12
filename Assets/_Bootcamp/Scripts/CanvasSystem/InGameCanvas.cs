using TMPro;
using UnityEngine;

namespace _Bootcamp.Scripts.CanvasSystem
{
    public class InGameCanvas : CanvasController
    {
        [SerializeField] private TextMeshProUGUI _interactionText;

        public void ShowInteractionText(bool show) => _interactionText.enabled = show;
    }
}