using _Bootcamp.Scripts.Interactable;
using DialogueEditor;
using UnityEngine;

namespace _Bootcamp.Scripts.NPC
{
    public class NpcController : MonoBehaviour, IInteractable
    {
        [SerializeField] private Canvas _canvas;

        public Canvas ICanvas
        {
            get => _canvas;
            set { }
        }

        [SerializeField] private NPCConversation _conversation;

        public void Do()
        {
            Talk();
            Debug.Log("Konuştu");
        }

        public void ShowCanvas(bool show)
        {
            ICanvas.enabled = show;
        }

        private void Talk()
        {
            ConversationManager.Instance.StartConversation(_conversation);
        }
    }
}