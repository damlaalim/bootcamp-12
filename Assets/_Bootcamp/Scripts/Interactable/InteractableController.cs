using UnityEngine;

namespace _Bootcamp.Scripts.Interactable
{
    public class InteractableController : MonoBehaviour
    {
        public void Do()
        {
            Debug.Log($"Interaction, obj: {gameObject.name}", gameObject);
        }
    }
}