using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Transition : NetworkBehaviour
    {
        void OnGUI()
        {
            //This displays a Button on the screen at position (20,30), width 150 and height 50. The button’s text reads the last parameter. Press this for the SceneManager to load the Scene.
            if (GUI.Button(new Rect(20, 30, 150, 30), "Other Scene Single"))
            {
                if(Mouse.current.leftButton.wasReleasedThisFrame){}
                //The SceneManager loads your new Scene as a single Scene (not overlapping). This is Single mode.
                SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
            }

            //Whereas pressing this Button loads the Additive Scene.
            if (GUI.Button(new Rect(20, 60, 150, 30), "Other Scene Additive"))
            {
                //SceneManager loads your new Scene as an extra Scene (overlapping the other). This is Additive mode.
                SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
            }
        }
    }
}

