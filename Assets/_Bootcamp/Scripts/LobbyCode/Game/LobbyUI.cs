using System;
using System.Collections;
using System.Collections.Generic;
using _Bootcamp.Scripts.LobbyCode.Game.Data;
using _Bootcamp.Scripts.LobbyCode.GameFramework.Events;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;
using LobbyEvents = Game.Events.LobbyEvents;

namespace Game
{
    public class LobbyUI : MonoBehaviour
    {
    
        [SerializeField] private TextMeshProUGUI _lobbyCodeText;
        [SerializeField] private Button _readyButton;
        [SerializeField] private Button _startButton;


        [SerializeField] private Image _mapImage;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private TextMeshProUGUI _mapName;

        [SerializeField] private MapSelectionData _mapSelectionData;


        private int _currentMapIndex = 0;
        private void OnEnable()
        {
            _readyButton.onClick.AddListener(OnReadyPressed);
            
            if (GameLobbyManager.Instance.IsHost)
            { 
                
                _leftButton.onClick.AddListener(OnleftButtonClick); 
                _rightButton.onClick.AddListener(OnRightButtonClick);
                _startButton.onClick.AddListener(OnStartButtonClick);
                LobbyEvents.OnLobbyReady += OnLobbyReady;
            }
            
            LobbyEvents.OnLobbyUpdated += OnLobbyUpdated;

        }

      

        private void OnDisable()
        {
                 _readyButton.onClick.RemoveAllListeners();
                 _leftButton.onClick.RemoveAllListeners();
                 _rightButton.onClick.RemoveAllListeners(); 
                 _startButton.onClick.RemoveAllListeners();
                 LobbyEvents.OnLobbyUpdated -= OnLobbyUpdated;
                 LobbyEvents.OnLobbyReady -= OnLobbyReady;
                  
            
        }


        private async void Start()
        {
            _lobbyCodeText.text = $"Lobby code: {GameLobbyManager.Instance.GetLobbyCode()}";

            if (!GameLobbyManager.Instance.IsHost)
            {
                 _leftButton.gameObject.SetActive(false);
                 _rightButton.gameObject.SetActive(false);
            }
            else
            {
                await GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex,_mapSelectionData.Maps[_currentMapIndex].SceneName);

            }
        }


        private async void OnleftButtonClick()
        {
            if (_currentMapIndex - 1 > 0)
            {
                _currentMapIndex--;
            }
            else
            {
                _currentMapIndex = 0;
            }

            UpdateMap();
           await GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex,_mapSelectionData.Maps[_currentMapIndex].SceneName);
        }

       

        private async void OnRightButtonClick()
        {
            
            if (_currentMapIndex + 1 < _mapSelectionData.Maps.Count-1)
            {
                _currentMapIndex++;
            }
            else
            {
                _currentMapIndex = _mapSelectionData.Maps.Count-1;
            }
            
            
            UpdateMap();
            await GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex,_mapSelectionData.Maps[_currentMapIndex].SceneName);
        }
        
        
        
        private async void OnReadyPressed()
        {
            bool succeed = await GameLobbyManager.Instance.SetPlayerReady();

            if (succeed)
            {
                 _readyButton.gameObject.SetActive(false);
            }
        }
        
        private void OnLobbyReady()
        {
            _startButton.gameObject.SetActive(true);
            Debug.Log("Start");
        }
        
        private void UpdateMap()
        {
            _mapImage.color = _mapSelectionData.Maps[_currentMapIndex].MapThumbnail;
            _mapName.text = _mapSelectionData.Maps[_currentMapIndex].MapName;
        }
        
        private void OnLobbyUpdated()
        {
             _currentMapIndex = GameLobbyManager.Instance.GetMapIndex();
             UpdateMap();
        }
        
        
          private async void OnStartButtonClick()
          {

              await GameLobbyManager.Instance.StartGame();

          }
        
        
        
    }
    
  
    
    
    
    
    
    
}

