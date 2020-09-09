using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    public spawn[] block;
    [SerializeField] float spawnDelay = 3f;
    [SerializeField] float DelayUntilRateDecrease = 30f;
    void Start()
    {
        StartCoroutine(SpawnObjects(spawnDelay));   
    }


    public IEnumerator SpawnObjects(float firstDelay)
    {
        float spawnRateCountDown = DelayUntilRateDecrease;
        float spawnCountDown = firstDelay;

        while(true)
        {
            yield return null;

            spawnRateCountDown -= Time.deltaTime;
            spawnCountDown -= Time.deltaTime;

            // Should a new object be spawnned?
            if(spawnCountDown < 0)
            {
                spawnCountDown += spawnDelay;
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

            if(spawnRateCountDown < 0  && spawnDelay > 1)
            {
                spawnRateCountDown += DelayUntilRateDecrease;
                spawnDelay -= 0.2f;
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
