using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
     public class MainMenuController : MonoBehaviour
     {
     
     
         [SerializeField] private GameObject _mainScreen;
         [SerializeField] private GameObject _joinScreen;
         
     
         [SerializeField] private Button _hostButton;
         [SerializeField] private Button _JoinButton;
     
     
         [SerializeField] private Button _submitCodeButton;
         [SerializeField] private TextMeshProUGUI _codetext;
         void OnEnable()
         {
             _hostButton.onClick.AddListener(OnHostClicked);
             _JoinButton.onClick.AddListener(OnJoinClicked);
             _submitCodeButton.onClick.AddListener(OnSubmitCOdeClicked);
         }
     
         private void OnDisable()
         {
             _hostButton.onClick.RemoveListener(OnHostClicked);
             _JoinButton.onClick.RemoveListener(OnJoinClicked);
             _submitCodeButton.onClick.RemoveListener(OnSubmitCOdeClicked);
     
         }
     
     
         private async void OnHostClicked()
         {
             bool succeded = await GameLobbyManager.Instance.CreateLobby();
     
             if (succeded)
             {
                 SceneManager.LoadSceneAsync("Lobby");
             }
         }
     
         private void OnJoinClicked()
         {
              _mainScreen.SetActive(false);
              _joinScreen.SetActive(true);
         }
     
         private async void OnSubmitCOdeClicked()
         {
             string code = _codetext.text;
     
             code = code.Substring(0, code.Length - 1);
     
             bool succeded =  await GameLobbyManager.Instance.JoinLobby(code);
             
             if (succeded)
             {
                 SceneManager.LoadSceneAsync("Lobby");
             }
         }
     }
}

