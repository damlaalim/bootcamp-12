using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PhotonCharacterController _character;
    public GameObject pauseMenu;

    public GameObject settingMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            _character.openCanvas = pauseMenu.activeSelf;
            _character.canMove = !pauseMenu.activeSelf;
            Cursor.lockState = !pauseMenu.activeSelf ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("LoadingPhoton");
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