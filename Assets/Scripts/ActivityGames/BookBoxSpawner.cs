using UnityEngine;
using System.Collections.Generic;

public class BookBoxSpawner : MonoBehaviour
{
    [Header("References")]
    public BookshelfManager bookshelfManager;
    public Transform spawnParent;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Box Prefabs")]
    public GameObject historyBoxPrefab;
    public GameObject languageBoxPrefab;
    public GameObject technologyBoxPrefab;

    [Header("Spawn Settings")]
    public int maxSpawns = 4; 
    public bool showDebug = true;

    private List<GameObject> spawnedBoxes = new List<GameObject>();

    public void SpawnBoxesForEmptyShelves()
    {
        if (bookshelfManager == null || spawnPoints.Length == 0) return;

        List<Bookshelf> emptyShelves = new List<Bookshelf>();
        foreach (var shelf in bookshelfManager.bookshelves)
        {
            if (shelf.IsEmpty()) emptyShelves.Add(shelf);
        }

        for (int i = 0; i < emptyShelves.Count; i++)
        {
            int rand = Random.Range(i, emptyShelves.Count);
            (emptyShelves[i], emptyShelves[rand]) = (emptyShelves[rand], emptyShelves[i]);
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int rand = Random.Range(i, spawnPoints.Length);
            (spawnPoints[i], spawnPoints[rand]) = (spawnPoints[rand], spawnPoints[i]);
        }


        int spawnCount = Mathf.Min(emptyShelves.Count, spawnPoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            Bookshelf shelf = emptyShelves[i];
            Transform spawnPoint = spawnPoints[i]; // no wraparound

            GameObject prefabToSpawn = GetPrefabForShelf(shelf.shelfIdentity);
            if (prefabToSpawn != null)
            {
                GameObject box = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity, spawnParent);
                spawnedBoxes.Add(box);
            }
        }


    }

    private GameObject GetPrefabForShelf(Bookshelf.ShelfType type)
    {
        switch (type)
        {
            case Bookshelf.ShelfType.History: return historyBoxPrefab;
            case Bookshelf.ShelfType.Language: return languageBoxPrefab;
            case Bookshelf.ShelfType.Technology: return technologyBoxPrefab;
            default: return null;
        }
    }
}
