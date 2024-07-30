using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{
    
    private SerializedDictionary<int, PlayerPositions> _playerPositionsMap;
    public List<GameObject> _levelList;
    private GameObject Ivariable;
    private int currentLevel;
    private GameObject _gameObject;
    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel >= _levelList.Count)
        {
            currentLevel = 0;
        }

        _gameObject= _levelList[currentLevel];
        
         if (Ivariable!=null)
         {
             DestroyImmediate(Ivariable);
         }
        Ivariable =Instantiate(_gameObject);
        Debug.Log("scene loaded"+_gameObject.name);
    }

    private void Start()
    {
       
        Cursor.lockState = CursorLockMode.None;
        currentLevel = -1;
        NextLevel();
        
    }
    
}

[Serializable]
public class PlayerPositions
{
    public Vector3 player1;
    public Vector3 player2;
    
}
