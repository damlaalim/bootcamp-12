using _Bootcamp.PhotonBurak.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    public void ExitGame()
    {
        SceneManager.LoadScene("LoadingPhoton");
    }
}
