using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 1.6f; // seconds between spawns
    public float gapSize = 2.2f;   // vertical gap between top/bottom
    public float yRange = 2f;      // random vertical offset range
    public float spawnX = 10f;     // x position to spawn at

    Coroutine spawnRoutine;

    void Start()
    {
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(1f); // small start delay
        while (true)
        {
            SpawnPipePair();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnPipePair()
    {
        float centerY = Random.Range(-yRange, yRange);

        // The pipePrefab should have top and bottom child positioned relative to center
        GameObject pipe = Instantiate(pipePrefab, new Vector3(spawnX, centerY, 0f), Quaternion.identity);
        // If the pipe prefab already contains the gap, the spawn position shift is enough
    }

    public void Stop()
    {
        if (spawnRoutine != null) StopCoroutine(spawnRoutine);
    }
}
