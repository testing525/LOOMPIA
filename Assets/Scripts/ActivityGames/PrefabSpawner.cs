using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] prefabs;     
    public Transform[] spawnPoints;  

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (prefabs.Length == 0 || spawnPoints.Length == 0) return;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
