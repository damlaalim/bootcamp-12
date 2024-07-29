using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class UserName : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject usernamePage;
    public TMP_Text myUsername;

     void Start()
    {
        if (PlayerPrefs.GetString("UserName") == "" || PlayerPrefs.GetString("UserName") == null)
        {
            usernamePage.SetActive(true);
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("UserName");
            myUsername.text = PlayerPrefs.GetString("UserName");
            usernamePage.SetActive(false);
        }
        
        
    }


    public void SaveUserName()
    {
        PhotonNetwork.NickName = inputField.text;
        
        PlayerPrefs.SetString("UserName", inputField.text);

        myUsername.text = inputField.text;
        
        usernamePage.SetActive(false);
    }
    
}
