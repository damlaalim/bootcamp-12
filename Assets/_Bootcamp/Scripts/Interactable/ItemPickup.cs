using UnityEngine;

namespace _Bootcamp.Scripts.Interactable
{
    public class ItemPickup : MonoBehaviour
    {
        public Item Item;

        void Pickup()
        {
            if (Item != null)
            {
                // InventoryManager.Instance.Add(Item);
                Destroy(gameObject);
            }
        }

        private void OnMouseDown()
        {
            Pickup();
        }
    }
}
