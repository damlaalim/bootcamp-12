using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Bootcamp.Scripts.Interactable
{
    public class CollectableController : MonoBehaviour, IInteractable
    {
        public Item data;
        [SerializeField] private Canvas _canvas;

        [Inject] private InventoryManager _inventoryManager;
        public Canvas ICanvas
        {
            get => _canvas;
            set { }
        }

        public void Do()
        {
            _inventoryManager.Add(this);
            transform.DOScale(Vector3.zero, .2f);
        }

        public void ShowCanvas(bool show)
        {
            ICanvas.enabled = show;
        }
        public void Use()
        {
            Debug.Log($"{data.itemName}");
        }
    }
}