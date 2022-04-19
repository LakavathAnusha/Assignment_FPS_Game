using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform playerSpawnPoints; //Parent of the spawn points
    Transform[] spawnPositions;
    public GameObject enemy;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {

        spawnPositions = playerSpawnPoints.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if (time > 3f)
        {
            int i = Random.Range(1, spawnPositions.Length);
            Debug.Log(i);
            Instantiate(enemy, spawnPositions[i].transform.position, Quaternion.identity);
            time = 0f;
        }

    }
}

