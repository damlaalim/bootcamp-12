using _Bootcamp.Scripts.Interactable;
using DialogueEditor;
using UnityEngine;

namespace _Bootcamp.Scripts.NPC
{
    public class NpcController: MonoBehaviour, IInteractable
    {
        [SerializeField] private NPCConversation _conversation;
        
        public void Do()
        {
            Talk();
        }

        private void Talk()
        {
            ConversationManager.Instance.StartConversation(_conversation);
        }
    }
}