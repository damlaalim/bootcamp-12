using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Init : MonoBehaviour
    {
        async void Start()
        {
            await UnityServices.InitializeAsync();

            if (UnityServices.State == ServicesInitializationState.Initialized)
            {
            
                AuthenticationService.Instance.SignedIn += OnSignedIn;

                await AuthenticationService.Instance.SignInAnonymouslyAsync();


                if (AuthenticationService.Instance.IsSignedIn)
                {
                    string username = PlayerPrefs.GetString("Username");

                    if (username == "")
                    {
                        username = "Player";
                        PlayerPrefs.SetString("Username", username);
                    }

                    SceneManager.LoadSceneAsync("mainMenu");
                }
            }
        }

        private void OnSignedIn()
        {
            Debug.Log($"Token: {AuthenticationService.Instance.AccessToken}");
            Debug.Log($"Player ID: {AuthenticationService.Instance.PlayerId}");
            

        }
    }
}

