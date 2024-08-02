using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    private bool isOnTrigger = true;
    public GameObject startDialogueBox;
    [SerializeField]Dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isOnTrigger)
        {
            isOnTrigger = false;
            
            if (dialogue != null)
            {
                startDialogueBox.SetActive(true);
                dialogue.StartDialogue();
            }
            else
            {
                Debug.LogError("Dialogue bileşeni bulunamadı!");
            }
        }
    }
}
