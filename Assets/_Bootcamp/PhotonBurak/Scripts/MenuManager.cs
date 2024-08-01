using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
   public static bool gameIsPaused;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.C))
      {
          Debug.Log("chat open");
          gameIsPaused = !gameIsPaused;
          PauseGame();
      }
   }

   public  void PauseGame()
   {
       if (gameIsPaused)
       {
           Time.timeScale = 0;
           Cursor.visible = true;
           Cursor.lockState = CursorLockMode.None;
       }
       else
       {
           Cursor.visible = false;
           Time.timeScale = 1;
       }
   }
   
   
}
