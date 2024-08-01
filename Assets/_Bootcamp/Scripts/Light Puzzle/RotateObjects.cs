using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RotateObjects : MonoBehaviourPunCallbacks
{
    public static RotateObjects Instance;

    public Vector3[] rotations;
    public int currentRotationIndex = 0;
    public Vector3 correctRotationOne;
    public Vector3 correctRotationTwo;

    public GameObject[] objectsForRotation; // Bu objeler ýþýklar veya aynalar için rotasyona baðlý olarak aktif edilecek.
    public GameObject[] conditionalObjects; // Aynalar için kontrol edilecek diðer objeler

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Baþlangýçta ilk rotasyona ayarla
        transform.eulerAngles = rotations[currentRotationIndex];
        UpdateActiveObjects();
    }

    public void RotateObject()
    {
        photonView.RPC("RotateObjectRPC", RpcTarget.AllBuffered); 
    }

    [PunRPC]
    private void RotateObjectRPC()
    {
        // Sonraki rotasyona geç
        currentRotationIndex = (currentRotationIndex + 1) % rotations.Length;
        transform.eulerAngles = rotations[currentRotationIndex];
        UpdateActiveObjects();   
    }

    public bool IsCorrectRotationOne()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(correctRotationOne);

        bool result = ApproximatelyEqual(currentRotation, targetRotation);
        Debug.Log($"{gameObject.name} - Current Rotation: {currentRotation.eulerAngles}, Target Rotation: {correctRotationOne}, Is Correct: {result}");
        return result;
    }

    public bool IsCorrectRotationTwo()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(correctRotationTwo);

        bool result = ApproximatelyEqual(currentRotation, targetRotation);
        Debug.Log($"{gameObject.name} - Current Rotation: {currentRotation.eulerAngles}, Target Rotation: {correctRotationTwo}, Is Correct: {result}");
        return result;
    }

    bool ApproximatelyEqual(Quaternion a, Quaternion b, float tolerance = 0.1f)
    {
        return Quaternion.Angle(a, b) < tolerance;
    }

    public void UpdateActiveObjects()
    {
        // Tüm objeleri önce deaktif et
        foreach (GameObject obj in objectsForRotation)
        {
            obj.SetActive(false);
        }

        // Geçerli rotasyona baðlý olarak doðru objeyi aktif et
        if (currentRotationIndex < objectsForRotation.Length)
        {
            objectsForRotation[currentRotationIndex].SetActive(true);
        }
    }

    public void UpdateMirrorActiveObjects()
    {
        // Aynalarýn koþullu objelerini kontrol ederek aktif hale getirme
        bool allConditionsMet = true;
        foreach (GameObject obj in conditionalObjects)
        {
            if (!obj.activeSelf)
            {
                allConditionsMet = false;
                break;
            }
        }

        // Koþullar saðlanýyorsa rotasyonlara göre objeleri aktif et
        if (allConditionsMet)
        {
            UpdateActiveObjects();
        }
        else
        {
            // Koþullar saðlanmýyorsa tüm objeleri deaktif et
            foreach (GameObject obj in objectsForRotation)
            {
                obj.SetActive(false);
            }
        }
    }
}