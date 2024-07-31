using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingMenu;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
        
    }
    public void MainMenu()
    {
        //Main menuyu yükle
    }
    public void CloseMenu()
    {
        pauseMenu.SetActive(false);
    }
    public void SettingsMenuOn()
    {
        settingMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void SettingsMenuOff()
    {
        settingMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void FullScreen()
    {
        Screen.fullScreen = true;
    }
    public void WindowedScreen()
    {
        Screen.fullScreen = false;
    }
}
