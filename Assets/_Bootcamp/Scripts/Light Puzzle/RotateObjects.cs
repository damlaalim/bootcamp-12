using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    public static RotateObjects Instance;

    public Vector3[] rotations;
    public int currentRotationIndex = 0;
    public Vector3 correctRotationOne;
    public Vector3 correctRotationTwo;

    public GameObject[] objectsForRotation; // Bu objeler ���klar veya aynalar i�in rotasyona ba�l� olarak aktif edilecek.
    public GameObject[] conditionalObjects; // Aynalar i�in kontrol edilecek di�er objeler

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Ba�lang��ta ilk rotasyona ayarla
        transform.eulerAngles = rotations[currentRotationIndex];
        UpdateActiveObjects();
    }

    public void RotateObject()
    {
        // Sonraki rotasyona ge�
        currentRotationIndex = (currentRotationIndex + 1) % rotations.Length;
        transform.eulerAngles = rotations[currentRotationIndex];
        UpdateActiveObjects();
    }

    public bool IsCorrectRotationOne()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(correctRotationOne);

        bool result = ApproximatelyEqual(currentRotation, targetRotation);
        Debug.Log($"{gameObject.name} - Current Rotation (Quaternion): {currentRotation}, Target Rotation (Quaternion): {targetRotation}, Is Correct: {result}");
        return result;
    }

    public bool IsCorrectRotationTwo()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(correctRotationTwo);

        bool result = ApproximatelyEqual(currentRotation, targetRotation);
        Debug.Log($"{gameObject.name} - Current Rotation (Quaternion): {currentRotation}, Target Rotation (Quaternion): {targetRotation}, Is Correct: {result}");
        return result;
    }

    bool ApproximatelyEqual(Quaternion a, Quaternion b, float tolerance = 0.1f)
    {
        return Quaternion.Angle(a, b) < tolerance;
    }

    public void UpdateActiveObjects()
    {
        // T�m objeleri �nce deaktif et
        foreach (GameObject obj in objectsForRotation)
        {
            obj.SetActive(false);
        }

        // Ge�erli rotasyona ba�l� olarak do�ru objeyi aktif et
        if (currentRotationIndex < objectsForRotation.Length)
        {
            objectsForRotation[currentRotationIndex].SetActive(true);
        }
    }

    public void UpdateMirrorActiveObjects()
    {
        // Aynalar�n ko�ullu objelerini kontrol ederek aktif hale getirme
        bool allConditionsMet = true;
        foreach (GameObject obj in conditionalObjects)
        {
            if (!obj.activeSelf)
            {
                allConditionsMet = false;
                break;
            }
        }

        // Ko�ullar sa�lan�yorsa rotasyonlara g�re objeleri aktif et
        if (allConditionsMet)
        {
            UpdateActiveObjects();
        }
        else
        {
            // Ko�ullar sa�lanm�yorsa t�m objeleri deaktif et
            foreach (GameObject obj in objectsForRotation)
            {
                obj.SetActive(false);
            }
        }
    }
}