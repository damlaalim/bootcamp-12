using UnityEngine;
using System.Collections.Generic;

public class LightSequenceManager : MonoBehaviour
{
    public List<LightControl> lights; // Işıkların listesi
    public List<int> correctSequence; // Doğru sıralama
    private List<int> currentSequence = new List<int>(); // Oyuncunun seçtiği sıralama

    public void AddToSequence(int lightOrder)
    {
        currentSequence.Add(lightOrder);
        
        // Sıralamayı kontrol et
        if (currentSequence.Count == correctSequence.Count)
        {
            CheckSequence();
        }
    }

    void CheckSequence()
    {
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (currentSequence[i] != correctSequence[i])
            {
                // Yanlış sıralama
                currentSequence.Clear();
                return;
            }
        }

        // Doğru sıralama
        LoadNextScene();
    }

    void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OfficePhoton");
    }
}