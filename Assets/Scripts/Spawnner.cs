using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    public spawn[] block;
    [SerializeField] float spawnDelay;
    [SerializeField] float DelayUntilRateDecrease = 30f;
    [SerializeField] float timeBeforeSpawnStart;

    Vector2 screenHalfSizeWorlUnits;
    void Start()
    {
        screenHalfSizeWorlUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize + 5f, Camera.main.orthographicSize);
        spawnDelay = Random.Range(2.5f, 6f);
        float tempRandom = Random.Range(0.5f, 1.5f);
        spawnDelay += tempRandom;
        timeBeforeSpawnStart = Random.Range(1f, 5f);
        StartCoroutine(SpawnObjects(spawnDelay));
    }


    public IEnumerator SpawnObjects(float firstDelay)
    {
        float spawnRateCountDown = DelayUntilRateDecrease;
        float spawnCountDown = firstDelay;
        float intialSpawnDelay = timeBeforeSpawnStart;

        while(true)
        {
            yield return null;
            spawnRateCountDown -= Time.deltaTime;
            spawnCountDown -= Time.deltaTime;
            intialSpawnDelay -= Time.deltaTime;
            //Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorlUnits.x, screenHalfSizeWorlUnits.x), screenHalfSizeWorlUnits.y);

            // Should a new object be spawnned?
            if(intialSpawnDelay< 0)
            {
                if(spawnCountDown < 0)
                {
                    spawnCountDown += spawnDelay;
                    InitiateSpawn();
                }
            }
            
            // Should the rate decrease?
            if (spawnRateCountDown < 0  && spawnDelay > 1)
            {
                spawnRateCountDown += DelayUntilRateDecrease;
                spawnDelay -= 0.2f;
            }

        }
    }

    private void InitiateSpawn()
    {
        int i = Random.Range(0, 100);

        for (int j = 0; j < block.Length; j++)
        {
            if (i >= block[j].minProbabilityRange && i <= block[j].maxProbabilityRange)
            {
                Instantiate(block[j].spawnObject, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}

[System.Serializable]
public class spawn
{
    public GameObject spawnObject;
    public int minProbabilityRange = 0;
    public int maxProbabilityRange = 0;
}
