using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PuzzleManager : MonoBehaviourPunCallbacks
{
    public bool test;
    public RotateObjects[] lamps;
    public RotateObjects[] mirrors;
    public GameObject finalSwitch;
    public GameObject finalTarget;
    public Animator doorAnim;
    public Animator doorTwoAnim;

    [SerializeField] private bool isFinalSwitchActive = false;
    [SerializeField] private bool firstPuzzleCompleted = false;
    [SerializeField] private bool secondPuzzleCompleted = false;
    [SerializeField] private bool firstLightsCompleted = false, secondLightsCompleted = false;
    [SerializeField] private bool firstMirrorsCompleted = false, secondMirrorsCompleted = false;

    void Start()
    {
        finalSwitch.SetActive(false);
        finalTarget.SetActive(false);
    }

    void Update()
    {
        CheckPuzzleCompletion();
        firstPuzzleCompleted = firstLightsCompleted && firstMirrorsCompleted;
        secondPuzzleCompleted = secondLightsCompleted && secondMirrorsCompleted;

        if (firstPuzzleCompleted && !isFinalSwitchActive)
        {
            photonView.RPC("FinalObjectActivate", RpcTarget.AllBuffered, true);
        }

        if (secondPuzzleCompleted && isFinalSwitchActive)
        {
            photonView.RPC("PuzzleCompletedAnimation", RpcTarget.AllBuffered, true);
        }
        else
        {
            photonView.RPC("PuzzleCompletedAnimation", RpcTarget.AllBuffered, false);
        }
    }

    [PunRPC]
    private void FinalObjectActivate(bool active)
    {
        finalSwitch.SetActive(true);
        finalTarget.SetActive(true);
        isFinalSwitchActive = true;
    }
    
    void CheckPuzzleCompletion()
    {
        firstLightsCompleted = CheckFirstLights();
        secondLightsCompleted = CheckSecondLights();
        firstMirrorsCompleted = CheckFirstMirrors();
        secondMirrorsCompleted = CheckSecondMirrors();

        Debug.Log($"firstLightsCompleted: {firstLightsCompleted}, secondLightsCompleted: {secondLightsCompleted}, firstMirrorsCompleted: {firstMirrorsCompleted}, secondMirrorsCompleted: {secondMirrorsCompleted}");
    }

    bool CheckFirstLights()
    {
        foreach (var lamp in lamps)
        {
            if (!lamp.IsCorrectRotationOne())
            {
                return false;
            }
        }
        return true;
    }

    bool CheckSecondLights()
    {
        foreach (var lamp in lamps)
        {
            if (!lamp.IsCorrectRotationTwo())
            {
                return false;
            }
        }
        return true;
    }

    bool CheckFirstMirrors()
    {
        foreach (var mirror in mirrors)
        {
            if (!mirror.IsCorrectRotationOne())
            {
                return false;
            }
        }
        return true;
    }

    bool CheckSecondMirrors()
    {
        foreach (var mirror in mirrors)
        {
            if (!mirror.IsCorrectRotationTwo())
            {
                return false;
            }
        }
        return true;
    }

    void UpdateActiveObjects()
    {
        foreach (var lamp in lamps)
        {
            lamp.UpdateActiveObjects();
        }

        foreach (var mirror in mirrors)
        {
            mirror.UpdateMirrorActiveObjects();
        }
    }

    [PunRPC]
    private void PuzzleCompletedAnimation(bool active)
    {
        doorAnim.SetBool("PuzzleCompleted", active);
        doorTwoAnim.SetBool("PuzzleCompleted", active);
    }
}