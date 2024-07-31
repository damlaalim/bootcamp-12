using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject infoCanvas, settingsCanvas, startPanel;
    // Start is called before the first frame update
    private void Awake()
    {
        infoCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LeaveGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {

    }
    public void SettingsMenu()
    {
        settingsCanvas.SetActive(true);
        startPanel.SetActive(false);
    }
    public void SettingsMenuOff()
    {
        startPanel.SetActive(true);
        settingsCanvas.SetActive(false);
    }
    public void InfoMenu()
    {
        infoCanvas.SetActive(true);
        startPanel.SetActive(false);
    }
    public void InfoMenuOff()
    {
        startPanel.SetActive(true);
        infoCanvas.SetActive(false);
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
