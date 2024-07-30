using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class LightSequenceManager : MonoBehaviour
{
    public List<LightControl> lights;
    public List<int> correctSequence;

    private int currentOrder = 0;
  
    public void SwitchCheck(int order)
    {
        if (correctSequence[currentOrder] == order)
        {
            currentOrder++;
            Debug.Log("Doğru tetiklendi current order" + currentOrder);
            if (currentOrder >= correctSequence.Count)
            {
                Debug.Log("WIN");
            }
        }
        else
        {
            currentOrder = 0;
            ResetAllSwitches();
            Debug.Log("current index resetlendi" + currentOrder);
        }
    }

    void ResetAllSwitches()
    {
        foreach (var light in lights)
        {
            light.TurnOff();
        }
    }

    public IEnumerator LightRoutine()
    {
         OpenAllSwitch();
         yield return new WaitForSeconds(2f);
         ResetAllSwitches();
         yield return new WaitForSeconds(0.3f);
         OpenAllSwitch();
         yield return new WaitForSeconds(0.6f);
         ResetAllSwitches();
         yield return new WaitForSeconds(0.3f);
         OpenAllSwitch();
         yield return new WaitForSeconds(0.1f);
         ResetAllSwitches();

    }
    
    void OpenAllSwitch()
    {
        foreach (var light in lights)
        {
            light.TurnOn();
        }
    }
    
    
}