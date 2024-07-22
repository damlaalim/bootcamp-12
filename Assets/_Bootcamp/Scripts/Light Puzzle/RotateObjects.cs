using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    public static RotateObjects Instance;

    public Vector3[] rotations;
    private int currentRotationIndex = 0;
    public Vector3 correctRotationOne;
    public Vector3 correctRotationTwo;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Baþlangýçta ilk rotasyona ayarla
        transform.eulerAngles = rotations[currentRotationIndex];
    }

    public void RotateObject()
    {
        // Sonraki rotasyona geç
        currentRotationIndex = (currentRotationIndex + 1) % rotations.Length;
        transform.eulerAngles = rotations[currentRotationIndex];
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
}