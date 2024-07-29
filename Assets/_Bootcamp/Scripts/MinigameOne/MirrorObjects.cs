using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorObjects : MonoBehaviour
{
    public GameObject lightOne, lightTwo, lightThree;

    [Header("Outcomes")]
    public GameObject outcomeOneX, outcomeOneY, outcomeOneZ;
    public GameObject outcomeTwoX, outcomeTwoY, outcomeTwoZ;
    public GameObject outcomeThreeX, outcomeThreeY, outcomeThreeZ;

    private RotateObjects rotateObjectComponent;

    void Start()
    {
        rotateObjectComponent = GetComponent<RotateObjects>();
        UpdateOutcomes();
    }

    void Update()
    {
        UpdateOutcomes();
    }

    void UpdateOutcomes()
    {
        SetOutcome(lightOne, 0, outcomeOneX, rotateObjectComponent);
        SetOutcome(lightOne, 1, outcomeOneY, rotateObjectComponent);
        SetOutcome(lightOne, 2, outcomeOneZ, rotateObjectComponent);

        SetOutcome(lightTwo, 0, outcomeTwoX, rotateObjectComponent);
        SetOutcome(lightTwo, 1, outcomeTwoY, rotateObjectComponent);
        SetOutcome(lightTwo, 2, outcomeTwoZ, rotateObjectComponent);

        SetOutcome(lightThree, 0, outcomeThreeX, rotateObjectComponent);
        SetOutcome(lightThree, 1, outcomeThreeY, rotateObjectComponent);
        SetOutcome(lightThree, 2, outcomeThreeZ, rotateObjectComponent);
    }

    void SetOutcome(GameObject light, int rotationIndex, GameObject outcome, RotateObjects rotateObjectComponent)
    {
        if (light.activeInHierarchy && rotateObjectComponent.currentRotationIndex == rotationIndex)
        {
            outcome.SetActive(true);
        }
        else
        {
            outcome.SetActive(false);
        }
    }
}
