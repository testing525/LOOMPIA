using UnityEngine;

public class BookshelfManager : MonoBehaviour
{
    public Bookshelf[] bookshelves;
    public int emptyCount = 4;

    public BookBoxSpawner spawner;

    private void Start()
    {
        SetRandomEmptyShelves();

        if (spawner != null)
        {
            spawner.maxSpawns = emptyCount;
            spawner.SpawnBoxesForEmptyShelves();
        }
    }

    private void SetRandomEmptyShelves()
    {
        if (bookshelves.Length == 0) return;

        foreach (var shelf in bookshelves)
        {
            shelf.SetEmpty(false);
        }

        int chosen = 0;
        while (chosen < emptyCount)
        {
            int index = Random.Range(0, bookshelves.Length);
            if (!bookshelves[index].IsEmpty())
            {
                bookshelves[index].SetEmpty(true);
                chosen++;
            }
        }
    }
}
