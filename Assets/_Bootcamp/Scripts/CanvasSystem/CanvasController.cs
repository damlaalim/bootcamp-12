using UnityEngine;
using Zenject;

namespace _Bootcamp.Scripts.CanvasSystem
{
    public class CanvasController : MonoBehaviour
    {
        public CanvasType canvasType;
        [Inject] protected CanvasManager CanvasManager;
        protected Canvas Canvas; 

        public virtual void Initialize()
        {
            Canvas = GetComponent<Canvas>();
            Close();
        }

        public virtual void Close() => Canvas.enabled = false;

        public virtual void Open() => Canvas.enabled = true;

        public virtual void Back() => CanvasManager.Back();
    }
}