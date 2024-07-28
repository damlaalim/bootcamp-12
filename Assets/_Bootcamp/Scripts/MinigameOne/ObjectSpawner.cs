using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectPrefabs; 
    public Transform spawnPoint;
    public Transform finishPoint;
    public float initialSpawnRate = 1f;
    public float initialObjectSpeed = 1f;
    public float difficultyIncreaseRate = 0.1f;
    public float maxSpawnRate = 5f;
    public float maxSpeed = 10f;

    private float currentSpawnRate;
    private float currentObjectSpeed;
    private float nextSpawnTime;
    private int currentPrefabIndex = 0;
    private int objectsInPlay = 0;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        currentObjectSpeed = initialObjectSpeed;

        SpawnObject();
        nextSpawnTime = Time.time + 1f / currentSpawnRate;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && currentPrefabIndex < objectPrefabs.Count)
        {
            SpawnObject();
            nextSpawnTime = Time.time + 1f / currentSpawnRate;
        }
    }

    void SpawnObject()
    {
        if (objectPrefabs.Count == 0)
        {
            Debug.LogWarning("No prefabs assigned to the spawner.");
            return;
        }

        GameObject obj = Instantiate(objectPrefabs[currentPrefabIndex], spawnPoint.position, Quaternion.identity);
        MovingObject movingObject = obj.GetComponent<MovingObject>();
        movingObject.Initialize(finishPoint.position, currentObjectSpeed, OnObjectDestroyed);

        currentPrefabIndex++;
        objectsInPlay++;

        if (currentPrefabIndex < objectPrefabs.Count)
        {
            IncreaseDifficulty();
        }
    }

    void IncreaseDifficulty()
    {
        currentSpawnRate = Mathf.Min(maxSpawnRate, currentSpawnRate + difficultyIncreaseRate);
        currentObjectSpeed = Mathf.Min(maxSpeed, currentObjectSpeed + difficultyIncreaseRate);
    }

    void OnObjectDestroyed(MovingObject movingObject)
    {
        objectsInPlay--;
        if (objectsInPlay == 0 && currentPrefabIndex >= objectPrefabs.Count)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        //
    }
}
