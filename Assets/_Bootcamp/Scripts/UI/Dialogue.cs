using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI TextComponent;
    public string[] Lines;
    public AudioClip[] AudioClips;
    public float TextSpeed;
    public AudioSource audioSource;
    private int Index;
    public Canvas canvas;
    public Collider NextSceneIU;

    private void Start()
    {
        TextComponent.text = string.Empty;
        Debug.Log("Dialogue script started, TextComponent text cleared, AudioSource component obtained.");
        NextSceneIU.enabled = false;
        canvas.enabled = false;
    }

    public void StartDialogue()
    {
        canvas.enabled = true;
        Index = 0;
        TextComponent.text = string.Empty; // Diyalog başladığında metni temizle
        Debug.Log("StartDialogue called, starting dialogue at index " + Index);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Ses dosyasını çal
        if (AudioClips.Length > 0 && Index < AudioClips.Length)
        {
            audioSource.clip = AudioClips[Index];
            audioSource.Play();
            Debug.Log("Playing audio clip: " + AudioClips[Index].name);
        }
        else
        {
            Debug.LogWarning("No audio clip found for index " + Index);
        }

        foreach (var letter in Lines[Index].ToCharArray())
        {
            TextComponent.text += letter;
            yield return new WaitForSeconds(TextSpeed);
        }

        Debug.Log("Line completed: " + Lines[Index]);

        // Diyalog bittikten sonra 2 saniye bekle
        yield return new WaitForSeconds(2f);

        NextLine();
    }

    void NextLine()
    {
        if (Index < Lines.Length - 1)
        {
            Index++;
            TextComponent.text = string.Empty;
            Debug.Log("Moving to next line, index " + Index);
            StartCoroutine(TypeLine());
        }
        else
        {
            Debug.Log("Dialogue complete, disabling gameObject.");
            gameObject.SetActive(false);
            NextSceneIU.enabled = true;
        }
    }
}
