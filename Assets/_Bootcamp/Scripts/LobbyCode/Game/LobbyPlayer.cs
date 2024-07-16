using System;
using _Bootcamp.Scripts.LobbyCode.Game.Data;
using TMPro;
using UnityEngine;

namespace Game
{
    public class LobbyPlayer : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _playerName;
        [SerializeField] private Renderer _isReadyRenderer;

        private MaterialPropertyBlock _propertyBlock;
        private LobbyPlayerData _data;

        private void Start()
        {
            _propertyBlock = new MaterialPropertyBlock();
        }

        public void SetData(LobbyPlayerData data)
        {
            // Check if the GameObject is active and not null before proceeding
            if (this == null || !gameObject.activeInHierarchy)
            {
                Debug.LogWarning("LobbyPlayer object is null or inactive. Aborting SetData.");
                return;
            }

            _data = data;
            _playerName.text = _data.GamerTag;

            if (_data.IsReady)
            {
                if (_isReadyRenderer != null)
                {
                    _isReadyRenderer.GetPropertyBlock(_propertyBlock);
                    _propertyBlock.SetColor("BaseColor", Color.white);
                    _isReadyRenderer.SetPropertyBlock(_propertyBlock);
                    Debug.Log("green");
                }
            }
            gameObject.SetActive(true);
        }
    }
}